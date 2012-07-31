using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using Swagometer.Commands;
using Swagometer.Dialogs;
using Swagometer.Data;

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

            EditSwagCommand = new DelegateCommand((_) => ExecuteEditSwag());
            EditAttendeesCommand = new DelegateCommand((_) => ExecuteEditAttendees());
            SetFileLocationCommand = new DelegateCommand((_) => ExecuteSetFileLocation());
            CloseCommand = new DelegateCommand((_) => ExecuteClose());
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
            var editSwag = new EditThingsDialog<ISwag>(new EditThingsViewModel<ISwag, ISwagSource>(_swagSource, FileLocation, new SwagDialogFactory()));

            editSwag.ShowDialog();
        }

        private void ExecuteEditAttendees()
        {
            var editAttendees = new EditThingsDialog<IAttendee>(new EditThingsViewModel<IAttendee, IAttendeeSource>(_attendeeSource, FileLocation, new AttendeeDialogFactory()));

            editAttendees.ShowDialog();
        }

        private void ExecuteSetFileLocation()
        {
            var dirDialog = new FolderBrowserDialog();

            if (dirDialog.ShowDialog().Equals(System.Windows.Forms.DialogResult.OK))
            {
                FileLocation = dirDialog.SelectedPath;
                _attendeeSource.Load(FileLocation);
                _swagSource.Load(FileLocation);
            }
        }

        private void ExecuteClose()
        {
            if (string.Compare(_originalFileLocation, FileLocation, true) != 0)
            {
                Properties.Settings.Default[Constants.FileLocationProperty] = FileLocation;

                if (!string.IsNullOrEmpty(_originalFileLocation))
                {
                    MoveFile(_originalFileLocation, FileLocation, Constants.SwagFilename);
                    MoveFile(_originalFileLocation, FileLocation, Constants.AttendeesFilename);
                }
            }

            Properties.Settings.Default[Constants.SaveWinnersOnExitProperty] = SaveWinnersOnExit;

            Properties.Settings.Default.Save();

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
            SaveWinnersOnExit = Properties.Settings.Default.SaveWinnersOnExit;
            FileLocation = Properties.Settings.Default.FileLocation;
            _originalFileLocation = FileLocation;
        }
    }
}
