using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using Swagometer.Properties;

namespace Swagometer.Collections
{
    internal class SwagCollection : ObservableCollection<ISwag>, IThingCollection<ISwag>
    {
        private SwagCollection() {}

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
            var isEmpty = true;

            if (File.ReadAllLines(swaglocation).Length > 0)
            {
                isEmpty = false;
            }

            return isEmpty;
        }

        private static string GetFileLocation(string swagLocation)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(swagLocation.TrimEnd("\\".ToCharArray()));
            sb.Append(Resources.SwagFile);

            return sb.ToString();
        }

        private void LoadFromFile(string swagLocation)
        {
            var swagPath = GetFileLocation(swagLocation);

            if (!string.IsNullOrEmpty(swagPath))
            {
                if (File.Exists(swagPath))
                {
                    if (!IsFileEmpty(swagPath))
                    {
                        var swagDoc = new XmlDocument();
                        swagDoc.Load(swagPath);

                        foreach (XmlNode swagElement in swagDoc.ChildNodes[1])
                        {
                            var companyElement = swagElement.FirstChild as XmlElement;
                            var thingElement = swagElement.ChildNodes[1] as XmlElement;
                            Add(Swag.Create(companyElement.InnerText, thingElement.InnerText));
                        }
                    }
                }
            }
        }

        public void Save(string swagLocation)
        {
            var swagPath = GetFileLocation(swagLocation);

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
