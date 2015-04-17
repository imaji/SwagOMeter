using System.Collections.Generic;

namespace Swagometer.Data
{
    public interface IThingSource<TThing>
    {
        IList<TThing> Load(string thingLocation);
        void Save(IList<TThing> thingToSave, string thingLocation);
    }
}
