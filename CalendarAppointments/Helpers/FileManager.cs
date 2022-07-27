using System;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace CalendarAppointments.Helpers
{
    public class FileManager
    {
        Outlook.Application Application = new Outlook.Application();
        private void SaveCalendarToDisk(string calendarFileName)
        {
            if (string.IsNullOrEmpty(calendarFileName))
                throw new ArgumentException("calendarFileName",
                "Parameter must contain a value.");

            Outlook.Folder calendar = Application.Session.GetDefaultFolder(
                Outlook.OlDefaultFolders.olFolderCalendar) as Outlook.Folder;
            Outlook.CalendarSharing exporter = calendar.GetCalendarExporter();

            // Set the properties for the export
            exporter.CalendarDetail = Outlook.OlCalendarDetail.olFullDetails;
            exporter.IncludeAttachments = true;
            exporter.IncludePrivateDetails = true;
            exporter.RestrictToWorkingHours = false;
            exporter.IncludeWholeCalendar = true;

            // Save the calendar to disk
            exporter.SaveAsICal(calendarFileName);
        }

        private void OpenICalendarFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("exportFileName",
                "Parameter must contain a value.");
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            // First try to open the icalendar file as an appointment 
            // (not a calendar folder).
            object item = null;
            try
            {
                item = Application.Session.OpenSharedItem(fileName);
            }
            catch
            { }

            if (item != null)
            {
                // Display the item
                OutlookItem olItem = new OutlookItem(item);
                olItem.Display();
                return;
            }

            // If unsucessful in opening it as an item, 
            // try opening it as a folder
            Outlook.Folder importedFolder = null;
            try
            {
                importedFolder = Application.Session.OpenSharedFolder(
                    fileName, Type.Missing, Type.Missing, Type.Missing)
                    as Outlook.Folder;
            }
            catch
            { }

            // If sucessful, open the folder in a new explorer window
            if (importedFolder != null)
            {
                Outlook.Explorer explorer =
                    Application.Explorers.Add(importedFolder,
                    Outlook.OlFolderDisplayMode.olFolderDisplayNormal);
                explorer.Display();
            }
        }
    }


}
