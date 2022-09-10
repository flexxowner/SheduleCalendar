using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Extensions;
using Helpers.Helpers;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Storage;
using Event = CalendarAppointments.Models.Models.Event;

namespace CalendarAppointments.ViewModel.Service
{
    public static class EventService 
    {
        public static void SaveToSelectedDay(ObservableCollection<Event> events, MonthDay SelectedDay, ObservableCollection<MonthDay> calendarDays)
        {
            try
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
            catch (Exception e)
            {
                DialogHelper.ErrorDialog(e);
            }
        }

        public static void AddEventToSelectedDay(
            ObservableCollection<Event> Events, ObservableCollection<MonthDay> calendarDays, TimeSpan StartTime, TimeSpan EndTime, string eventSubject, MonthDay SelectedDay, string Path )
        {
            if (EndTime > StartTime)
            {
                Events.Add(new Event() { StartTime = StartTime, EndTime = EndTime, StartDate = SelectedDay.Date, Subject = eventSubject });
                FileManager.WriteToFile(Events, Path);
                Events.SaveToSelectedDay(SelectedDay, calendarDays);
            }
            else if (eventSubject == null)
            {
                DialogHelper.SubjectEmptyWarning();
            }
            else if (EndTime <= StartTime)
            {
                DialogHelper.WarningDialog();
            }
        }

        public static void SearchEvent(ObservableCollection<Event> FoundEvents, string Title, ObservableCollection<Event> events)
        {
            if (Title != "")
            {
                var _events = events.Where(x => x.Subject.Contains(Title));
                foreach (var item in _events)
                {
                    if (item != null)
                    {
                        FoundEvents.Add(item);
                    }
                }
            }
        }

        public static async void ReadEventsFromFile(string path, ObservableCollection<MonthDay> dates)
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
                AddMonthEvents(events, dates);
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
                AddYearEvents(events, years);
            }
        }

        public static async void ReadEventsFromFile(string path, ObservableCollection<Event> Events)
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

                foreach (var item in events)
                {
                    Events.Add(item);
                }
            }
        }

        public static async void ReadEventsFromFile(ObservableCollection<Event> events, ObservableCollection<DayHour> hours, string path)
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

                var data = ObjectSerializer<ObservableCollection<Event>>.FromXml(localData);
                AddHourEvents(data, hours);
            }
        }

        private static void AddYearEvents(ObservableCollection<Event> events, ObservableCollection<Year> years)
        {
            foreach (var e in events)
            {
                foreach (var item in years)
                {
                    var days = from d in item.CalendarDays
                               where d.Date.Date == e.StartDate.Date
                               select d;
                    ColorService.AddColor(days);
                }
            }
        }

        private static void AddMonthEvents(ObservableCollection<Event> events, ObservableCollection<MonthDay> calendarDays)
        {
            foreach (var day in calendarDays)
            {
                foreach (var e in events)
                {
                    if (e.StartDate.Date == day.Date.Date)
                    {
                        day.Events.Add(e);
                    }
                }
            }
        }

        private static void AddHourEvents(ObservableCollection<Event> events, ObservableCollection<DayHour> hours)
        {
            foreach (var hour in hours)
            {
                foreach (var e in events)
                {
                    if (e.StartTime.Hours == hour.Hour.Hour && e.StartDate.Date == hour.Date.Date)
                    {
                        hour.Events.Add(e);
                    }
                }
            }
        }
    }
}
