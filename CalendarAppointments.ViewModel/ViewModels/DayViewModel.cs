﻿using System;
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
        private DateTime tomorrow;
        private int currentWeek;
        private int tomorrowWeek;
        private string month;
        private string tomorrowMonth;

        public DayViewModel()
        {
            daysOfWeeks = new ObservableCollection<DayOfWeek>();
            events = new ObservableCollection<Event>();
            dayHours = new ObservableCollection<DayHour>();
            hours = (List<DateTime>)Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i))).ToList();
            tomorrow = today.AddDays(1);
            currentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            tomorrowWeek = cal.GetWeekOfYear(today.AddDays(1), CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            month = today.ToString(Format, myCulture.DateTimeFormat).ToLower();
            tomorrowMonth = tomorrow.ToString(Format, myCulture.DateTimeFormat).ToLower();
            DayHours.AddHours(hours, Today, Tomorrow);
            Events.ReadEventsFromFile(DayHours,FirstPath, Today, Tomorrow);
            Events.ReadEventsFromFile(DayHours, SecondPath, Today, Tomorrow);
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

        public DateTime Tomorrow
        {
            get => tomorrow;
            set 
            { 
                tomorrow = value;
                OnPropertyChanged(nameof(Tomorrow));
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

        public int TomorrowWeek
        {
            get => tomorrowWeek;
            set
            { 
                tomorrowWeek = value;
                OnPropertyChanged(nameof(TomorrowWeek));
            }
        }

        public string TomorrowMonth
        {
            get => tomorrowMonth;
            set
            {
                tomorrowMonth = value;
                OnPropertyChanged(nameof(TomorrowMonth));
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
            Tomorrow = DataChanger.ChangeTomorrowBack(Tomorrow);
            Month = today.ToString(Format, myCulture.DateTimeFormat).ToLower();
            TomorrowMonth = tomorrow.ToString(Format, myCulture.DateTimeFormat).ToLower();
            CurrentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            TomorrowWeek = cal.GetWeekOfYear(tomorrow, CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            DayHours.AddHours(hours, Today, Tomorrow);
            Events.ReadEventsFromFile(DayHours, FirstPath, Today, Tomorrow);
            Events.ReadEventsFromFile(DayHours, SecondPath, Today, Tomorrow);
        }

        private void GoForward()
        {
            Today = DataChanger.ChangeTodayForward(Today);
            Tomorrow = DataChanger.ChangeTomorrowForward(Tomorrow);
            Month = today.ToString(Format, myCulture.DateTimeFormat).ToLower();
            TomorrowMonth = tomorrow.ToString(Format, myCulture.DateTimeFormat).ToLower();
            CurrentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            TomorrowWeek = cal.GetWeekOfYear(tomorrow, CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            DayHours.AddHours(hours, Today, Tomorrow);
            Events.ReadEventsFromFile(DayHours, FirstPath, Today, Tomorrow);
            Events.ReadEventsFromFile(DayHours, SecondPath, Today, Tomorrow);
        }

    }
}
