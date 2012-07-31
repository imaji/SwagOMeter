using System;
using System.Text;

namespace Swagometer
{
    internal class Swag : ISwag
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
            var newSwag = new Swag {Company = company, Thing = thing};
            return newSwag;
        }

        public Guid Id { get; set; }

        public string Company { get; set; }

        public string Thing { get; set; }

        public bool IsValid()
        {
            bool isValid = true;

            if ((string.IsNullOrEmpty(Company)) ||
                (Company.Equals(DEFAULT_COMPANY)))
            {
                isValid = false;
            }

            if ((string.IsNullOrEmpty(Thing)) ||
                (Thing.Equals(DEFAULT_THING)))
            {
                isValid = false;
            }

            return isValid;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Company);
            sb.AppendFormat(" {0}", Thing);

            return sb.ToString();
        }

        public ISwag Duplicate()
        {
            return new Swag { Id = Id, Company = Company, Thing = Thing };
        }
    }
}
