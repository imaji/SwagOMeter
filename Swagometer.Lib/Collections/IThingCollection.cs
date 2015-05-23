using System.Collections.Generic;
using Swagometer.Lib.Interfaces;

namespace Swagometer.Lib.Collections
{
    public interface IThingCollection<TThing> : IList<TThing>
            where TThing : IThing<TThing>
    {
        void Save(string fileName);
    }
}
