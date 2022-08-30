using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class YearViewModel : ObservableObject
    {
        private ObservableCollection<Year> years;
        private DateTime currentDate;
        private static ObservableCollection<string> firstDayOfWeek;
        public YearViewModel()
        {
            currentDate = DateTime.Today;
            years = new ObservableCollection<Year>();
            AddDays();
        }

        public ObservableCollection<Year> Years
        {
            get { return years; }
            set { years = value; }
        }

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; }
        }
        public void AddDays()
        {
            for (int i = 1; i < 13; i++)
            {
                var month = new DateTime(currentDate.Year, i, 1);
                var days = new ObservableCollection<MonthDay>();
                for (int j = 1; j < DateTime.DaysInMonth(currentDate.Year, month.Month) + 1; j++)
                {
                    days.Add(new MonthDay() { Date = new DateTime(currentDate.Year, i, j) });
                }
                GetListOfWeekDays(currentDate.Year, i);
                years.Add(new Year() { Month = month.ToString("MMMM"), DaysOfWeek = firstDayOfWeek, CalendarDays = days });
            }
        }

        private ObservableCollection<string> GetListOfWeekDays(int CurrentYear, int CurrentMonth)
        {
            var firstDayOfMonth = new DateTime(CurrentYear, CurrentMonth, 1);
            var startDate = firstDayOfMonth;
            var endDate = startDate.AddDays(7);
            var numDays = (int)((endDate - startDate).TotalDays);
            List<DateTime> myDates = Enumerable
                       .Range(0, numDays)
                       .Select(x => startDate.AddDays(x))
                       .ToList();
            var observableDateList = myDates.Select(d => d.DayOfWeek.ToString()).ToList();
            var temp = new ObservableCollection<string>();

            foreach (var item in observableDateList)
            {
                var abr = item.Length > 3 ? item.Remove(3) : item;
                temp.Add(abr);
            }

            firstDayOfWeek = temp;
            return firstDayOfWeek;
        }
    }
}
