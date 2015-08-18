using Swagometer.Lib.Interfaces;
using Swagometer.ViewModels;

namespace Swagometer.Dialogs
{
    public class SwagDialogFactory : IDialogFactory<ISwag>
    {
        public ICreateNewThings<ISwag> CreateDialog()
        {
            var viewModel = new CreateSwagViewModel();

            return new CreateSwagDialog(viewModel);
        }
    }
}
