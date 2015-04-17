using System.Collections.Generic;
using System.Windows;
using Swagometer.Data;
using Swagometer.Interfaces;
using Swagometer.ViewModels;

namespace Swagometer.Dialogs
{
    public partial class SettingsDialog
    {
        private readonly SettingsViewModel _viewModel;
        
        internal static SettingsDialog Create(IList<ISwag> swag, IList<IAttendee> attendees, IAttendeeSource attendeeSource, ISwagSource swagSource)
        {
            var newDialog = new SettingsDialog(attendeeSource, swagSource);

            return newDialog;
        }

        private SettingsDialog(IAttendeeSource attendeeSource, ISwagSource swagSource)
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
