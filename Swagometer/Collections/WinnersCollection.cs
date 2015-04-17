using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using Swagometer.Interfaces;
using Swagometer.Properties;

namespace Swagometer.Collections
{
    internal class WinnersCollection : ObservableCollection<IWinner>
    {
        private WinnersCollection() {}

        internal static WinnersCollection Create()
        {
            var collection = new WinnersCollection();

            return collection;
        }

        private static string GetFileLocation()
        {
            string attendeesLocation = Settings.Default.FileLocation;

            var sb = new StringBuilder();
            sb.Append(attendeesLocation.TrimEnd("\\".ToCharArray()));
            sb.AppendFormat(Resources.SwagWinnersFile, DateTime.Now.ToString("yyyyMMdd-HHmmss"));

            return sb.ToString();
        }

        public void Save()
        {
            var winnersLocation = GetFileLocation();
            var saveWinners = Settings.Default.SaveWinnersOnExit;

            if (saveWinners && Count > 0)
            {
                if ((!string.IsNullOrEmpty(winnersLocation)))
                {
                    var winnerDoc = new XmlDocument();

                    var declaration =  winnerDoc.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
                    winnerDoc.AppendChild(declaration);
                    var winnersElement = winnerDoc.CreateElement("Winners");
                    winnerDoc.AppendChild(winnersElement);

                    foreach (var winner in this)
                    {
                        var winnerElement = winnerDoc.CreateElement("Winner");
                        var nameElement = winnerDoc.CreateElement("Name");
                        var swagElement = winnerDoc.CreateElement("Swag");
                        var attendeeIdAttribute = winnerDoc.CreateAttribute("userId");

                        attendeeIdAttribute.Value = winner.WinningAttendee.Id.ToString();
                        nameElement.Attributes.Append(attendeeIdAttribute);
                        nameElement.InnerText = winner.WinningAttendee.Name;
                        swagElement.InnerText = winner.AwardedSwag.ToString();

                        winnerElement.AppendChild(nameElement);
                        winnerElement.AppendChild(swagElement);
                        winnersElement.AppendChild(winnerElement);
                    }

                    winnerDoc.Save(winnersLocation);
                }
            }
        }
    }
}
