using System;

using CalendarAppointments.ViewModels;

using Windows.UI.Xaml.Controls;

namespace CalendarAppointments.Views
{
    public sealed partial class DayPage : Page
    {
        public DayViewModel ViewModel { get; } = new DayViewModel();

        public DayPage()
        {
            InitializeComponent();
        }
    }
}
