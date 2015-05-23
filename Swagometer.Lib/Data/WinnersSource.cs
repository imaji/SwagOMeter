using Swagometer.Lib.Collections;
using Swagometer.Lib.Interfaces;
using System.Collections.Generic;

namespace Swagometer.Lib.Data
{
    public class WinnersSource : IWinnersSource
    {
        private IFileDetailProvider _fileDetailProvider;
        public WinnersSource(IFileDetailProvider fileDetailProvider)
        {
            _fileDetailProvider = fileDetailProvider;
        }

        public void Save(IList<IWinner> winners)
        {
            if (winners != null)
            {
                var winnersOutput = WinnersCollection.Create(_fileDetailProvider);

                foreach (var winner in winners)
                    winnersOutput.Add(winner);

                winnersOutput.Save();
            }
        }
    }
}
