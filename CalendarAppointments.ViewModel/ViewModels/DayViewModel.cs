using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Extensions;
using Helpers.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Windows.Storage;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class DayViewModel : ObservableObject, INotifyPropertyChanged
    {
        CultureInfo myCulture = new CultureInfo("en-US");
        Calendar cal = new CultureInfo("en-US").Calendar;
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        private const string firstPath = "Appointments.xml";
        private const string secondPath = "outlook.xml";
        private List<DateTime> hours { get; set; }
        private ObservableCollection<DaysOfWeek> daysOfWeeks;
        private ObservableCollection<Day> days;
        private ObservableCollection<Event> events;
        private DateTime today = DateTime.Now;
        private DateTime tomorrow;
        private int currentWeek;
        private int tomorrowWeek;
        private string month;
        private string tomorrowMonth;
        public DayViewModel()
        {
            daysOfWeeks = new ObservableCollection<DaysOfWeek>();
            events = new ObservableCollection<Event>();
            days = new ObservableCollection<Day>();
            hours = (List<DateTime>)Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i))).ToList();
            tomorrow = today.AddDays(1);
            currentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            tomorrowWeek = cal.GetWeekOfYear(today.AddDays(1), CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            month = today.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            tomorrowMonth = tomorrow.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            Days.AddHours(hours, Today, Tomorrow);
            Days.ReadFromFile(firstPath, Today, Tomorrow);
            Days.ReadFromFile(secondPath, Today, Tomorrow);
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
        }

        public ObservableCollection<DaysOfWeek> DaysOfWeeks
        {
            get { return daysOfWeeks; }
            set { daysOfWeeks = value; }
        }
        public ObservableCollection<Day> Days
        {
            get { return days; }
            set { days = value; }
        }
        public DateTime Today
        {
            get { return today; }
            set {
                today = value;
                OnPropertyChanged();
            }
        }
        public DateTime Tomorrow
        {
            get { return tomorrow; }
            set 
            { 
                tomorrow = value;
                OnPropertyChanged();
            }
        }
        public int CurrentWeek
        {
            get { return currentWeek;}
            set
            { 
                currentWeek = value;
                OnPropertyChanged();
            }
        }
        public int TomorrowWeek
        {
            get { return tomorrowWeek;}
            set
            { 
                tomorrowWeek = value;
                OnPropertyChanged();
            }
        }
        public string TomorrowMonth
        {
            get { return tomorrowMonth;}
            set
            {
                tomorrowMonth = value;
                OnPropertyChanged();
            }
        }
        public string Month
        {
            get { return month; }
            set 
            { 
                month = value;
                OnPropertyChanged();
            }
        }
        private void GoBack()
        {
            Today = today.AddDays(-1);
            Tomorrow = tomorrow.AddDays(-1);
            Month = today.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            TomorrowMonth = tomorrow.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            CurrentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            TomorrowWeek = cal.GetWeekOfYear(tomorrow, CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            Days.AddHours(hours, Today, Tomorrow);
            Days.ReadFromFile(firstPath, Today, Tomorrow);
            Days.ReadFromFile(secondPath, Today, Tomorrow);
        }
        private void GoForward()
        {
            Today = today.AddDays(1);
            Tomorrow = tomorrow.AddDays(1);
            Month = today.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            TomorrowMonth = tomorrow.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            CurrentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            TomorrowWeek = cal.GetWeekOfYear(tomorrow, CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            Days.AddHours(hours, Today, Tomorrow);
            Days.ReadFromFile(firstPath, Today, Tomorrow);
            Days.ReadFromFile(secondPath, Today, Tomorrow);
        }

    }
}
