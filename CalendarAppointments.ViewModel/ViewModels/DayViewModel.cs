using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Windows.UI.Xaml;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class DayViewModel : ObservableObject
    {
        CultureInfo myCulture = new CultureInfo("en-US");
        Calendar cal = new CultureInfo("en-US").Calendar;
        public IEnumerable<string> Hours { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        private ObservableCollection<DaysOfWeek> daysOfWeeks;
        private ObservableCollection<CalendarDay> calendarDays;
        private ObservableCollection<Day> days;
        private DateTime today = DateTime.Now;
        private DateTime tomorrow;
        private int currentWeek;
        private int tomorrowWeek;
        public string month;
        public string tomorrowMonth;
        public DayViewModel()
        {
            daysOfWeeks = new ObservableCollection<DaysOfWeek>();
            calendarDays = new ObservableCollection<CalendarDay>();
            tomorrow = today.AddDays(1);
            currentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            tomorrowWeek = cal.GetWeekOfYear(today.AddDays(1), CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            month = today.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            tomorrowMonth = tomorrow.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            days = new ObservableCollection<Day>()
            {
                new Day() {Today = today, Tomorrow = tomorrow, CurrentWeek = currentWeek, TomorrowWeek = tomorrowWeek, Month = month, TomorrowMonth = tomorrowMonth}
            };
            Hours = Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i)).ToString("H:mm"));
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
        }

        public ObservableCollection<DaysOfWeek> DaysOfWeeks
        {
            get { return daysOfWeeks; }
            set { daysOfWeeks = value; }
        }
        public ObservableCollection<CalendarDay> CalendarDays
        {
            get { return calendarDays; }
            set { calendarDays = value; }
        }

        public ObservableCollection<Day> Days
        {
            get { return days; }
            set { days = value; }
        }
        public DateTime Today
        {
            get { return today; }
            set { today = value; }
        }

        private void GoBack()
        {
            today = today.AddDays(-1);
            tomorrow = tomorrow.AddDays(-1);
            month = today.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            tomorrowMonth = tomorrow.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            currentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            tomorrowWeek = cal.GetWeekOfYear(tomorrow, CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            for (int i = 0; i < days.Count; i++)
            {
                days[i] = new Day() { CurrentWeek = currentWeek, Today = today, Tomorrow = tomorrow, Month = month, TomorrowMonth = tomorrowMonth, TomorrowWeek = tomorrowWeek };
            }
        }

        private void GoForward()
        {
            today = today.AddDays(1);
            tomorrow = tomorrow.AddDays(1);
            month = today.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            tomorrowMonth = tomorrow.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            currentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            tomorrowWeek = cal.GetWeekOfYear(tomorrow, CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            for (int i = 0; i < days.Count; i++)
            {
                days[i] = new Day() { CurrentWeek = currentWeek, Today = today, Tomorrow = tomorrow, Month = month, TomorrowMonth = tomorrowMonth, TomorrowWeek = tomorrowWeek };
            }
        }
    }
}
