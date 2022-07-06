using System;

using SheduleCalendar.ViewModels;

using Windows.UI.Xaml.Controls;

namespace SheduleCalendar.Views
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
