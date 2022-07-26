
using CalendarAppointments.Controllers;
using CalendarAppointments.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CalendarAppointments.Views
{

    public sealed partial class SettingsPage : Page
    {
        SettingsViewModel ViewModel { get; } = new SettingsViewModel();
        public GraphControl GraphControl { get; } = new GraphControl();
        public SettingsPage()
        {
            this.InitializeComponent();
        }
    }
}
