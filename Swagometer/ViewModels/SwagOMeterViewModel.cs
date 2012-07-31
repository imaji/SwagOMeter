using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Swagometer.Commands;
using Swagometer.Data;
using Swagometer.Dialogs;

namespace Swagometer.ViewModels
{
    public class SwagOMeterViewModel : ViewModelBase
    {
        public event EventHandler Close;
        public event EventHandler PlayMusic;
        public event EventHandler StopMusic;

        private readonly ISwagOMeterAwardEngine _swagOMeterAwardEngine;
        private readonly IList<IWinner> _winners = new List<IWinner>();
        private readonly IWinnersSource _winnersSource;
        private readonly IAttendeeSource _attendeeSource;
        private readonly ISwagSource _swagSource;

        private IList<ISwag> _swag;
        private IList<IAttendee> _attendees;
        private IAttendee _winningAttendee;
        private ISwag _awardedSwag;

        public SwagOMeterViewModel(IAttendeeSource attendeeSource, ISwagSource swagSource, IWinnersSource winnersSource, ISwagOMeterAwardEngine swagOMeterAwardEngine)
        {
            _swagOMeterAwardEngine = swagOMeterAwardEngine;
            _swagSource = swagSource;
            _attendeeSource = attendeeSource;
            _winnersSource = winnersSource;

            AwardSwagCommand = new DelegateCommand((_) => ExecuteAwardSwag());
            AttendeeNotPresentCommand = new DelegateCommand((_) => ExecuteAttendeeNotPresent());
            AlreadyGotSwagCommand = new DelegateCommand((_) => ExecuteAttendeeDoesNotWantSwag());
            CloseCommand = new DelegateCommand((_) => ExecuteClose());
            SettingsCommand = new DelegateCommand((_) => ExecuteOpenSettings());
            PlayMusicCommand = new DelegateCommand((_) => ExecutePlayMusic());

            Music = new Uri("Resources\\Music.mp3", UriKind.Relative);

            SwagText = Properties.Resources.CantSwag;
        }

        public ICommand AwardSwagCommand { get; private set; }
        public ICommand AttendeeNotPresentCommand { get; private set; }
        public ICommand AlreadyGotSwagCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand PlayMusicCommand { get; private set; }

        public Uri Music { get; private set; }

        private bool CanExecuteAwardSwag()
        {
            return CanSwag;
        }

        private bool _canSwag;
        public bool CanSwag
        {
            get
            {
                return _canSwag;
            }
            set
            {
                _canSwag = value;
                FirePropertyChanged("CanSwag");
            }
        }

        private bool _notCollectedEnabled;
        public bool NotCollectedEnabled
        {
            get
            {
                return _notCollectedEnabled;
            }
            set
            {
                _notCollectedEnabled = value;
                FirePropertyChanged("NotCollectedEnabled");
            }
        }

        private bool _notHereAnymoreEnabled;
        public bool NotHereAnymoreEnabled
        {
            get
            {
                return _notHereAnymoreEnabled;
            }
            set
            {
                _notHereAnymoreEnabled = value;
                FirePropertyChanged("NotHereAnymoreEnabled");
            }
        }

        public string WinningAttendee
        {
            get
            {
                string attendee = null;

                if (_winningAttendee != null)
                    attendee = _winningAttendee.Name;

                return attendee;
            }
        }

        public string WonSwag
        {
            get
            {
                string swag = null;

                if (_awardedSwag != null)
                    swag = string.Format("{0} {1}", _awardedSwag.Company, _awardedSwag.Thing);

                return swag;
            }
        }

        private bool _musicPlaying;
        public bool MusicPlaying
        {
            get
            {
                return _musicPlaying;
            }
            set
            {
                _musicPlaying = value;
                FirePropertyChanged("MusicPlaying");
            }
        }

        private string _swagText;
        public string SwagText
        {
            get
            {
                return _swagText;
            }
            private set
            {
                _swagText = value;
                FirePropertyChanged("SwagText");
            }
        }

        public void ViewReady()
        {
            _attendees = _attendeeSource.Load(Properties.Settings.Default.FileLocation);
            _swag = _swagSource.Load(Properties.Settings.Default.FileLocation);

            CheckCanSwag();
        }

        private void CheckCanSwag()
        {
            CanSwag = _swagOMeterAwardEngine.CheckCanSwag();

            if (CanSwag)
                SwagText = Properties.Resources.SwagEm;
            else if (_winners != null && _winners.Count > 0)
                SwagText = Properties.Resources.AllSwaggedOut;
        }

        private void ExecuteAwardSwag()
        {
            if (!CanSwag)
            {
                MessageBox.Show("All swagged out");
                SwagText = "All swagged out";
            }
            else
            {
                if (_swagOMeterAwardEngine.CanSwag)
                {
                    _swagOMeterAwardEngine.AwardSwag();

                    _awardedSwag = _swagOMeterAwardEngine.AwardedSwag;
                    _winningAttendee = _swagOMeterAwardEngine.WinningAttendee;

                    CanSwag = _swagOMeterAwardEngine.CanSwag;

                    if (!CanSwag)
                        SwagText = "All Swagged Out!";

                    ResetDisplayText();
                    ChangeResponseButtons(true);
                }
            }
        }

        private void ExecuteAttendeeNotPresent()
        {
            _swagOMeterAwardEngine.AttendeeNotPresent();
            _awardedSwag = null;

            CheckCanSwag();
            ResetDisplayText();

            ChangeResponseButtons(false);
        }

        private void ExecuteAttendeeDoesNotWantSwag()
        {
            _swagOMeterAwardEngine.AttendeeDoesNotWantSwag();

            _winningAttendee = null;
            _awardedSwag = null;

            CheckCanSwag();
            ResetDisplayText();

            ChangeResponseButtons(false);
        }

        private void ExecuteClose()
        {
            _swagOMeterAwardEngine.SaveWinners(_winnersSource);

            if (Close != null)
                Close(this, EventArgs.Empty);
        }

        private void ExecuteOpenSettings()
        {
            var settings = SettingsDialog.Create(_swag, _attendees, _attendeeSource, _swagSource);
            var result = settings.ShowDialog();

            if (result.HasValue && result.Value)
            {
                _swagOMeterAwardEngine.RefreshData(_attendeeSource, _swagSource);
                
                CheckCanSwag();
            }
        }

        private void ExecutePlayMusic()
        {
            if (MusicPlaying)
            {
                if (StopMusic != null)
                {
                    StopMusic(this, EventArgs.Empty);
                    MusicPlaying = false;
                }
            }
            else
            {
                if (PlayMusic != null)
                {
                    PlayMusic(this, EventArgs.Empty);
                    MusicPlaying = true;
                }
            }
        }

        private void ChangeResponseButtons(bool enabled)
        {
            NotCollectedEnabled = enabled;
            NotHereAnymoreEnabled = enabled;
        }

        private void ResetDisplayText()
        {
            FirePropertyChanged("WinningAttendee");
            FirePropertyChanged("WonSwag");
        }
    }
}
