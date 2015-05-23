using Swagometer.Commands;
using Swagometer.Dialogs;
using Swagometer.Lib.Interfaces;
using Swagometer.Properties;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace Swagometer.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public event EventHandler Close;

        private readonly IAttendeeSource _attendeeSource;
        private readonly ISwagSource _swagSource;
        private string _originalFileLocation = string.Empty;

        public SettingsViewModel(IAttendeeSource attendeeSource, ISwagSource swagSource)
        {
            _swagSource = swagSource;
            _attendeeSource = attendeeSource;

            EditSwagCommand = new DelegateCommand(ExecuteEditSwag);
            EditAttendeesCommand = new DelegateCommand(ExecuteEditAttendees);
            SetFileLocationCommand = new DelegateCommand(ExecuteSetFileLocation);
            CloseCommand = new DelegateCommand(ExecuteClose);
        }

        public ICommand EditSwagCommand { get; private set; }
        public ICommand EditAttendeesCommand { get; private set; }
        public ICommand SetFileLocationCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        private string _fileLocation;
        public string FileLocation
        {
            get
            {
                return _fileLocation;
            }
            set
            {
                _fileLocation = value;
                FirePropertyChanged("FileLocation");
            }
        }

        private bool _saveWinnersOnExit;
        public bool SaveWinnersOnExit
        {
            get
            {
                return _saveWinnersOnExit;
            }
            set
            {
                _saveWinnersOnExit = value;
                FirePropertyChanged("SaveWinnersOnExit");
            }
        }

        private void ExecuteEditSwag()
        {
            var editSwag = new EditThingsDialog<ISwag>(new EditThingsViewModel<ISwag, ISwagSource>(_swagSource, FileLocation + "\\Swag.xml", new SwagDialogFactory()));

            editSwag.ShowDialog();
        }

        private void ExecuteEditAttendees()
        {
            var editAttendees = new EditThingsDialog<IAttendee>(new EditThingsViewModel<IAttendee, IAttendeeSource>(_attendeeSource, FileLocation + "\\Attendees.xml", new AttendeeDialogFactory()));

            editAttendees.ShowDialog();
        }

        private void ExecuteSetFileLocation()
        {
            var dirDialog = new FolderBrowserDialog();

            if (dirDialog.ShowDialog().Equals(DialogResult.OK))
            {
                FileLocation = dirDialog.SelectedPath;
                _attendeeSource.Load(Path.Combine(FileLocation, Resources.AttendeesFile));
                _swagSource.Load(Path.Combine(FileLocation, Resources.SwagFile));
            }
        }

        private void ExecuteClose()
        {
            if (string.Compare(_originalFileLocation, FileLocation, StringComparison.OrdinalIgnoreCase) != 0)
            {
                Settings.Default[Constants.FileLocationProperty] = FileLocation;

                if (!string.IsNullOrEmpty(_originalFileLocation))
                {
                    MoveFile(_originalFileLocation, FileLocation, Constants.SwagFilename);
                    MoveFile(_originalFileLocation, FileLocation, Constants.AttendeesFilename);
                }
            }

            Settings.Default[Constants.SaveWinnersOnExitProperty] = SaveWinnersOnExit;

            Settings.Default.Save();

            if (Close != null)
                Close(this, EventArgs.Empty);
        }

        private static void MoveFile(string target, string destination, string filename)
        {
            var locationFile = new FileInfo(String.Format("{0}\\{1}", target, filename));

            if (locationFile.Exists)
                locationFile.MoveTo(String.Format("{0}\\{1}", destination, filename));
        }

        public void ViewReady()
        {
            SaveWinnersOnExit = Settings.Default.SaveWinnersOnExit;
            FileLocation = Settings.Default.FileLocation;
            _originalFileLocation = FileLocation;
        }
    }
}
