using System;
using System.Collections.Generic;
using Swagometer.Lib.Collections;
using Swagometer.Lib.Interfaces;

namespace Swagometer.Lib.Data
{
    public abstract class ThingSource<TThing>
        where TThing : IThing<TThing>
    {
        private readonly IDisplayErrorMessages _displayErrorMessage;
        private readonly string _thing;

        protected ThingSource(IDisplayErrorMessages displayErrorMessage, string thing)
        {
            _thing = thing;
            _displayErrorMessage = displayErrorMessage;
        }

        public IList<TThing> Load(string thingLocation)
        {
            IList<TThing> attendees = new List<TThing>();

            var errorMessage = string.Empty;

            if (string.IsNullOrEmpty(thingLocation))
                errorMessage = _thing + " location not specified";
            else
            {
                attendees = LoadThings(thingLocation);

                if (attendees.Count == 0)
                    errorMessage = String.Format("No {0} available", _thing);
            }

            _displayErrorMessage.HandleError(errorMessage);

            return attendees;
        }

        protected abstract IList<TThing> LoadThings(string thingLocation);

        public void Save(IList<TThing> thingToSave, string thingLocation, string fileName)
        {
            if (thingToSave != null)
            {
                var attendeeOutput = GetCollection();

                foreach (var thing in thingToSave)
                    attendeeOutput.Add(thing);

                attendeeOutput.Save(thingLocation, fileName);
            }
        }

        protected abstract IThingCollection<TThing> GetCollection();
    }
}
