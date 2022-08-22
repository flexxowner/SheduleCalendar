using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using CalendarAppointments.Models.Models;
using Helpers.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class MonthViewModel : ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        public RelayCommand AddEventCommand { get; set; }
        private MonthDay selectedDay;
        private DateTimeOffset today = DateTimeOffset.Now;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private ObservableCollection<CalendarMonth> months;
        private ObservableCollection<DaysOfWeek> daysOfWeeks;
        private ObservableCollection<string> firstDayOfWeek;
        private ObservableCollection<Event> events;
        private ObservableCollection<MonthDay> calendarDays;
        private int currentMonth;
        private int currentYear;
        private string eventSubject;
        private bool isOpen;
        public MonthViewModel()
        {
            months = new ObservableCollection<CalendarMonth>()
            {
                new CalendarMonth() { Month = today.ToString("MMMM"), Year = today.Year}
            };
            daysOfWeeks = new ObservableCollection<DaysOfWeek>();
            calendarDays = new ObservableCollection<MonthDay>();
            events = new ObservableCollection<Event>();
            CurrentMonth = today.Month;
            CurrentYear = today.Year;
            AddDaysOfMonth();
            GetListOfWeekDays();
            AddDaysOfWeek();
            ReadFromFile();
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
            AddEventCommand = new RelayCommand(AddEvent);
        }

        public DateTimeOffset Today
        {
            get { return today; }
            set { SetProperty(ref today, value); }
        }
        public ObservableCollection<string> FirstDayOfWeek
        {
            get { return firstDayOfWeek; }
            set { firstDayOfWeek = value; }
        }
        public int CurrentMonth
        {
            get { return currentMonth; }
            set { currentMonth = value; }
        }
        public int CurrentYear
        {
            get { return currentYear; }
            set { currentYear = value; }
        }
        public ObservableCollection<CalendarMonth> Months
        {
            get { return months; }
            set { months = value; }
        }
        public ObservableCollection<DaysOfWeek> DaysOfWeeks
        { 
            get { return daysOfWeeks; } 
            set { daysOfWeeks = value; }
        }
        public ObservableCollection<MonthDay> CalendarDays
        {
            get { return calendarDays; }
            set { calendarDays = value; }
        }
        public ObservableCollection<Event> Events
        {
            get { return events; }
            set { events = value; }
        }

        public MonthDay SelectedDay
        {
            get { return selectedDay; }
            set { selectedDay = value; }
        }
        public string EventSubject
        {
            get { return eventSubject; }
            set { eventSubject = value; }
        }
        public TimeSpan StartTime
        {
            get { return startTime; }
            set
            { 
                startTime = value;
                OnPropertyChanged("StartTime");
            }
        }
        public TimeSpan EndTime
        {
            get { return endTime; }
            set
            { 
                endTime = value;
                OnPropertyChanged("EndTime");
            }
        }
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                if (isOpen == value) return;
                isOpen = value;
                OnPropertyChanged("IsOpen");
            }
        }
        private void AddDaysOfMonth()
        {
            calendarDays.Clear();

            for (int i = 1; i < DateTime.DaysInMonth(CurrentYear, CurrentMonth) + 1; i++)
            {
                calendarDays.Add(new MonthDay { Date = new DateTime(CurrentYear, CurrentMonth, i) });
            }
        }
        private ObservableCollection<string> GetListOfWeekDays()
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
        private void AddDaysOfWeek()
        {
            daysOfWeeks.Clear();

            for (int i = 0; i < FirstDayOfWeek.Count; i++)
            {
                daysOfWeeks.Add(new DaysOfWeek() { DayOfWeek = FirstDayOfWeek[i] });
            }

        }
        private void GoBack()
        {
            CurrentMonth = CurrentMonth - 1;

            if (CurrentMonth > 12)
            {
                CurrentYear = CurrentYear + 1;
                CurrentMonth = 1;
            }
            else if (CurrentMonth < 1)
            {
                CurrentYear = CurrentYear - 1;
                CurrentMonth = 12;
            }

            FirstDayOfWeek.Clear();
            GetListOfWeekDays();
            AddDaysOfWeek();
            AddDaysOfMonth();
            ReadFromFile();
            SaveEvent();
            today = new DateTime(CurrentYear, CurrentMonth, 1);

            for (int i = 0; i < months.Count; i++)
            {
                months[i] = new CalendarMonth() { Month = today.ToString("MMMM"), Year = today.Year };
            }

        }
        private void GoForward()
        {
            CurrentMonth = CurrentMonth + 1;

            if (CurrentMonth > 12)
            {
                CurrentYear = CurrentYear + 1;
                CurrentMonth = 1;
            }
            else if (CurrentMonth < 1)
            {
                CurrentYear = CurrentYear - 1;
                CurrentMonth = 12;
            }

            FirstDayOfWeek.Clear();
            GetListOfWeekDays();
            AddDaysOfWeek();
            AddDaysOfMonth();
            SaveEvent();
            ReadFromFile();
            today = new DateTime(CurrentYear, CurrentMonth, 1);

            for (int i = 0; i < months.Count; i++)
            {
                months[i] = new CalendarMonth() { Month = today.ToString("MMMM"), Year = today.Year };
            }
        }
        public void HandleDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            IsOpen = true;
            isOpen = false;
        }
        private void AddEvent()
        {
            if (EndTime > StartTime)
            {
                events.Add(new Event() { StartTime = StartTime, EndTime = EndTime, StartDate = SelectedDay.Date, Subject = eventSubject });
                SaveToFile();
                SaveEvent();
            }
            else if (eventSubject == null)
            {
                DialogHelper.SubjectEmptyWarning();
            }
            else if(EndTime <= StartTime)
            {
                DialogHelper.WarningDialog();
            }
        }
        private void SaveEvent()
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
        private void ShowEvents()
        {
            foreach (var e in events)
            {
                foreach (var day in calendarDays)
                {
                    if (e.StartDate == day.Date)
                    {
                        day.Events.Add(e);
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
            if (localFile != null )
            {
                string localData = await FileIO.ReadTextAsync(localFile);

                events = ObjectSerializer<ObservableCollection<Event>>.FromXml(localData);
                ShowEvents();
            }
        }
    }
}
