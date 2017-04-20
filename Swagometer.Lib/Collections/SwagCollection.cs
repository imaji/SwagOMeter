using Swagometer.Lib.Interfaces;
using Swagometer.Lib.Objects;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Swagometer.Lib.Collections
{
    [XmlRoot("SwagStash")]
    public class SwagCollection : ObservableCollection<SwagBase>, IThingCollection<SwagBase>
    {
        private SwagCollection() { }

        public static SwagCollection Create()
        {
            return new SwagCollection();
        }

        public static SwagCollection Load(string swagLocation)
        {
            var lotsOfSwag = new SwagCollection();

            lotsOfSwag.LoadFromFile(swagLocation);

            return lotsOfSwag;
        }

        private static bool IsFileEmpty(string swaglocation)
        {
            return !(File.ReadAllLines(swaglocation).Length > 0);
        }

        private static string GetFileLocation(string swagLocation)
        {
            return swagLocation;
        }

        private void LoadFromFile(string swagLocation)
        {
            var swagPath = GetFileLocation(swagLocation);

            if (string.IsNullOrEmpty(swagPath))
            {
                return;
            }
            if (!File.Exists(swagPath))
            {
                return;
            }
            if (IsFileEmpty(swagPath))
            {
                return;
            }

            var swagDoc = new XmlDocument();
            swagDoc.Load(swagPath);

            foreach (XmlNode swagElement in swagDoc.ChildNodes[1])
            {
                var companyElement = swagElement.FirstChild as XmlElement;
                var thingElement = swagElement.ChildNodes[1] as XmlElement;
                Add(Swag.Create(companyElement.InnerText, thingElement.InnerText));
            }
        }

        public void Save(string fileName)
        {
            var swagPath = fileName;

            if ((!string.IsNullOrEmpty(swagPath)))
            {
                var swagDoc = new XmlDocument();

                var declaration = swagDoc.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
                swagDoc.AppendChild(declaration);
                var swagStashElement = swagDoc.CreateElement("SwagStash");
                swagDoc.AppendChild(swagStashElement);

                foreach (var swag in this)
                {
                    var swagElement = swagDoc.CreateElement("Swag");
                    var companyElement = swagDoc.CreateElement("Company");
                    var thingElement = swagDoc.CreateElement("Thing");

                    companyElement.InnerText = swag.Company;
                    thingElement.InnerText = swag.Thing;

                    swagElement.AppendChild(companyElement);
                    swagElement.AppendChild(thingElement);
                    swagStashElement.AppendChild(swagElement);
                }

                swagDoc.Save(swagPath);
            }
        }
    }
}
