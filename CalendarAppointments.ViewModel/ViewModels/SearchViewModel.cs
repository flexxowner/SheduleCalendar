﻿using System.Collections.ObjectModel;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using CalendarAppointments.ViewModel.Extensions;
using System.ComponentModel;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class SearchViewModel : ObservableObject, INotifyPropertyChanged
    {
        private const string firstPath = "Appointments.xml";
        private const string secondPath = "outlook.xml";
        private readonly ObservableCollection<Event> events;
        private readonly ObservableCollection<Event> foundEvents;
        private string title;

        public SearchViewModel()
        {
            events = new ObservableCollection<Event>();
            foundEvents = new ObservableCollection<Event>();
            Events.ReadEventsFromFile(firstPath);
            Events.ReadEventsFromFile(secondPath);
        }

        public ObservableCollection<Event> Events
        {
            get => events;
        }

        public ObservableCollection<Event> FoundEvents
        {
            get => foundEvents;
        }

        public string Title
        {
            get => title;
            set
            {
                FoundEvents.Clear();
                title = value;
                FoundEvents.SearchEvent(Title, Events);
            }
        }
    }
}
