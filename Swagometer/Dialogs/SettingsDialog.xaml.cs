using System.Collections.Generic;
using System.IO;
using System.Windows;
using Swagometer.ViewModels;
using Swagometer.Data;

namespace Swagometer.Dialogs
{
    public partial class SettingsDialog : Window
    {
        private readonly SettingsViewModel _viewModel;
        
        internal static SettingsDialog Create(IList<ISwag> swag, IList<IAttendee> attendees, IAttendeeSource attendeeSource, ISwagSource swagSource)
        {
            var newDialog = new SettingsDialog(swag, attendees, attendeeSource, swagSource);

            return newDialog;
        }

        private SettingsDialog(IList<ISwag> swag, IList<IAttendee> attendees, IAttendeeSource attendeeSource, ISwagSource swagSource)
        {
            _viewModel = new SettingsViewModel(attendeeSource, swagSource);

            DataContext = _viewModel;
            _viewModel.Close += (e, o) =>
            {
                DialogResult = true;
                Close();
            };

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.ViewReady();
        }
    }
}
