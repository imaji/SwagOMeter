using Swagometer.Lib.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

namespace Swagometer.Lib.Collections
{
    internal class WinnersCollection : ObservableCollection<IWinner>
    {
        private static IFileDetailProvider _fileDetailProvider;
        private WinnersCollection() { }

        internal static WinnersCollection Create(IFileDetailProvider fileDetailProvider)
        {
            _fileDetailProvider = fileDetailProvider;

            var collection = new WinnersCollection();

            return collection;
        }

        private static string GetFileLocation()
        {
            string attendeesLocation = _fileDetailProvider.FileLocation;

            var sb = new StringBuilder();
            sb.Append(attendeesLocation.TrimEnd("\\".ToCharArray()));

            sb.AppendFormat(_fileDetailProvider.SwagWinnersFileFormat, DateTime.Now.ToString("yyyyMMdd-HHmmss"));

            return sb.ToString();
        }

        public void Save()
        {
            var winnersLocation = GetFileLocation();
            //TODO: make this work
            var saveWinners = false;//Settings.Default.SaveWinnersOnExit;

            if (saveWinners && Count > 0)
            {
                if ((!string.IsNullOrEmpty(winnersLocation)))
                {
                    var winnerDoc = new XmlDocument();

                    var declaration = winnerDoc.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
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
