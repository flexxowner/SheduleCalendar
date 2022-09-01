using CalendarAppointments.Models.Models;
using Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace CalendarAppointments.ViewModel.Extensions
{
    public static class ListViewModelExtension
    {
        public static void AddDates(this ObservableCollection<ListModel> dates,int Year, string firstPath, string secondPath)
        {
            for (int i = 1; i < 13; i++)
            {
                var month = new DateTime(Year, i, 1);
                var days = new ObservableCollection<MonthDay>();
                for (int j = 1; j < DateTime.DaysInMonth(Year, month.Month) + 1; j++)
                {
                    days.Add(new MonthDay() { Date = new DateTime(Year, i, j) });
                }
                dates.Add(new ListModel() { Date = month, Dates = days });
                ReadEventsFromFile(firstPath, days);
                ReadEventsFromFile(secondPath, days);
            }
        }

        private static void AddEvents(ObservableCollection<Event> events, ObservableCollection<MonthDay> dates)
        {
            foreach (var date in dates)
            {
                foreach (var e in events)
                {
                    if (e.StartDate.Date == date.Date.Date)
                    {
                        date.Events.Add(e);
                    }
                }
            }
        }

        private static async void ReadEventsFromFile(string path, ObservableCollection<MonthDay> dates)
        {
            StorageFile localFile;
            try
            {
                localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(path);
            }
            catch (FileNotFoundException ex)
            {
                localFile = null;
            }
            if (localFile != null)
            {
                string localData = await FileIO.ReadTextAsync(localFile);

                var events = ObjectSerializer<ObservableCollection<Event>>.FromXml(localData);
                AddEvents(events, dates);
            }
        }
    }
}
