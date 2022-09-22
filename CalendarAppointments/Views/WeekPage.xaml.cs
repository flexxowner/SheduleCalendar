using System;
using Windows.UI.Xaml.Controls;
using CalendarAppointments.ViewModel.ViewModels;
namespace CalendarAppointments.Views
{
    public sealed partial class WeekPage : Page
    {
        public WeekViewModel ViewModel { get; } = new WeekViewModel();

        public WeekPage()
        {
            InitializeComponent();
        }
    }
}
