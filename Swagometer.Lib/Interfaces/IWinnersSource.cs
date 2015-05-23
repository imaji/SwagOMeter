using System.Collections.Generic;

namespace Swagometer.Lib.Interfaces
{
    public interface IWinnersSource
    {
        void Save(IList<IWinner> winners);
    }
}
