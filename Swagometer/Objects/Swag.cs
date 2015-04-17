using System;
using Swagometer.Interfaces;

namespace Swagometer.Objects
{
    public class Swag : ISwag
    {
        private const string DEFAULT_COMPANY = "Who?";
        private const string DEFAULT_THING = "What?";

        public Swag()
        {
            Company = DEFAULT_COMPANY;
            Thing = DEFAULT_THING;
        }

        public static ISwag Create(string company, string thing)
        {
            var newSwag = new Swag { Company = company, Thing = thing };
            return newSwag;
        }

        public Guid Id { get; set; }

        public string Company { get; set; }

        public string Thing { get; set; }

        public bool IsValid()
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

        public ISwag Duplicate()
        {
            return new Swag { Id = Id, Company = Company, Thing = Thing };
        }
    }
}
