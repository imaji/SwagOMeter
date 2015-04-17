using Swagometer.Interfaces;
using Swagometer.ViewModels;

namespace Swagometer.Dialogs
{
    public class EditThingsDialog<TThing> : BaseEditThingsDialog, ICanClose
        where TThing : IThing<TThing>
    {
        public EditThingsDialog(IEditThingsViewModel<TThing> viewModel)
        {
            viewModel.RegisterView(this);

            DataContext = viewModel;

            viewModel.ViewReady();
        }

        public void Close(bool result)
        {
            DialogResult = result;
            Close();
        }
    }

    public partial class BaseEditThingsDialog
    {
        protected BaseEditThingsDialog()
        {
            InitializeComponent();
        }
    }
}
