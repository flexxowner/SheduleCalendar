using System;
using System.Collections.ObjectModel;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using CalendarAppointments.ViewModel.Service;
using Microsoft.Toolkit.Mvvm.Input;
using CalendarAppointments.ViewModel.Services;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class YearViewModel : ObservableObject
    {
        private const string Path = "Appointments.xml";
        private const string SecondPath = "outlook.xml";
        private const string Format = "MMMM";
        private const string Culture = "en";
        private const int Min = 1;
        private readonly ObservableCollection<Year> years;
        private DateTime currentDate;

        public YearViewModel()
        {
            currentDate = DateTime.Today;
            years = new ObservableCollection<Year>();
            years.AddDates(CurrentDate.Year, Path, SecondPath, Format, Culture);
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
        }

        public RelayCommand GoBackCommand { get; set; }

        public RelayCommand GoForwardCommand { get; set; }

        public ObservableCollection<Year> Years
        {
            get => years;
        }

        public DateTime CurrentDate
        {
            get => currentDate;
            set
            {
                currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        private void GoBack()
        {
            CurrentDate = DataChanger.ChangeDateBack(CurrentDate, Min);
            years.Clear();
            years.AddDates(CurrentDate.Year, Path, SecondPath, Format, Culture);
        }

        private void GoForward()
        {
            CurrentDate = DataChanger.ChangeDateForward(CurrentDate, Min);
            years.Clear();
            years.AddDates(CurrentDate.Year, Path, SecondPath, Format, Culture);
        }
    }
}
