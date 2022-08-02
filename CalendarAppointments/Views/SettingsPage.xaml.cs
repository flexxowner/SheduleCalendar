using CalendarAppointments.Controllers;
using CalendarAppointments.Services;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CalendarAppointments.Views
{

    public sealed partial class SettingsPage : Page
    {
        SettingsPageService ViewModel { get; } = new SettingsPageService();
        public SettingsPage()
        {
            this.InitializeComponent();
        }
    }
}
