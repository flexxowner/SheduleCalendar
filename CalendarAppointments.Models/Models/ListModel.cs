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
        private const string Culture = "en";
        private const string Format = "MMMM";

        public ListModel()
        {
        }

        public ObservableCollection<MonthDay> Dates { get; set; }

        public DateTime Date { get; set; }

        public string DateToString
        {
            get => Date.ToString(Format, CultureInfo.CreateSpecificCulture(Culture));
        }
    }
}
