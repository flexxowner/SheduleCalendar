using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DayOfWeek = CalendarAppointments.Models.Models.DayOfWeek;

namespace CalendarAppointments.ViewModel.Service
{
    public static class DayOfWeekService
    {
        public static ObservableCollection<string> DaysOfWeek { get; set; }

        public static ObservableCollection<string> GetListOfWeekDays(int CurrentYear, int CurrentMonth)
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
                temp.Add(item);
            }

            DaysOfWeek = temp;
            return DaysOfWeek;
        }

        public static ObservableCollection<string> GetListOfAbbreviatedWeekDays(int CurrentYear, int CurrentMonth)
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

            DaysOfWeek = temp;
            return DaysOfWeek;
        }

        public static void AddDaysOfWeek(ObservableCollection<DayOfWeek> daysOfWeeks, int CurrentYear, int CurrentMonth)
        {
            daysOfWeeks.Clear();
            GetListOfWeekDays(CurrentYear, CurrentMonth);
            for (int i = 0; i < DaysOfWeek.Count; i++)
            {
                daysOfWeeks.Add(new DayOfWeek() { WeekDay = DaysOfWeek[i] });
            }
        }
    }
}
