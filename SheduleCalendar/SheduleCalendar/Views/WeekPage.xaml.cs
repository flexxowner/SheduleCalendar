using System;

using SheduleCalendar.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SheduleCalendar.Views
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
