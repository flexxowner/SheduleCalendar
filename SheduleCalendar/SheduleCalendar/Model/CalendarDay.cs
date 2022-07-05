using System;
using System.Collections.Generic;
using Windows.UI;

namespace SheduleCalendar.Model
{
    public class CalendarDay
    {
        public DateTime Date { get; }
        private int _daysInMonth { get;}
        private List<int> _monthDays;
        public List<int> MonthDays
        {
            get { return _monthDays; }
            set
            {
                _monthDays.Add(_daysInMonth);
            }
        }
        public CalendarDay(DateTime date)
        {
            this._daysInMonth = DateTime.DaysInMonth(date.Year,date.Month);
            Date = date;
        }
    }
}
