using System;
using System.Collections.ObjectModel;
using System.Globalization;
using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Extensions;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using DayOfWeek = CalendarAppointments.Models.Models.DayOfWeek;
using Windows.UI.Xaml.Input;
using CalendarAppointments.ViewModel.Service;
using System.ComponentModel;
using CalendarAppointments.ViewModel.Services;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class MonthViewModel : ObservableObject, INotifyPropertyChanged
    {
        private const string FirstPath = "Appointments.xml";
        private const string SecondPath = "outlook.xml";
        private const string Format = "MMMM";
        private const int Max = 12;
        private const int Min = 1;
        private const string Culture = "en";
        private readonly string month;
        private readonly ObservableCollection<DayOfWeek> daysOfWeeks;
        private readonly ObservableCollection<Event> events;
        private readonly ObservableCollection<MonthDay> calendarDays;
        private readonly ObservableCollection<CalendarMonth> months;
        private MonthDay selectedDay;
        private DateTimeOffset today = DateTimeOffset.Now;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private int currentMonth;
        private int currentYear;
        private string eventSubject;
        private bool isOpen;

        public MonthViewModel()
        {
            month = today.ToString(Format, CultureInfo.CreateSpecificCulture(Culture)).ToUpper();
            months = new ObservableCollection<CalendarMonth>()
            {
                new CalendarMonth() { Month = month, Year = today.Year}
            };
            daysOfWeeks = new ObservableCollection<DayOfWeek>();
            calendarDays = new ObservableCollection<MonthDay>();
            events = new ObservableCollection<Event>();
            CurrentMonth = today.Month;
            CurrentYear = today.Year;
            CalendarDays.AddDaysOfMonth(CurrentYear, CurrentMonth);
            DaysOfWeeks.AddDaysOfWeek(CurrentYear, CurrentMonth);
            EventService.ReadEventsFromFile(FirstPath, CalendarDays);
            EventService.ReadEventsFromFile(SecondPath, CalendarDays);
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
            AddEventCommand = new RelayCommand(AddEventToSelectedDay);
        }

        public void HandleDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            IsOpen = true;
            isOpen = false;
        }

        public RelayCommand GoBackCommand { get; set; }

        public RelayCommand GoForwardCommand { get; set; }

        public RelayCommand AddEventCommand { get; set; }

        public DateTimeOffset Today
        {
            get => today;
            set { SetProperty(ref today, value); }
        }

        public int CurrentMonth
        {
            get => currentMonth;
            set { currentMonth = value; }
        }

        public int CurrentYear
        {
            get => currentYear;
            set { currentYear = value; }
        }

        public ObservableCollection<CalendarMonth> Months
        {
            get => months;
        }

        public ObservableCollection<DayOfWeek> DaysOfWeeks
        {
            get => daysOfWeeks;
        }

        public ObservableCollection<MonthDay> CalendarDays
        {
            get => calendarDays;
        }

        public ObservableCollection<Event> Events
        {
            get => events;
        }

        public MonthDay SelectedDay
        {
            get => selectedDay;
            set { selectedDay = value; }
        }

        public string EventSubject
        {
            get => eventSubject;
            set
            {
                eventSubject = value;
                OnPropertyChanged(nameof(EventSubject));
            }
        }

        public TimeSpan StartTime
        {
            get => startTime;
            set
            {
                startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        public TimeSpan EndTime
        {
            get => endTime;
            set
            {
                endTime = value;
                OnPropertyChanged(nameof(EndTime));
            }
        }

        public bool IsOpen
        {
            get => isOpen;
            set
            {
                if (isOpen == value) return;
                isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }

        private void GoBack()
        {
            DataChanger.ChangeMonthBack(ref currentMonth, Max, Min, ref currentYear);
            DaysOfWeeks.Clear();
            DaysOfWeeks.AddDaysOfWeek(CurrentYear, CurrentMonth);
            CalendarDays.AddDaysOfMonth(CurrentYear, CurrentMonth);
            Today = new DateTime(CurrentYear, CurrentMonth, Min);
            Months.UpdateMonths(Today, Format, Culture);
            EventService.ReadEventsFromFile(FirstPath, CalendarDays);
            EventService.ReadEventsFromFile(SecondPath, CalendarDays);
        }

        private void GoForward()
        {
            DataChanger.ChangeMonthForward(ref currentMonth, Max, Min, ref currentYear);
            DaysOfWeeks.Clear();
            DaysOfWeeks.AddDaysOfWeek(CurrentYear, CurrentMonth);
            CalendarDays.AddDaysOfMonth(CurrentYear, CurrentMonth);
            Today = new DateTime(CurrentYear, CurrentMonth, Min);
            Months.UpdateMonths(Today, Format, Culture);
            EventService.ReadEventsFromFile(FirstPath, CalendarDays);
            EventService.ReadEventsFromFile(SecondPath, CalendarDays);
        }

        private void AddEventToSelectedDay()
        {
            EventService.AddEventToSelectedDay(Events, CalendarDays, StartTime, EndTime, EventSubject, SelectedDay, FirstPath);
        }
    }
}
