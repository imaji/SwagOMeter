using System.Collections.Generic;
using Swagometer.Interfaces;

namespace Swagometer.Collections
{
    public interface IThingCollection<TThing> : IList<TThing>
            where TThing : IThing<TThing>
    {
        void Save(string thingLocation);
    }
}
