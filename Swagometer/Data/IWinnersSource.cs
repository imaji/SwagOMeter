using System.Collections.Generic;

namespace Swagometer.Data
{
    public interface IWinnersSource
    {
        void Save(IList<IWinner> winners);
    }
}
