using System.Windows;
using Swagometer.Lib.Interfaces;
using Swagometer.ViewModels;

namespace Swagometer.Dialogs
{
    public partial class CreateAttendeeDialog : ICreateNewThings<AttendeeBase>
    {
        private readonly CreateAttendeeViewModel _viewModel;

        public CreateAttendeeDialog(CreateAttendeeViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.ThingGood += RespondToAttendeeGood;

            DataContext = _viewModel;

            InitializeComponent();
        }

        public AttendeeBase NewThing
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
