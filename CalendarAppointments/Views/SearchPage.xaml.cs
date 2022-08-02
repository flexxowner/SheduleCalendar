using System;
using CalendarAppointments.ViewModel.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CalendarAppointments.Views
{
    public sealed partial class SearchPage : Page
    {
        public SearchViewModel ViewModel { get; } = new SearchViewModel();

        public SearchPage()
        {
            InitializeComponent();
        }
    }
}
