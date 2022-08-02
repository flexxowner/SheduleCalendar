using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class MonthViewModel : ObservableObject
    {
        private readonly ObservableCollection<Event> events;
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand GoForwardCommand { get; set; }
        private DateTime today = DateTime.Now;
        private ObservableCollection<CalendarMonth> months;
        private ObservableCollection<CalendarDay> days;
        private ObservableCollection<DaysOfWeek> daysOfWeeks;
        private ObservableCollection<string> firstDayOfWeek;
        private int currentMonth;
        private int currentYear;

        public MonthViewModel()
        {
            months = new ObservableCollection<CalendarMonth>()
            {
                new CalendarMonth() { Month = today.ToString("MMMM"), Year = today.Year}
            };
            days = new ObservableCollection<CalendarDay>();
            daysOfWeeks = new ObservableCollection<DaysOfWeek>();
            CurrentMonth = today.Month;
            CurrentYear = today.Year;
            AddDaysOfMonth();
            GetListOfWeekDays();
            AddDaysOfWeek();
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);

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

        public ObservableCollection<CalendarDay> Days 
        { 
            get { return days; } 
            set { days = value; }
        }

        public ObservableCollection<DaysOfWeek> DaysOfWeeks
        { 
            get { return daysOfWeeks; } 
            set { daysOfWeeks = value; }
        }

        public ObservableCollection<Event> Events
        {
            get { return events; }
        }

        private void AddDaysOfMonth()
        {
            days.Clear();

            for (int i = 1; i < DateTime.DaysInMonth(CurrentYear, CurrentMonth) + 1; i++)
            {
                days.Add(new CalendarDay { Number = i });
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
    }
}
