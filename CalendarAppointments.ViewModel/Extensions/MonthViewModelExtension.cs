using CalendarAppointments.Models.Models;
using Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace CalendarAppointments.ViewModel.Extensions
{
    public static class MonthViewModelExtension
    {
        public static ObservableCollection<string> FirstDayOfWeek;
        private static TimeSpan timer = new TimeSpan(0, 0, 1);
        public static void AddDaysOfMonth(ObservableCollection<MonthDay> calendarDays, int CurrentYear, int CurrentMonth)
        {
            calendarDays.Clear();

            for (int i = 1; i < DateTime.DaysInMonth(CurrentYear, CurrentMonth) + 1; i++)
            {
                calendarDays.Add(new MonthDay { Date = new DateTime(CurrentYear, CurrentMonth, i) });
            }
        }

        public static void UpdateMonths(ObservableCollection<CalendarMonth> months, DateTimeOffset today)
        {
            for (int i = 0; i < months.Count; i++)
            {
                months[i] = new CalendarMonth() { Month = today.ToString("MMMM"), Year = today.Year };
            }
        }
        public static void AddDaysOfWeek(ObservableCollection<DaysOfWeek> daysOfWeeks, int CurrentYear, int CurrentMonth)
        {
            daysOfWeeks.Clear();
            GetListOfWeekDays(CurrentYear, CurrentMonth);
            for (int i = 0; i < FirstDayOfWeek.Count; i++)
            {
                daysOfWeeks.Add(new DaysOfWeek() { DayOfWeek = FirstDayOfWeek[i] });
            }
        }

        public static void SaveNewEvent(ObservableCollection<Event> events, MonthDay SelectedDay, ObservableCollection<MonthDay> calendarDays )
        {
            if (events != null && SelectedDay != null)
            {
                var filterDays = from day in calendarDays
                                 where day.Date == SelectedDay.Date
                                 select day;
                var filterEvents = from e in events
                                   where e.StartDate == SelectedDay.Date && !SelectedDay.Events.Contains(e)
                                   select e;
                foreach (var day in filterDays)
                {
                    foreach (var e in filterEvents)
                    {
                        day.Events.Add(e);
                    }
                }
            }
        }

        public static void AddEvents(ObservableCollection<Event> events, ObservableCollection<MonthDay> calendarDays)
        {
            foreach (var day in calendarDays)
            {
                foreach (var e in events)
                {
                    if (e.StartDate.Date == day.Date.Date && !day.Events.Contains(e))
                    {
                        day.Events.Add(e);
                    }
                }
            }
        }

        public static async void ReadEventsFromFile(string path, ObservableCollection<MonthDay> calendarDays)
        {
            StorageFile localFile;
            try
            {
                localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(path);
            }
            catch (FileNotFoundException ex)
            {
                localFile = null;
            }
            if (localFile != null)
            {
                string localData = await FileIO.ReadTextAsync(localFile);

                var events = ObjectSerializer<ObservableCollection<Event>>.FromXml(localData);
                AddEvents(events, calendarDays);
            }
        }

        private static ObservableCollection<string> GetListOfWeekDays(int CurrentYear, int CurrentMonth)
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

            FirstDayOfWeek = temp;
            return FirstDayOfWeek;
        }
    }
}
