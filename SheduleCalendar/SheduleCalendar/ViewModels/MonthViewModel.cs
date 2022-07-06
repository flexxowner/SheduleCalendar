using System;
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

        private readonly DateTime _currentDate;
        public MonthViewModel()
        {
            _currentDate = DateTime.Now;
            this._month = new ObservableCollection<CalendarMonth>()
            {
                new CalendarMonth(_currentDate)
            };
        }
    }
}
