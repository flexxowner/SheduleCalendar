using System;
using System.Threading.Tasks;
using CalendarAppointments.BackgroundTasks;
using CalendarAppointments.Controllers;
using CalendarAppointments.Services;
using CalendarAppointments.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CalendarAppointments.Views
{
    public sealed partial class MonthPage : Page
    {
        public MonthViewModel ViewModel { get; } = new MonthViewModel();

        public MonthPage()
        {
            InitializeComponent();
            BackgroundTaskService.GetBackgroundTasksRegistration<BackgroundTask>();
        }

    }
}
