using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using CalendarAppointments.Models.Models;
using Helpers.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Windows.Storage;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class DayViewModel : ObservableObject
    {
        CultureInfo myCulture = new CultureInfo("en-US");
        Calendar cal = new CultureInfo("en-US").Calendar;
        private List<DateTime> hours { get; set; }
        private List<DateTime> minutes { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        private ObservableCollection<DaysOfWeek> daysOfWeeks;
        private ObservableCollection<Day> days;
        private ObservableCollection<Event> events;
        private ObservableCollection<DayHours> dayHours;
        private DateTime today = DateTime.Now;
        private DateTime tomorrow;
        private int currentWeek;
        private int tomorrowWeek;
        private string month;
        private string tomorrowMonth;
        private int interval = 1;
        public DayViewModel()
        {
            daysOfWeeks = new ObservableCollection<DaysOfWeek>();
            events = new ObservableCollection<Event>();
            days = new ObservableCollection<Day>();
            dayHours = new ObservableCollection<DayHours>();
            tomorrow = today.AddDays(1);
            currentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            tomorrowWeek = cal.GetWeekOfYear(today.AddDays(1), CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            month = today.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            tomorrowMonth = tomorrow.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            days = new ObservableCollection<Day>()
            {
                new Day() {Today = today, Tomorrow = tomorrow, CurrentWeek = currentWeek, TomorrowWeek = tomorrowWeek, Month = month, TomorrowMonth = tomorrowMonth}
            };
            AddHours();
            ReadFromFile();
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
        public ObservableCollection<DayHours> DayHours
        {
            get { return dayHours; }
            set { dayHours = value; }
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
            ReadFromFile();
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
            ReadFromFile();
            for (int i = 0; i < days.Count; i++)
            {
                days[i] = new Day() { CurrentWeek = currentWeek, Today = today, Tomorrow = tomorrow, Month = month, TomorrowMonth = tomorrowMonth, TomorrowWeek = tomorrowWeek };
            }
        }
        private void AddHours()
        {
            hours = (List<DateTime>)Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i))).ToList();
            for (int i = 0; i < hours.Count; i++)
            {
                dayHours.Add(new DayHours() { Hour = hours[i]});
            }
        }
        private void AddEvents()
        {
            foreach (var item in dayHours)
            {
                foreach (var e in events)
                {
                    if (item.Hour.TimeOfDay == e.StartTime && (e.StartDate == today || e.StartDate == tomorrow))
                    {
                        item.Events.Add(e);
                    }
                }
            }
        }
        private async void SaveToFile()
        {
            string rootFrameDataString = ObjectSerializer<ObservableCollection<Event>>.ToXml(events);
            if (!string.IsNullOrEmpty(rootFrameDataString))
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("events.xml", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(localFile, rootFrameDataString);
            }
        }
        private async void ReadFromFile()
        {
            StorageFile localFile;
            try
            {
                localFile = await ApplicationData.Current.LocalFolder.GetFileAsync("events.xml");
            }
            catch (FileNotFoundException ex)
            {
                localFile = null;
            }
            if (localFile != null)
            {
                string localData = await FileIO.ReadTextAsync(localFile);

                events = ObjectSerializer<ObservableCollection<Event>>.FromXml(localData);
                AddEvents();
            }
        }
    }
}
