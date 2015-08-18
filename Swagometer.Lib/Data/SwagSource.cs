using Swagometer.Lib.Collections;
using Swagometer.Lib.Interfaces;
using System.Collections.Generic;

namespace Swagometer.Lib.Data
{
    public class SwagSource : ThingSource<ISwag>, ISwagSource
    {
        public SwagSource(IDisplayErrorMessages displayErrorMessage) : base(displayErrorMessage, "Swag") { }

        protected override IList<ISwag> LoadThings(string thingLocation)
        {
            return SwagCollection.Load(thingLocation);
        }

        protected override IThingCollection<ISwag> GetCollection()
        {
            return SwagCollection.Create();
        }
    }
}
