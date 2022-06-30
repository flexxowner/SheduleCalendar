using System;

using SheduleCalendar.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SheduleCalendar.Views
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
