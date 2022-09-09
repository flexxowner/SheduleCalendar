using System;
using System.Collections.ObjectModel;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using CalendarAppointments.ViewModel.Service;
using System.ComponentModel;
using CalendarAppointments.ViewModel.Services;
using CalendarAppointments.ViewModel.Extensions;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class ListViewModel : ObservableObject, INotifyPropertyChanged
    {
        private const string firstPath = "Appointments.xml";
        private const string secondPath = "outlook.xml";
        private const int min = 1;
        private readonly ObservableCollection<ListModel> dates;
        private DateTime currentDate;
        private int year;

        public ListViewModel()
        {
            currentDate = DateTime.Today;
            year = currentDate.Year;
            dates = new ObservableCollection<ListModel>();
            dates.AddDates(year, firstPath, secondPath);
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
        }

        public RelayCommand GoBackCommand { get; set; }

        public RelayCommand GoForwardCommand { get; set; }

        public DateTime CurrentDate
        {
            get => currentDate;
            set
            { 
                currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        public ObservableCollection<ListModel> Dates
        {
            get => dates;
        }

        public int Year
        {
            get => year;
            set { year = value; }
        }

        private void GoBack()
        {
            CurrentDate = DataChanger.ChangeDateBack(CurrentDate, min);
            dates.Clear();
            Dates.AddDates(year, firstPath, secondPath);
        }

        private void GoForward()
        {
            CurrentDate = DataChanger.ChangeDateForward(CurrentDate, min);
            dates.Clear();
            Dates.AddDates(year, firstPath, secondPath);
        }

    }
}
