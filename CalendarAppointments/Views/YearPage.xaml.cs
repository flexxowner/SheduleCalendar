using System;
using CalendarAppointments.ViewModel.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CalendarAppointments.Views
{
    public sealed partial class YearPage : Page
    {
        public YearViewModel ViewModel { get; } = new YearViewModel();

        public YearPage()
        {
            InitializeComponent();
        }
    }
}
