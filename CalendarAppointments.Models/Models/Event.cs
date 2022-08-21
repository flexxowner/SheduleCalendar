using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CalendarAppointments.Models.Models
{
    [Serializable]
    public class Event 
    {
        public string Subject { get; set; }
        [XmlIgnore]
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        [XmlElement("SomeDate")]
        public string StartDateString
        {
            get { return this.StartDate.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { this.StartDate = DateTime.Parse(value); }
        }
    }
}
