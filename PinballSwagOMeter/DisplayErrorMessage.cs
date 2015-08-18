using Swagometer.Lib.Interfaces;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PinballSwagOMeter
{
    public sealed class DisplayErrorMessages : IDisplayErrorMessages
    {
        private readonly IList<string> _errorMessages = new List<string>();

        public void HandleError(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage) ||
                !string.IsNullOrWhiteSpace(errorMessage))
            {
                MessageBox.Show(errorMessage, @"An Error Occured");
            }
            _errorMessages.Add(errorMessage);
        }

        public IEnumerable<string> GetErrors()
        {
            return _errorMessages;
        }
    }
}
