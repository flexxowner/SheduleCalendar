using CalendarAppointments.Services;
using Windows.UI.Xaml.Controls;
using Helpers.Helpers;
using CalendarAppointments.Controllers;

namespace CalendarAppointments.Views
{
    public sealed partial class ShellPage : Page
    {
        ShellService ViewModel { get; } = new ShellService();
        Graph GraphControl { get; } = new Graph();
        public ShellPage()
        {
            InitializeComponent();
            DataContext = GraphControl;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }

    }
}
