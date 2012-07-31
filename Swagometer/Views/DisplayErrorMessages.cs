using System;
using System.Windows.Forms;
using Swagometer.Views;

namespace Swagometer
{
    public sealed class DisplayErrorMessages : IDisplayErrorMessages
    {
        public void DisplayError(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage) ||
                !string.IsNullOrWhiteSpace(errorMessage))
                MessageBox.Show(errorMessage, "An Error Occured");
        }
    }
}
