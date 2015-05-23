using System.Collections.Generic;

namespace Swagometer.Lib.Interfaces
{
    public interface IThingSource<TThing>
    {
        IList<TThing> Load(string thingLocation);
        void Save(IList<TThing> thingToSave, string thingLocation,string fileName);
    }
}
