using System.Windows;
using Swagometer.Interfaces;
using Swagometer.ViewModels;

namespace Swagometer.Dialogs
{
    public partial class CreateAttendeeDialog : ICreateNewThings<IAttendee>
    {
        private readonly CreateAttendeeViewModel _viewModel;

        public CreateAttendeeDialog(CreateAttendeeViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.ThingGood += RespondToAttendeeGood;

            DataContext = _viewModel;

            InitializeComponent();
        }

        public IAttendee NewThing
        {
            get
            {
                return _viewModel.NewThing;
            }
        }

        private void RespondToAttendeeGood(object o, ThingGoodEventArgs e)
        {
            if (e.IsGood)
            {
                DialogResult = e.IsGood;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid attendee Name", "Name is required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
