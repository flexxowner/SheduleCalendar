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
    public static class SearchViewModelExtension
    {
        public static void SearchEvent(this ObservableCollection<Event> FoundEvents, string Title, ObservableCollection<Event> events)
        {
            if (Title != "")
            {
                var _events = events.Where(x => x.Subject.Contains(Title));
                foreach (var item in _events)
                {
                    if (item != null)
                    {
                        FoundEvents.Add(item);
                    }
                }
            }
        }

        public static async void ReadEventsFromFile(this ObservableCollection<Event> Events, string path)
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

                foreach (var item in events)
                {
                    Events.Add(item);
                }
            }
        }
    }
}
