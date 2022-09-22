using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Extensions;
using CalendarAppointments.ViewModel.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using DayOfWeek = CalendarAppointments.Models.Models.DayOfWeek;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class DayViewModel : ObservableObject, INotifyPropertyChanged
    {
        CultureInfo myCulture = new CultureInfo("en-US");
        Calendar cal = new CultureInfo("en-US").Calendar;
        private const string FirstPath = "Appointments.xml";
        private const string SecondPath = "outlook.xml";
        private const string Format = "MMMM";
        private readonly ObservableCollection<DayOfWeek> daysOfWeeks;
        private readonly ObservableCollection<DayHour> dayHours;
        private readonly List<DateTime> hours;
        private readonly ObservableCollection<Event> events;
        private DateTime today = DateTime.Now;
        private int currentWeek;
        private string month;

        public DayViewModel()
        {
            daysOfWeeks = new ObservableCollection<DayOfWeek>();
            events = new ObservableCollection<Event>();
            dayHours = new ObservableCollection<DayHour>();
            hours = DataChanger.GetHours();
            currentWeek = DataChanger.GetWeek(cal, today);
            month = DataChanger.DateToStringLower(today, Format, myCulture);
            DayHours.AddHours(hours, Today);
            DayHours.ReadEventsFromFile(FirstPath);
            DayHours.ReadEventsFromFile(SecondPath);
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
        }

        public RelayCommand GoBackCommand { get; set; }

        public RelayCommand GoForwardCommand { get; set; }

        public ObservableCollection<DayOfWeek> DaysOfWeeks
        {
            get => daysOfWeeks;
        }

        public ObservableCollection<DayHour> DayHours
        {
            get => dayHours;
        }

        public List<DateTime> Hours
        {
            get => hours;
        }

        public DateTime Today
        {
            get => today;
            set {
                today = value;
                OnPropertyChanged(nameof(Today));
            }
        }

        public ObservableCollection<Event> Events
        {
            get => events;
        }

        public int CurrentWeek
        {
            get => currentWeek;
            set
            { 
                currentWeek = value;
                OnPropertyChanged(nameof(CurrentWeek));
            }
        }

        public string Month
        {
            get => month;
            set 
            { 
                month = value;
                OnPropertyChanged(nameof(Month));
            }
        }

        private void GoBack()
        {
            Today = DataChanger.ChangeTodayBack(Today);
            Month = DataChanger.DateToStringLower(today, Format, myCulture);
            CurrentWeek = DataChanger.GetWeek(cal, Today);
            DayHours.AddHours(hours, Today);
            DayHours.ReadEventsFromFile(FirstPath);
            DayHours.ReadEventsFromFile(SecondPath);
        }

        private void GoForward()
        {
            Today = DataChanger.ChangeTodayForward(Today);
            Month = DataChanger.DateToStringLower(today, Format, myCulture);
            CurrentWeek = DataChanger.GetWeek(cal,Today);
            DayHours.AddHours(hours, Today);
            DayHours.ReadEventsFromFile(FirstPath);
            DayHours.ReadEventsFromFile(SecondPath);
        }

    }
}
