using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swagometer.Interfaces;

namespace Swagometer.Data
{
    public interface IThingSource<TThing>
    {
        IList<TThing> Load(string thingLocation);
        void Save(IList<TThing> thingToSave, string thingLocation);
    }
}
