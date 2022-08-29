using CalendarAppointments.Models.Models;
using Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace CalendarAppointments.ViewModel.Extensions
{
    public static class DayViewModelExtension
    {
        public static void AddHours(this ObservableCollection<Day> days, List<DateTime> hours, DateTime Today, DateTime Tomorrow)
        {
            days.Clear();
            for (int j = 0; j < hours.Count; j++)
            {
                days.Add(new Day() { Hour = hours[j], Today = Today, Tomorrow = Tomorrow });
            }
        }
        public static void AddEvents(this ObservableCollection<Event> events,ObservableCollection<Day> days, DateTime Today, DateTime Tomorrow)
        {
            foreach (var day in days)
            {
                foreach (var e in events)
                {
                    if ((e.StartDate.Date == day.Today.Date || e.StartDate.Date == day.Tomorrow.Date) && !day.Events.Contains(e))
                    {
                        day.Events.Add(e);
                    }
                }
            }
        }
        public static async void ReadFromFile(this ObservableCollection<Day> days, string path, DateTime Today, DateTime Tomorrow)
        {
            StorageFile localFile;
            try
            {
                localFile = await ApplicationData.Current.LocalFolder.GetFileAsync("path");
            }
            catch (FileNotFoundException ex)
            {
                localFile = null;
            }
            if (localFile != null)
            {
                string localData = await FileIO.ReadTextAsync(localFile);

                var events = ObjectSerializer<ObservableCollection<Event>>.FromXml(localData);
                AddEvents(events, days, Today, Tomorrow);
            }
        }
    }
}
