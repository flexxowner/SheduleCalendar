using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using Helpers.Helpers;
using System.Collections.ObjectModel;
using System.Timers;
using System.Threading;

namespace CalendarAppointments.Controllers
{
    public class Graph
    {
        public RelayCommand SignOutCommand { get; set; }
        public RelayCommand CallGraphCommand { get; set; }
        private static readonly IPublicClientApplication Pca = PublicClientApplicationBuilder.Create(ClientId).WithTenantId(Tenant).Build();
        private const string path = "outlook.xml";
        private const string ClientId = "efaa2831-d90e-4876-8fd3-560952cdff66";
        private const string Tenant = "common";
        private const string Authority = "https://login.microsoftonline.com/" + Tenant;
        private static IPublicClientApplication PublicClientApp;
        private static string MSGraphURL = "https://graph.microsoft.com/v1.0/";
        private static AuthenticationResult authResult;
        private static string[] scopes = new string[] { "user.read", "Calendars.Read" };

        public Graph()
        {
            CallGraphCommand = new RelayCommand(CallGraph);
            SignOutCommand = new RelayCommand(SignOut);
        }

        public static async void GetEventsAsync()
        {
            var appointments = new ObservableCollection<Models.Models.Event>();
            try
            {
                var authProvider = new DelegateAuthenticationProvider(async (request) =>
                {

                    var result = await Pca.AcquireTokenByIntegratedWindowsAuth(scopes).ExecuteAsync();

                    request.Headers.Authorization =
                        new AuthenticationHeaderValue("bearer", result.AccessToken);
                });
                GraphServiceClient graphClient = await SignInAndInitializeGraphServiceClient(scopes);
                var events = await graphClient.Me.Calendar.Events
                    .Request()
                    .GetAsync();
                var list = events.ToList();
                foreach (var item in list)
                {
                    var startDate = DateTime.Parse(item.Start.DateTime);
                    var endDate = DateTime.Parse(item.End.DateTime);
                    appointments.Add(new Models.Models.Event() { Subject = item.Subject, SerializableDate = item.Start.DateTime, StartTime = startDate.ToLocalTime().TimeOfDay, EndTime = endDate.ToLocalTime().TimeOfDay });
                }
            }
            catch (Exception e)
            {
                DialogHelper.ErrorDialog(e);
            }
            if (appointments != null)
            {
                FileManager.SaveToExistingFile(appointments,path);
            }
        }

        private static async Task<string> SignInUserAndGetTokenUsingMSAL(string[] scopes)
        {
            PublicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority(Authority)
                .WithUseCorporateNetwork(false)
                .WithRedirectUri(DefaultRedirectUri.Value)
                 .WithLogging((level, message, containsPii) =>
                 {
                     Debug.WriteLine($"MSAL: {level} {message} ");
                 }, LogLevel.Warning, enablePiiLogging: false, enableDefaultPlatformLogging: true)
                .Build();

            IEnumerable<IAccount> accounts = await PublicClientApp.GetAccountsAsync().ConfigureAwait(false);
            IAccount firstAccount = accounts.FirstOrDefault();

            try
            {
                authResult = await PublicClientApp.AcquireTokenSilent(scopes, firstAccount)
                                                  .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                authResult = await PublicClientApp.AcquireTokenInteractive(scopes)
                                                  .ExecuteAsync()
                                                  .ConfigureAwait(false);

            }
            return authResult.AccessToken;
        }

        private async void CallGraph()
        {
            try
            {
                GraphServiceClient graphClient = await SignInAndInitializeGraphServiceClient(scopes);
                User GraphUser = await graphClient.Me.Request().GetAsync();
            }
            catch(Exception e)
            {
                DialogHelper.ErrorDialog(e);
            }
        }

        private async void SignOut()
        {
            try
            {
                IEnumerable<IAccount> accounts = await PublicClientApp.GetAccountsAsync().ConfigureAwait(false);
                IAccount firstAccount = accounts.FirstOrDefault();
                await PublicClientApp.RemoveAsync(firstAccount).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                DialogHelper.ErrorDialog(e);
            }
        }

        private async static Task<GraphServiceClient> SignInAndInitializeGraphServiceClient(string[] scopes)
        {
            GraphServiceClient graphClient = new GraphServiceClient(MSGraphURL,
                new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", await SignInUserAndGetTokenUsingMSAL(scopes));
                }));

            return await Task.FromResult(graphClient);
        }

    }
}
