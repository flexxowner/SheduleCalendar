using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class WeekViewModel : ObservableObject
    {
        Calendar cal = new CultureInfo("en-US").Calendar;
        public IEnumerable<string> Hours { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        private ObservableCollection<Week> weeks { get; set; }
        private DateTime today;
        private int currentWeek;
        private string month;

        public WeekViewModel()
        {
            today = DateTime.Now;
            currentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            weeks = new ObservableCollection<Week>();
            AddWeekDays();
        }
        public ObservableCollection<Week> Weeks
        {
            get { return weeks; }
            set { weeks = value; }
        }
        private void AddWeekDays()
        {
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }
            var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
            foreach(var date in dates)
            {
                weeks.Add(new Week() { WeekNumber = currentWeek, Today = date.Date, DayOfWeek = date.DayOfWeek, Hours = Hours });
            }
        }
    }
}
