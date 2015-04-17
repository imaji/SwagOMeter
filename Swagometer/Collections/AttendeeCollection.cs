using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using Swagometer.Interfaces;
using Swagometer.Objects;
using Swagometer.Properties;

namespace Swagometer.Collections
{
    internal class AttendeeCollection : ObservableCollection<IAttendee>, IThingCollection<IAttendee>
    {
        private AttendeeCollection() {}

        public static AttendeeCollection Create()
        {
            return new AttendeeCollection();
        }

        public static AttendeeCollection Load(string attendeesLocation)
        {
            var attendees = new AttendeeCollection();

            attendees.LoadFromFile(attendeesLocation);

            return attendees;
        }

        private static bool IsFileEmpty(string attendeelocation)
        {
            return !(File.ReadAllLines(attendeelocation).Length > 0);
        }

        private static string GetFileLocation(string attendeesLocation)
        {
            var sb = new StringBuilder();
            sb.Append(attendeesLocation.TrimEnd("\\".ToCharArray()));
            sb.Append(Resources.AttendeesFile);

            return sb.ToString();
        }

        private void LoadFromFile(string attendeesLocation)
        {
            var attendeeLocation = GetFileLocation(attendeesLocation);

            if (!string.IsNullOrEmpty(attendeeLocation))
            {
                if (File.Exists(attendeeLocation))
                {
                    if (!IsFileEmpty(attendeeLocation))
                    {
                        var attendeesDoc = new XmlDocument();
                        attendeesDoc.Load(attendeeLocation);

                        foreach (XmlNode attendeeElement in attendeesDoc.ChildNodes[1])
                            Add(Attendee.Create(attendeeElement));
                    }
                }
            }
        }

        public void Save(string attendeesLocation)
        {
            var attendeeLocation = GetFileLocation(attendeesLocation);

            if ((!string.IsNullOrEmpty(attendeeLocation)))
            {
                var attendeeDoc = new XmlDocument();

                var declaration = attendeeDoc.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
                attendeeDoc.AppendChild(declaration);
                var attendeesElement = attendeeDoc.CreateElement("Attendees");
                attendeeDoc.AppendChild(attendeesElement);

                foreach (var attendee in this)
                {
                    var attendeeElement = attendeeDoc.CreateElement("Attendee");
                    var attendeeIdAttribute = attendeeDoc.CreateAttribute("id");
                    attendeeIdAttribute.Value = attendee.Id.ToString();
                    attendeeElement.Attributes.Append(attendeeIdAttribute);
                    var nameElement = attendeeDoc.CreateElement("Name");

                    nameElement.InnerText = attendee.Name;

                    attendeeElement.AppendChild(nameElement);
                    attendeesElement.AppendChild(attendeeElement);
                }

                attendeeDoc.Save(attendeeLocation);
            }
        }
    }
}
