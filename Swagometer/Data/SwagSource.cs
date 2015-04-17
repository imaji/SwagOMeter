using System.Collections.Generic;
using Swagometer.Collections;
using Swagometer.Interfaces;
using Swagometer.Views;

namespace Swagometer.Data
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
