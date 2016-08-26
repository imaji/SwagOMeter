using Swagometer.Lib.Objects;
using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using Swagometer.Lib.Collections;
using System.Configuration;
namespace SwagOMeter.Config
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            var people = AttendeeCollection.Create();
            foreach (var person in textBox1.Lines)
            {
                if (!string.IsNullOrWhiteSpace(person))
                {
                    people.Add(new Attendee { Name = person });
                }
            }

            using (var fs = File.Open(Path.Combine(GetSetting("FileLocation"), "Attendees.xml"), FileMode.Truncate, FileAccess.Write))
            {
                new XmlSerializer(typeof(AttendeeCollection)).Serialize(fs, people);
            }
        }

        private string GetSetting(string key)
        {
            return ((ClientSettingsSection)((UserSettingsGroup)(ConfigurationManager.OpenExeConfiguration("PinballSwagOMeter.exe").SectionGroups[0])).Sections[0]).Settings.Get(key).Value.ValueXml.InnerXml;
        }

        private void saveSwag_Click(object sender, EventArgs e)
        {
            var swags = SwagCollection.Create();
            foreach (DataGridViewRow swagRow in swagGrid.Rows)
            {
                if (swagRow.Cells[0].Value != null && swagRow.Cells[1].Value != null)
                {
                    swags.Add(new Swag { Company = swagRow.Cells[0].Value.ToString(), Thing = swagRow.Cells[1].Value.ToString() });
                }
            }
            using (var fs = File.Open(GetPathFor("Swag.xml"), FileMode.Truncate, FileAccess.Write))
            {
                new XmlSerializer(typeof(SwagCollection)).Serialize(fs, swags);
            }
        }

        private string GetPathFor(string filename)
        {
            return Path.Combine(GetSetting("FileLocation"), filename);
        }

        private void configForm_Load(object sender, EventArgs e)
        {
            var swags = SwagCollection.Load(GetPathFor("Swag.xml"));
            foreach (var swag in swags)
            {
                var newRow = new DataGridViewRow();
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = swag.Company });
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = swag.Thing });
                swagGrid.Rows.Add(newRow);
            }

            var people = AttendeeCollection.Load(GetPathFor("Attendees.xml"));
            foreach (var person in people)
            {
                textBox1.AppendText(person.Name + Environment.NewLine);
            }
        }
    }
}
