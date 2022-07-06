using System;

using SheduleCalendar.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SheduleCalendar.Views
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
