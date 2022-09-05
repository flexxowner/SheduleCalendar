using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CalendarAppointments.Models.Models;
using Helpers.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.Storage;
using CalendarAppointments.ViewModel.Extensions;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class SearchViewModel : ObservableObject
    {
        private ObservableCollection<Event> events;
        private ObservableCollection<Event> foundEvents;
        private string title;
        private const string firstPath = "Appointments.xml";
        private const string secondPath = "outlook.xml";

        public SearchViewModel()
        {
            events = new ObservableCollection<Event>();
            foundEvents = new ObservableCollection<Event>();
            Events.ReadEventsFromFile(firstPath);
            Events.ReadEventsFromFile(secondPath);
        }

        public ObservableCollection<Event> Events
        {
            get { return events; }
            set { events = value; }
        }

        public ObservableCollection<Event> FoundEvents
        {
            get { return foundEvents; }
            set { foundEvents = value; }
        }

        public string Title
        {
            get { return title; }
            set
            {
                FoundEvents.Clear();
                title = value;
                FoundEvents.SearchEvent(Title, Events);
            }
        }
    }
}
