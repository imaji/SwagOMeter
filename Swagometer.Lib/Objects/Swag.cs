using System;
using Swagometer.Lib.Interfaces;
using System.Xml.Serialization;

namespace Swagometer.Lib.Objects
{
    public class Swag : SwagBase
    {
        private const string DEFAULT_COMPANY = "Who?";
        private const string DEFAULT_THING = "What?";

        public Swag()
        {
            Company = DEFAULT_COMPANY;
            Thing = DEFAULT_THING;
        }

        public static SwagBase Create(string company, string thing)
        {
            var newSwag = new Swag { Company = company, Thing = thing };
            return newSwag;
        }

        [XmlIgnore]
        public Guid Id { get; set; }

        public override string Company { get; set; }

        public override string Thing { get; set; }

        public override bool IsValid()
        {
            var isValid = !(string.IsNullOrEmpty(Company) || Company.Equals(DEFAULT_COMPANY));

            if (string.IsNullOrEmpty(Thing) || Thing.Equals(DEFAULT_THING))
            {
                isValid = false;
            }

            return isValid;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Company, Thing);
        }

        public override SwagBase Duplicate()
        {
            return new Swag { Id = Id, Company = Company, Thing = Thing };
        }
    }
}
