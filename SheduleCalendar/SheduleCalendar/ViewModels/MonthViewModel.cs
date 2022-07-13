using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using SheduleCalendar.Model;

namespace SheduleCalendar.ViewModels
{
    public class MonthViewModel : ObservableObject
    {
        private readonly ObservableCollection<CalendarMonth> _month;
        public ObservableCollection<CalendarMonth> Month
        {
            get { return _month; }
        }

        private readonly ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events
        {
            get { return _events; }
        }
        private readonly ObservableCollection<int> _days;
        public ObservableCollection<int> Days { get { return _days; }  }
        public MonthViewModel()
        {
            this._month = new ObservableCollection<CalendarMonth>();
            this._days = new ObservableCollection<int>();
            _currentDate = DateTime.Now;
            CurrentMonth = _currentDate.ToString("MMMM").ToUpperInvariant();
            CurrentYear = _currentDate.ToString("yyyy").ToUpperInvariant();
            AddDays();
            AddToMonthList();

        }

        private readonly DateTime _currentDate;

        private string _currentMonth;
        public string CurrentMonth
        {
            get { return _currentMonth; }
            set { _currentMonth = value; }
        }
        private string _currentYear;
        public string CurrentYear
        {
            get { return _currentYear; }
            set { _currentYear = value; }
        }

        private void CreateEvents()
        {
            //_events.Add(new Event());
        }
        private void AddDays()
        {
            for (int i = 0; i < DateTime.DaysInMonth(_currentDate.Year,_currentDate.Month); i++)
            {
                _days.Add(i);
            }
        }

        private void AddToMonthList()
        {
            for (int i = 1; i < this._days.Count; i++)
            {
                _month.Add(new CalendarMonth() { Year = _currentDate.Year, Month = _currentDate.Month, Day = _days[i] });
            }
        }
    }
}
