using System;
using CalendarAppointments.ViewModel.ViewModels;
using Windows.UI.Xaml.Controls;
namespace CalendarAppointments.Views
{
    public sealed partial class ListPage : Page
    {
        public ListViewModel ViewModel { get; } = new ListViewModel();

        public ListPage()
        {
            InitializeComponent();
        }
    }
}
