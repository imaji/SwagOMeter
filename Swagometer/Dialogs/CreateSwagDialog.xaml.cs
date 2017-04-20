using System.Windows;
using Swagometer.Lib.Interfaces;
using Swagometer.ViewModels;

namespace Swagometer.Dialogs
{
    public partial class CreateSwagDialog : ICreateNewThings<SwagBase>
    {
        private readonly CreateSwagViewModel _viewModel;

        public CreateSwagDialog(CreateSwagViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.ThingGood += RespondToSwagGood;

            DataContext = _viewModel;

            InitializeComponent();
        }

        public SwagBase NewThing
        {
            get
            {
                return _viewModel.NewThing;
            }
        }

        private void RespondToSwagGood(object o, ThingGoodEventArgs e)
        {
            if (e.IsGood)
            {
                DialogResult = e.IsGood;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid swag Company and Thing", "Company and Thing is required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
