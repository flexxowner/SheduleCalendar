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
        //Set the scope for API call to user.read
        private string[] scopes = new string[] { "user.read" };

        // Below are the clientId (Application Id) of your app registration and the tenant information. 
        // You have to replace:
        // - the content of ClientID with the Application Id for your app registration
        // - The content of Tenant by the information about the accounts allowed to sign-in in your application:
        //   - For Work or School account in your org, use your tenant ID, or domain
        //   - for any Work or School accounts, use organizations
        //   - for any Work or School accounts, or Microsoft personal account, use common
        //   - for Microsoft Personal account, use consumers
        private const string ClientId = "4603eb32-dedb-471c-92aa-48c6285e6d52";

        private const string Tenant = "common"; // Alternatively "[Enter your tenant, as obtained from the azure portal, e.g. kko365.onmicrosoft.com]"
        private const string Authority = "https://login.microsoftonline.com/" + Tenant;

        IPublicClientApplication pca = PublicClientApplicationBuilder
            .Create(ClientId)
            .WithTenantId(Tenant)
            .Build();
        // The MSAL Public client app
        private static IPublicClientApplication PublicClientApp;

        private static string MSGraphURL = "https://graph.microsoft.com/v1.0/";
        private static AuthenticationResult authResult;


        public GraphService()
        {
            CallGraphCommand = new RelayCommand(CallGraph);
            SignOutCommand = new RelayCommand(SignOut);
        }
        /// <summary>
        /// Signs in the user and obtains an Access token for MS Graph
        /// </summary>
        /// <param name="scopes"></param>
        /// <returns> Access Token</returns>
        private static async Task<string> SignInUserAndGetTokenUsingMSAL(string[] scopes)
        {
            // Initialize the MSAL library by building a public client application
            PublicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority(Authority)
                .WithUseCorporateNetwork(false)
                .WithRedirectUri(DefaultRedirectURI.Value)
                 .WithLogging((level, message, containsPii) =>
                 {
                     Debug.WriteLine($"MSAL: {level} {message} ");
                 }, LogLevel.Warning, enablePiiLogging: false, enableDefaultPlatformLogging: true)
                .Build();

            // It's good practice to not do work on the UI thread, so use ConfigureAwait(false) whenever possible.
            IEnumerable<IAccount> accounts = await PublicClientApp.GetAccountsAsync().ConfigureAwait(false);
            IAccount firstAccount = accounts.FirstOrDefault();

            try
            {
                authResult = await PublicClientApp.AcquireTokenSilent(scopes, firstAccount)
                                                  .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                // A MsalUiRequiredException happened on AcquireTokenSilentAsync. This indicates you need to call AcquireTokenAsync to acquire a token
                Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                authResult = await PublicClientApp.AcquireTokenInteractive(scopes)
                                                  .ExecuteAsync()
                                                  .ConfigureAwait(false);

            }
            return authResult.AccessToken;
        }

        /// <summary>
        /// Sign in user using MSAL and obtain a token for MS Graph
        /// </summary>
        /// <returns>GraphServiceClient</returns>
        private async static Task<GraphServiceClient> SignInAndInitializeGraphServiceClient(string[] scopes)
        {
            GraphServiceClient graphClient = new GraphServiceClient(MSGraphURL,
                new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", await SignInUserAndGetTokenUsingMSAL(scopes));
                }));

            return await Task.FromResult(graphClient);
        }

        public async Task<IUserCalendarsCollectionPage> GetAllCalendars()
        {
            var authProvider = new DelegateAuthenticationProvider(async (request) => {
                // Use Microsoft.Identity.Client to retrieve token
                var result = await pca.AcquireTokenByIntegratedWindowsAuth(scopes).ExecuteAsync();

                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
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
                // Use Microsoft.Identity.Client to retrieve token
                var result = await pca.AcquireTokenByIntegratedWindowsAuth(scopes).ExecuteAsync();

                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
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
                // Use Microsoft.Identity.Client to retrieve token
                var result = await pca.AcquireTokenByIntegratedWindowsAuth(scopes).ExecuteAsync();

                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);
            });
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);
            var events = await graphClient.Me.Calendar.Events
                .Request()
                .Filter("startsWith(subject,'All')")
                .GetAsync();
            return events;
        }


        public RelayCommand CallGraphCommand { get; set; }
        private async void CallGraph()
        {
            // Sign-in user using MSAL and obtain an access token for MS Graph
            GraphServiceClient graphClient = await SignInAndInitializeGraphServiceClient(scopes);

            // Call the /me endpoint of Graph
          User graphUser = await graphClient.Me.Request().GetAsync();
        }
        public RelayCommand SignOutCommand { get; set; }
        private async void SignOut()
        {
            IEnumerable<IAccount> accounts = await PublicClientApp.GetAccountsAsync().ConfigureAwait(false);
            IAccount firstAccount = accounts.FirstOrDefault();
            await PublicClientApp.RemoveAsync(firstAccount).ConfigureAwait(false);

        }
    }
}
