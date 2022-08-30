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
        public static void AddHours(this ObservableCollection<DayHour> days, List<DateTime> hours, DateTime Today, DateTime Tomorrow)
        {
            foreach (var hour in hours)
            {
                days.Add(new DayHour() { Hour = hour, Date = Today });
            }
        }
        public static void AddEvents(ObservableCollection<Event> events,ObservableCollection<DayHour> days, DateTime Today, DateTime Tomorrow)
        {
            var hours = (List<DateTime>)Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i))).ToList();
            foreach (var day in days)
            {
                foreach (var e in events)
                {
                    if (e.StartTime.Hours == day.Hour.Hour && e.StartDate.Date == day.Date)
                    {
                        day.Events.Add(e);
                    }
                }
            }
        }
        public static async void ReadFromFile(this ObservableCollection<DayHour> days, string path, DateTime Today, DateTime Tomorrow)
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
