using System.Collections.Generic;
using Swagometer.Lib.Interfaces;

namespace Swagometer.Web
{
    public class ErrorMessageCollection : IDisplayErrorMessages
    {
        private readonly List<string> _errorMessages;

        public ErrorMessageCollection()
        {
            _errorMessages = new List<string>();
        }

        public void HandleError(string errorMessage)
        {
            _errorMessages.Add(errorMessage);
        }

        public IEnumerable<string> GetErrors()
        {
            return _errorMessages;
        }
    }
}