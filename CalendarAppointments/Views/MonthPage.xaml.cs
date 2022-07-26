using System;
using System.Threading.Tasks;
using CalendarAppointments.BackgroundTasks;
using CalendarAppointments.Controllers;
using CalendarAppointments.Services;
using CalendarAppointments.ViewModels;
using Microsoft.Office365.OutlookServices;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.UI.Xaml.Controls;

namespace CalendarAppointments.Views
{
    public sealed partial class MonthPage : Page
    {
        public MonthViewModel ViewModel { get; } = new MonthViewModel();

        public MonthPage()
        {
            InitializeComponent();
            this.Loaded += MonthPage_Loaded;
            BackgroundTaskService.GetBackgroundTasksRegistration<BackgroundTask>();
        }

        private async void MonthPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await EnsureClient();
        }

        private static async Task<string> GetAccessToken()
        {
            string token = null;

            //first try to get the token silently
            WebAccountProvider aadAccountProvider = await WebAuthenticationCoreManager.FindAccountProviderAsync("https://login.windows.net");
            WebTokenRequest webTokenRequest = new WebTokenRequest(aadAccountProvider, String.Empty, App.Current.Resources["ida:ClientID"].ToString(), WebTokenRequestPromptType.Default);
            webTokenRequest.Properties.Add("authority", "https://login.windows.net");
            webTokenRequest.Properties.Add("resource", "https://outlook.office365.com/");
            WebTokenRequestResult webTokenRequestResult = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(webTokenRequest);
            if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
            {
                WebTokenResponse webTokenResponse = webTokenRequestResult.ResponseData[0];
                token = webTokenResponse.Token;
            }
            else if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.UserInteractionRequired)
            {
                //get token through prompt
                webTokenRequest = new WebTokenRequest(aadAccountProvider, String.Empty, App.Current.Resources["ida:ClientID"].ToString(), WebTokenRequestPromptType.ForceAuthentication);
                webTokenRequest.Properties.Add("authority", "https://login.windows.net");
                webTokenRequest.Properties.Add("resource", "https://outlook.office365.com/");
                webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest);
                if (webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success)
                {
                    WebTokenResponse webTokenResponse = webTokenRequestResult.ResponseData[0];
                    token = webTokenResponse.Token;
                }
            }

            return token;
        }

        private static async Task<OutlookServicesClient> EnsureClient()
        {
            return new OutlookServicesClient(new Uri("https://outlook.office365.com/ews/odata"), async () => {
                return await GetAccessToken();
            });
        }
    }
}
