using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Service;
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
        public static void SearchEvent(
            this ObservableCollection<Event> FoundEvents, string Title, ObservableCollection<Event> events)
        {
            EventService.SearchEvent(FoundEvents, Title, events);
        }

        public static void ReadEventsFromFile(
            this ObservableCollection<Event> Events, string path)
        {
            EventService.ReadEventsFromFile(path, Events);
        }
    }
}
