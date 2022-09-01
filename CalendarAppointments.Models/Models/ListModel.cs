using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarAppointments.Models.Models
{
    public class ListModel
    {
        public DateTime Date { get; set; }
        public ObservableCollection<MonthDay> Dates { get; set; }
        public string DateToString
        {
            get { return Date.ToString("MMMM", CultureInfo.CreateSpecificCulture("en")); }
        }
    }
}
