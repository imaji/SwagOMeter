using System.Windows;
using Swagometer.Data;
using Swagometer.ViewModels;
using System;
using System.Windows.Forms;
using Swagometer.Interfaces;

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

    public partial class BaseEditThingsDialog : Window
    {
        protected BaseEditThingsDialog()
        {
            InitializeComponent();
        }
    }
}
