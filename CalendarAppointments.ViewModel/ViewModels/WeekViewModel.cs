using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class WeekViewModel : ObservableObject, INotifyPropertyChanged
    {
        Calendar cal = new CultureInfo("en-US").Calendar;
        public IEnumerable<string> Hours { get; set; }
        private readonly ObservableCollection<Week> weeks;

        public WeekViewModel()
        {
            weeks = new ObservableCollection<Week>();
        }

        public RelayCommand GoBackCommand { get; set; }

        public RelayCommand GoForwardCommand { get; set; }

        public ObservableCollection<Week> Weeks
        {
            get => weeks;
        }

    }
}
