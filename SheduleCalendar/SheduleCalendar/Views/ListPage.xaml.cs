using System;

using SheduleCalendar.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SheduleCalendar.Views
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
