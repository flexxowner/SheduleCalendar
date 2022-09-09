using CalendarAppointments.BackgroundTasks;
using CalendarAppointments.Controllers;
using CalendarAppointments.Models.Models;
using CalendarAppointments.Services;
using CalendarAppointments.ViewModel.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CalendarAppointments.Views
{
    public sealed partial class MonthPage : Page
    {
        public MonthViewModel ViewModel { get; } = new MonthViewModel();
        public MonthPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            BackgroundTaskService.GetBackgroundTasksRegistration<BackgroundTask>();
        }
    }
}
