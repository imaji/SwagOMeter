using Swagometer.Lib.Interfaces;
using Swagometer.ViewModels;

namespace Swagometer.Dialogs
{
    public class SwagDialogFactory : IDialogFactory<SwagBase>
    {
        public ICreateNewThings<SwagBase> CreateDialog()
        {
            var viewModel = new CreateSwagViewModel();

            return new CreateSwagDialog(viewModel);
        }
    }
}
