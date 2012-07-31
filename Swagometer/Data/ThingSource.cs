using System;
using System.Collections.Generic;
using Swagometer.Collections;
using Swagometer.Interfaces;
using Swagometer.Views;

namespace Swagometer.Data
{
    public abstract class ThingSource<TThing>
        where TThing : IThing<TThing>
    {
        private readonly IDisplayErrorMessages _displayErrorMessage;
        private readonly string _thing;
        
        public ThingSource(IDisplayErrorMessages displayErrorMessage, string thing)
        {
            _thing = thing;
            _displayErrorMessage = displayErrorMessage;
        }

        public IList<TThing> Load(string thingLocation)
        {
            IList<TThing> attendees = new List<TThing>();

            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(thingLocation))
                errorMessage = _thing + " location not specified";
            else
            {
                attendees = LoadThings(thingLocation);

                if (attendees.Count == 0)
                    errorMessage = String.Format("No {0} available", _thing);
            }

            _displayErrorMessage.DisplayError(errorMessage);

            return attendees;
        }

        protected abstract IList<TThing> LoadThings(string thingLocation);

        public void Save(IList<TThing> thingToSave, string thingLocation)
        {
            if (thingToSave != null)
            {
                var attendeeOutput = GetCollection();

                foreach (var thing in thingToSave)
                    attendeeOutput.Add(thing);

                attendeeOutput.Save(thingLocation);
            }
        }

        protected abstract IThingCollection<TThing> GetCollection();
    }
}
