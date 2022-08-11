using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;


namespace CalendarAppointments.Controllers
{
    public class GraphService
    {
        private const string ClientId = "efaa2831-d90e-4876-8fd3-560952cdff66";
        private const string Tenant = "common";
        private const string Authority = "https://login.microsoftonline.com/" + Tenant;
        public RelayCommand SignOutCommand { get; set; }
        public RelayCommand CallGraphCommand { get; set; }
        private static IPublicClientApplication PublicClientApp;
        private static string MSGraphURL = "https://graph.microsoft.com/v1.0/";
        private static AuthenticationResult authResult;
        private string[] scopes = new string[] { "user.read" };
        private IPublicClientApplication Pca = PublicClientApplicationBuilder
            .Create(ClientId)
            .WithTenantId(Tenant)
            .Build();

        public GraphService()
        {
            CallGraphCommand = new RelayCommand(CallGraph);
            SignOutCommand = new RelayCommand(SignOut);
        }

        public async Task<IUserCalendarsCollectionPage> GetAllCalendars()
        {
            var authProvider = new DelegateAuthenticationProvider(async (request) => {
                var result = await Pca.AcquireTokenByIntegratedWindowsAuth(scopes).ExecuteAsync();

                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.AccessToken);
            });
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);
            var calendars = await graphClient.Me.Calendars
                .Request()
                .GetAsync();
            return calendars;
        }

        public async Task<Calendar> GetCalendar()
        {
            var authProvider = new DelegateAuthenticationProvider(async (request) => {
                
                var result = await Pca.AcquireTokenByIntegratedWindowsAuth(scopes).ExecuteAsync();

                request.Headers.Authorization =
                    new AuthenticationHeaderValue("bearer", result.AccessToken);
            });
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);
            var calendar = await graphClient.Me.Calendar
                .Request()
                .GetAsync();
            return calendar;
        }
        public async Task<ICalendarEventsCollectionPage> GetEventsAsync()
        {
            var authProvider = new DelegateAuthenticationProvider(async (request) => {

                var result = await Pca.AcquireTokenByIntegratedWindowsAuth(scopes).ExecuteAsync();

                request.Headers.Authorization =
                    new AuthenticationHeaderValue("bearer", result.AccessToken);
            });
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);
            var events = await graphClient.Me.Calendar.Events
                .Request()
                .Filter("startsWith(subject,'All')")
                .GetAsync();
            return events;
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
            catch (ServiceException)
            {

            }
        }

        private async void SignOut()
        {
            IEnumerable<IAccount> accounts = await PublicClientApp.GetAccountsAsync().ConfigureAwait(false);
            IAccount firstAccount = accounts.FirstOrDefault();
            await PublicClientApp.RemoveAsync(firstAccount).ConfigureAwait(false);
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
