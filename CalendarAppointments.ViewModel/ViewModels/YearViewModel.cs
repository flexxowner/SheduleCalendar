using System;
using System.Collections.ObjectModel;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using CalendarAppointments.ViewModel.Extensions;
using Microsoft.Toolkit.Mvvm.Input;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class YearViewModel : ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        private ObservableCollection<Year> years;
        private DateTime currentDate;
        private const string path = "Appointments.xml";
        private const string secondPath = "outlook.xml";
        private int year;

        public YearViewModel()
        {
            currentDate = DateTime.Today;
            year = currentDate.Year;
            years = new ObservableCollection<Year>();
            years.AddDays(CurrentDate.Year, path, secondPath);
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
        }

        public ObservableCollection<Year> Years
        {
            get { return years; }
            set { years = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set 
            { 
                currentDate = value;
                OnPropertyChanged();
            }
        }

        private void GoBack()
        {
            CurrentDate = new DateTime(CurrentDate.Year - 1, 1, 1);
            years.Clear();
            years.AddDays(CurrentDate.Year, path, secondPath);
        }

        private void GoForward()
        {
            CurrentDate = new DateTime(CurrentDate.Year + 1, 1, 1);
            years.Clear();
            years.AddDays(CurrentDate.Year, path, secondPath);
        }
    }
}
