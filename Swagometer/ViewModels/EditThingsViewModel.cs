using Swagometer.Commands;
using Swagometer.Dialogs;
using Swagometer.Lib.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Swagometer.ViewModels
{
    public class EditThingsViewModel<TThing, TThingSource> : ViewModelBase, IEditThingsViewModel<TThing>
        where TThing : IThing<TThing>
        where TThingSource : IThingSource<TThing>
    {
        private ObservableCollection<TThing> _swag;
        private readonly TThingSource _thingSource;
        private readonly string _filePath;
        private bool _hasChanged;
        private readonly IDialogFactory<TThing> _dialogFactory;
        private ICanClose _view;

        public EditThingsViewModel(TThingSource thingSource, string filePath, IDialogFactory<TThing> dialogFactory)
        {
            _dialogFactory = dialogFactory;
            _filePath = filePath;
            _thingSource = thingSource;

            SaveCommand = new DelegateCommand(ExecuteSave);
            CreateCommand = new DelegateCommand(ExecuteCreate);
            DeleteCommand = new DelegateCommand(ExecuteDelete);
            DuplicateCommand = new DelegateCommand(ExecuteDuplicate);
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DuplicateCommand { get; set; }

        public ObservableCollection<TThing> Things
        {
            get
            {
                return _swag;
            }
            set
            {
                _swag = value;
                FirePropertyChanged("Things");
            }
        }

        public TThing SelectedThing { get; set; }

        public void ViewReady()
        {
            Things = new ObservableCollection<TThing>(_thingSource.Load(_filePath));

            _hasChanged = false;

            Things.CollectionChanged += (o, e) => _hasChanged = true;
        }

        public void RegisterView(ICanClose view)
        {
            _view = view;
        }

        private void ExecuteSave()
        {
            if (_hasChanged)
            {
                _thingSource.Save(Things, _filePath, "");
            }
            _view.Close(_hasChanged);
        }

        private void ExecuteCreate()
        {
            var createSwag = _dialogFactory.CreateDialog();

            createSwag.ShowDialog();

            if (createSwag.NewThing != null)
                Things.Add(createSwag.NewThing);
        }

        private void ExecuteDelete()
        {
            if (SelectedThing != null)
            {
                Things.Remove(SelectedThing);
                SelectedThing = default(TThing);
            }
        }

        private void ExecuteDuplicate()
        {
            if (SelectedThing != null)
            {
                Things.Add(SelectedThing.Duplicate());
            }
        }
    }
}
