using Swagometer.Interfaces;

namespace Swagometer.Dialogs
{
    public interface IDialogFactory<out TThing>
        where TThing : IThing<TThing>
    {
        ICreateNewThings<TThing> CreateDialog();
    }
}
