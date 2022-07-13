using System;

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
        }
    }
}
