using System.Collections.Generic;
using Swagometer.Interfaces;

namespace Swagometer.Data
{
    public interface IWinnersSource
    {
        void Save(IList<IWinner> winners);
    }
}
