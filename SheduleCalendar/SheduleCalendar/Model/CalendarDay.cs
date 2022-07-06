using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheduleCalendar.Model
{
    public class CalendarDay
    {
        public DateTime Date { get; }
        private int _daysInMonth { get; }
        private readonly List<int> _monthDays;
        public List<int> MonthDays
        {
            get { return _monthDays; }
        }
        public CalendarDay(DateTime date)
        {
            this._monthDays = new List<int>();
            this._daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            AddDays();
            Date = date;
        }
        private void AddDays()
        {
            for (int i = 0; i < _daysInMonth; i++)
            {
                _monthDays.Add(i);
            }
        }
    }
}
