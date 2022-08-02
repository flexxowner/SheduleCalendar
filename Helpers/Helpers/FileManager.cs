using System;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Helpers.Helpers
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

            exporter.CalendarDetail = Outlook.OlCalendarDetail.olFullDetails;
            exporter.IncludeAttachments = true;
            exporter.IncludePrivateDetails = true;
            exporter.RestrictToWorkingHours = false;
            exporter.IncludeWholeCalendar = true;

            
            exporter.SaveAsICal(calendarFileName);
        }

        private void OpenICalendarFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("exportFileName",
                "Parameter must contain a value.");
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            object item = null;
            try
            {
                item = Application.Session.OpenSharedItem(fileName);
            }
            catch
            { }

            if (item != null)
            {
                OutlookItem olItem = new OutlookItem(item);
                olItem.Display();
                return;
            }

            Outlook.Folder importedFolder = null;
            try
            {
                importedFolder = Application.Session.OpenSharedFolder(
                    fileName, Type.Missing, Type.Missing, Type.Missing)
                    as Outlook.Folder;
            }
            catch
            { }

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
