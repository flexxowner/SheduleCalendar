using CalendarAppointments.Models.Models;
using Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CalendarAppointments.ViewModel.Extensions
{
    public static class YearViewModelExtension
    {
        private static ObservableCollection<string> daysOfWeek;
        private static Brush greenColor = new SolidColorBrush(Colors.Green);

        public static void AddDays(this ObservableCollection<Year> years, int Year, string path, string secondPath)
        {
            for (int i = 1; i < 13; i++)
            {
                var month = new DateTime(Year, i, 1);
                var days = new ObservableCollection<YearDay>();
                for (int j = 1; j < DateTime.DaysInMonth(Year, month.Month) + 1; j++)
                {
                    days.Add(new YearDay() { Date = new DateTime(Year, i, j) });
                }
                GetListOfWeekDays(Year, i);
                years.Add(new Year() { Month = month.ToString("MMMM"), DaysOfWeek = daysOfWeek, CalendarDays = days });
                ReadEventsFromFile(path, years);
                ReadEventsFromFile(secondPath, years);
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
                var abr = item.Length > 3 ? item.Remove(3) : item;
                temp.Add(abr);
            }

            daysOfWeek = temp;
            return daysOfWeek;
        }

        private static void AddEvents(ObservableCollection<Event> events, ObservableCollection<Year> years)
        {
            foreach (var e in events)
            {
                foreach (var item in years)
                {
                    var days = from d in item.CalendarDays
                               where d.Date.Date == e.StartDate.Date
                               select d;
                    AddColor(days);
                }
            }
        }
        private static void AddColor(IEnumerable<YearDay> days)
        {
            foreach (var item in days)
            {
                item.Color = greenColor;
            }
        }
        public static async void ReadEventsFromFile(string path, ObservableCollection<Year> years)
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
                AddEvents(events, years);
            }
        }
    }
}
