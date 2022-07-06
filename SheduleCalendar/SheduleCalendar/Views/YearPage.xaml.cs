using System;

using SheduleCalendar.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SheduleCalendar.Views
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
