using System;

using CalendarAppointments.ViewModels;

using Windows.UI.Xaml.Controls;

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
