using Swagometer.Lib.Objects;
using System.Xml.Serialization;

namespace Swagometer.Lib.Interfaces
{
    [XmlInclude(typeof(Swag))]
    public abstract class SwagBase : IThing<SwagBase>
    {
        public abstract string Company { get; set; }
        public abstract string Thing { get; set; }
        public abstract SwagBase Duplicate();
        public abstract bool IsValid();
    }
}
