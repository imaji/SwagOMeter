using System.Collections.ObjectModel;
using System.Windows.Input;
using Swagometer.Dialogs;
using Swagometer.Lib.Interfaces;

namespace Swagometer.ViewModels
{
    public interface IEditThingsViewModel<TThing>
            where TThing : IThing<TThing>
    {
        ICommand SaveCommand { get; set; }
        ICommand CreateCommand { get; set; }
        ICommand DeleteCommand { get; set; }
        ICommand DuplicateCommand { get; set; }
        ObservableCollection<TThing> Things { get; set; }
        TThing SelectedThing { get; set; }
        void ViewReady();
        void RegisterView(ICanClose view);
    }
}
