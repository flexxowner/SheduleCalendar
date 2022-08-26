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
    public class DayViewModel : ObservableObject, INotifyPropertyChanged
    {
        CultureInfo myCulture = new CultureInfo("en-US");
        Calendar cal = new CultureInfo("en-US").Calendar;
        private List<DateTime> hours { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        private ObservableCollection<DaysOfWeek> daysOfWeeks;
        private ObservableCollection<Day> days;
        private ObservableCollection<Event> events;
        private DateTime today = DateTime.Now;
        private DateTime tomorrow;
        private int currentWeek;
        private int tomorrowWeek;
        private string month;
        private string tomorrowMonth;
        public DayViewModel()
        {
            daysOfWeeks = new ObservableCollection<DaysOfWeek>();
            events = new ObservableCollection<Event>();
            days = new ObservableCollection<Day>();
            hours = (List<DateTime>)Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i))).ToList();
            tomorrow = today.AddDays(1);
            currentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            tomorrowWeek = cal.GetWeekOfYear(today.AddDays(1), CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            month = today.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            tomorrowMonth = tomorrow.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
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
        public DateTime Today
        {
            get { return today; }
            set {
                today = value;
                OnPropertyChanged();
            }
        }
        public DateTime Tomorrow
        {
            get { return tomorrow; }
            set 
            { 
                tomorrow = value;
                OnPropertyChanged();
            }
        }
        public int CurrentWeek
        {
            get { return currentWeek;}
            set
            { 
                currentWeek = value;
                OnPropertyChanged();
            }
        }
        public int TomorrowWeek
        {
            get { return tomorrowWeek;}
            set
            { 
                tomorrowWeek = value;
                OnPropertyChanged();
            }
        }
        public string TomorrowMonth
        {
            get { return tomorrowMonth;}
            set
            {
                tomorrowMonth = value;
                OnPropertyChanged();
            }
        }
        public string Month
        {
            get { return month; }
            set 
            { 
                month = value;
                OnPropertyChanged();
            }
        }
        private void GoBack()
        {
            Today = today.AddDays(-1);
            Tomorrow = tomorrow.AddDays(-1);
            Month = today.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            TomorrowMonth = tomorrow.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            CurrentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            TomorrowWeek = cal.GetWeekOfYear(tomorrow, CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            AddHours();
            ReadFromFile();
        }
        private void GoForward()
        {
            Today = today.AddDays(1);
            Tomorrow = tomorrow.AddDays(1);
            Month = today.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            TomorrowMonth = tomorrow.ToString("MMMM", myCulture.DateTimeFormat).ToLower();
            CurrentWeek = cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, today.DayOfWeek);
            TomorrowWeek = cal.GetWeekOfYear(tomorrow, CalendarWeekRule.FirstDay, tomorrow.DayOfWeek);
            AddHours();
            ReadFromFile();
        }
        private void AddHours()
        {
            for (int i = 0; i < days.Count; i++)
            {
                for (int j = 0; j < hours.Count; j++)
                {
                    days[i] = new Day() { Hour = hours[j], Today = Today, Tomorrow = Tomorrow };
                }
            }
        }
        private void AddEvents()
        {
            var filter = from e in events
                         where e.StartDate == Today || e.StartDate == Tomorrow
                         select e;
            foreach (var item in filter)
            {
                foreach (var day in days)
                {
                    if (day.Hour.Hour == item.StartTime.Hours )
                    {
                        day.Events.Add(item);
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
