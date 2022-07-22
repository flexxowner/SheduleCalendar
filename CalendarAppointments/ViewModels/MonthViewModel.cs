using System;
using System.Collections.ObjectModel;
using CalendarAppointments.Model;
using CalendarAppointments.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace CalendarAppointments.ViewModels
{
    public class MonthViewModel : ObservableObject
    {
        public readonly ObservableCollection<CalendarMonth> _months;
        public ObservableCollection<CalendarMonth> Months
        {
            get { return _months; }
        }

        private readonly ObservableCollection<int> _days;
        public ObservableCollection<int> Days { get { return _days; } }

        private readonly ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events
        {
            get { return _events; }
        }

        public MonthViewModel()
        {
            this._months = new ObservableCollection<CalendarMonth>();
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
        private void AddDays()
        {
            for (int i = 0; i < DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month)+1; i++)
            {
                _days.Add(i);
            }
        }

        private void AddToMonthList()
        {
            for (int i = 1; i < this._days.Count; i++)
            {
                _months.Add(new CalendarMonth() { Year = _currentDate.Year, Month = _currentDate.Month, Day = _days[i] });
            }
        }
    }
}
