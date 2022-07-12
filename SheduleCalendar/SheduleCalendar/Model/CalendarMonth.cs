using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheduleCalendar.Model
{
    public class CalendarMonth
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Days { get; set; }

        private readonly List<int> _monthDays;
        public List<int> MonthDays
        {
            get { return _monthDays; }
        }

        public CalendarMonth()
        {
            this._monthDays = new List<int>();
            AddDays();
        }

        private void AddDays()
        {
            for (int i = 0; i < Days; i++)
            {
                _monthDays.Add(i);
            }
        }
    }
}
