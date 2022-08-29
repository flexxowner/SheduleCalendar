using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Extensions;
using Helpers.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Windows.Storage;
using Windows.UI.Xaml.Input;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class MonthViewModel : ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        public RelayCommand AddEventCommand { get; set; }
        private const string firstPath = "Appointments.xml";
        private const string secondPath = "outlook.xml";
        private MonthDay selectedDay;
        private DateTimeOffset today = DateTimeOffset.Now;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private ObservableCollection<CalendarMonth> months;
        private ObservableCollection<DaysOfWeek> daysOfWeeks;
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
            CalendarDays.AddDaysOfMonth(CurrentYear, CurrentMonth);
            DaysOfWeeks.AddDaysOfWeek(CurrentYear, CurrentMonth);
            CalendarDays.ReadEventsFromFile(firstPath);
            CalendarDays.ReadEventsFromFile(secondPath);
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
            AddEventCommand = new RelayCommand(AddEventToSelectedDay);
        }

        public void HandleDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            IsOpen = true;
            isOpen = false;
        }

        public DateTimeOffset Today
        {
            get { return today; }
            set { SetProperty(ref today, value); }
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

            MonthViewModelExtension.FirstDayOfWeek.Clear();
            DaysOfWeeks.AddDaysOfWeek(CurrentYear, CurrentMonth);
            CalendarDays.AddDaysOfMonth(CurrentYear, CurrentMonth);
            CalendarDays.ReadEventsFromFile(firstPath);
            CalendarDays.ReadEventsFromFile(secondPath);
            today = new DateTime(CurrentYear, CurrentMonth, 1);
            Months.UpdateMonths(Today);
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

            MonthViewModelExtension.FirstDayOfWeek.Clear();
            DaysOfWeeks.AddDaysOfWeek(CurrentYear, CurrentMonth);
            CalendarDays.AddDaysOfMonth(CurrentYear, CurrentMonth);
            CalendarDays.ReadEventsFromFile(firstPath);
            CalendarDays.ReadEventsFromFile(secondPath);
            today = new DateTime(CurrentYear, CurrentMonth, 1);
            Months.UpdateMonths(Today);
        }

        private void AddEventToSelectedDay()
        {
            if (EndTime > StartTime)
            {
                events.Add(new Event() { StartTime = StartTime, EndTime = EndTime, StartDate = SelectedDay.Date, Subject = eventSubject });
                FileManager.SaveToFile(events, firstPath);
                Events.SaveNewEvent(SelectedDay, calendarDays);
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
    }
}
