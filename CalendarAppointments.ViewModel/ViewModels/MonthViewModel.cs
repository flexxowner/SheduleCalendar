using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.DialogPage;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class MonthViewModel : ObservableObject
    {
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        public RelayCommand OpenDialogCommand { get; set; }
        public RelayCommand SaveEventCommand { get; set; }
        public int SelectedItem { get; set; }
        private DateTimeOffset today = DateTimeOffset.Now;
        private ObservableCollection<CalendarMonth> months;
        private ObservableCollection<DaysOfWeek> daysOfWeeks;
        private ObservableCollection<string> firstDayOfWeek;
        private ObservableCollection<Event> events;
        private ObservableCollection<CalendarDay> calendarDays;
        private int currentMonth;
        private int currentYear;
        private string eventContent;

        public MonthViewModel()
        {
            months = new ObservableCollection<CalendarMonth>()
            {
                new CalendarMonth() { Month = today.ToString("MMMM"), Year = today.Year}
            };
            daysOfWeeks = new ObservableCollection<DaysOfWeek>();
            events = new ObservableCollection<Event>();
            calendarDays = new ObservableCollection<CalendarDay>();
            CurrentMonth = today.Month;
            CurrentYear = today.Year;
            EventContent = "Enter theme";
            AddDaysOfMonth();
            GetListOfWeekDays();
            AddDaysOfWeek();
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
            OpenDialogCommand = new RelayCommand(OpenDialog);
            SaveEventCommand = new RelayCommand(SaveEvent);
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

        public string EventContent
        {
            get { return eventContent; }
            set { eventContent = value; }
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

        public ObservableCollection<CalendarDay> CalendarDays
        {
            get { return calendarDays; }
            set { calendarDays = value; }
        }

        public ObservableCollection<Event> Events
        {
            get { return events; }
            set { events = value; }
        }

        private void AddDaysOfMonth()
        {
            calendarDays.Clear();

            for (int i = 1; i < DateTime.DaysInMonth(CurrentYear, CurrentMonth) + 1; i++)
            {
                calendarDays.Add(new CalendarDay { Number = i });
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
            today = new DateTime(CurrentYear, CurrentMonth, 1);

            for (int i = 0; i < months.Count; i++)
            {
                months[i] = new CalendarMonth() { Month = today.ToString("MMMM"), Year = today.Year };
            }
        }

        private async void OpenDialog()
        {
            ContentDialog dialog = new ContentDialog
            {
                Content = new EventPage(),
                CloseButtonText = "Close",
                SecondaryButtonCommand = SaveEventCommand,
                SecondaryButtonText = "Save"
            };
            await dialog.ShowAsync();
        }

        private void SaveEvent()
        {
            events.Add(new Event() { StartTime = today, Title = eventContent});

            for (int i = 0; i < calendarDays.Count; i++)
            {
                for (int j = 0; j < events.Count; j++)
                {
                    if (events[j].StartTime.Day == calendarDays[i].Number && events[j].StartTime.Month == CurrentMonth && events[j].StartTime.Year == CurrentYear)
                    {
                        CalendarDays[i].Events.Add(events[j]);
                    }
                }
            }


        }

    }
}
