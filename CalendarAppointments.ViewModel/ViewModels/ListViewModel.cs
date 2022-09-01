using System;
using System.Collections.ObjectModel;
using System.IO;
using CalendarAppointments.Models.Models;
using Helpers.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using CalendarAppointments.ViewModel.Extensions;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class ListViewModel : ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        private DateTime currentDate;
        private int year;
        private ObservableCollection<ListModel> dates;
        private const string firstPath = "Appointments.xml";
        private const string secondPath = "outlook.xml";
        public ListViewModel()
        {
            currentDate = DateTime.Today;
            year = currentDate.Year;
            dates = new ObservableCollection<ListModel>();
            Dates.AddDates(year, firstPath, secondPath);
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
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

        public ObservableCollection<ListModel> Dates
        {
            get { return dates; }
            set { dates = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        private void GoBack()
        {
            CurrentDate = new DateTime(CurrentDate.Year - 1, 1, 1);
            dates.Clear();
            Dates.AddDates(year, firstPath, secondPath);
        }

        private void GoForward()
        {
            CurrentDate = new DateTime(CurrentDate.Year + 1, 1, 1);
            dates.Clear();
            Dates.AddDates(year, firstPath, secondPath);
        }

    }
}
