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
        public string SerializableDate
        {
            get => this.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
            set { this.StartDate = DateTime.Parse(value); }
        }

        [XmlIgnore]
        public string StartTimeString
        {
            get => this.StartTime.ToString(@"hh\:mm");
        }

        [XmlIgnore]
        public string EndTimeString
        {
            get => this.EndTime.ToString(@"hh\:mm");
        }

        [XmlIgnore]
        public string StartAndEndTimeString
        {
            get => $"{StartTimeString} - {EndTimeString}";
        }

        [XmlIgnore]
        public string StringDate
        {
            get => this.StartDate.ToString("ddd, dd MMM");
        }
    }
}
