using System;
using System.Collections.ObjectModel;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using CalendarAppointments.ViewModel.Service;
using System.ComponentModel;
using CalendarAppointments.ViewModel.Services;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class ListViewModel : ObservableObject, INotifyPropertyChanged
    {
        private const string FirstPath = "Appointments.xml";
        private const string SecondPath = "outlook.xml";
        private const int Min = 1;
        private readonly ObservableCollection<ListModel> dates;
        private DateTime currentDate;
        private int year;

        public ListViewModel()
        {
            currentDate = DateTime.Today;
            year = currentDate.Year;
            dates = new ObservableCollection<ListModel>();
            dates.AddDates(year, FirstPath, SecondPath);
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
            CurrentDate = DataChanger.ChangeDateBack(CurrentDate, Min);
            dates.Clear();
            Dates.AddDates(year, FirstPath, SecondPath);
        }

        private void GoForward()
        {
            CurrentDate = DataChanger.ChangeDateForward(CurrentDate, Min);
            dates.Clear();
            Dates.AddDates(year, FirstPath, SecondPath);
        }

    }
}
