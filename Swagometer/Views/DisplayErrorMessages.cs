using System.Windows.Forms;

namespace Swagometer.Views
{
    public sealed class DisplayErrorMessages : IDisplayErrorMessages
    {
        public void DisplayError(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage) ||
                !string.IsNullOrWhiteSpace(errorMessage))
                MessageBox.Show(errorMessage, @"An Error Occured");
        }
    }
}
