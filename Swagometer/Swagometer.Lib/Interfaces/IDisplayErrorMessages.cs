using System.Collections.Generic;

namespace Swagometer.Lib.Interfaces
{
    public interface IDisplayErrorMessages
    {
        void HandleError(string errorMessage);
        IEnumerable<string> GetErrors();
    }
}
