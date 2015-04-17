using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Swagometer.Data;
using Swagometer.Interfaces;

namespace Swagometer.Objects
{
    public class SwagOMeterAwardEngine : ISwagOMeterAwardEngine
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IList<IWinner> _badSwagCombinations = new List<IWinner>();
        private readonly IList<IWinner> _winners = new List<IWinner>();
        private IList<IAttendee> _attendees;
        private IList<ISwag> _swag;

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
                OnPropertyChanged(this, "CanSwag");
            }
        }

        private IAttendee _winningAttendee;
        public IAttendee WinningAttendee
        {
            get
            {
                return _winningAttendee;
            }
            set
            {
                _winningAttendee = value;
                OnPropertyChanged(this, "WinningAttendee");
            }
        }

        private ISwag _awardedSwag;
        public ISwag AwardedSwag
        {
            get
            {
                return _awardedSwag;
            }
            set
            {
                _awardedSwag = value;
                OnPropertyChanged(this, "AwardedSwag");
            }
        }

        public SwagOMeterAwardEngine(IAttendeeSource attendeeSource, ISwagSource swagSource)
        {
            _attendees = attendeeSource.Load(Properties.Settings.Default.FileLocation);
            _swag = swagSource.Load(Properties.Settings.Default.FileLocation);

            CheckCanSwag();
        }

        public bool CheckCanSwag()
        {
            var componentsAvailable = ((_swag != null && _swag.Count > 0) && (_attendees != null && _attendees.Count > 0));

            var validCombinationsAvailable = AttendeeSwagCombinationStillExist();

            CanSwag = componentsAvailable && validCombinationsAvailable;

            return CanSwag;
        }

        private void OnPropertyChanged(object sender, string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(sender, new PropertyChangedEventArgs(propertyName));
        }

        public void AwardSwag()
        {
            if (CanSwag)
            {
                var winner = GetWinner();

                AwardedSwag = winner.AwardedSwag;
                WinningAttendee = winner.WinningAttendee;
            }
        }

        private IWinner GetWinner()
        {
            IAttendee winningAttendee = null;
            ISwag swag = null;

            var swagAwarded = false;

            while (!swagAwarded)
            {
                winningAttendee = GetAttendee();
                swag = GetSwag();

                swagAwarded = IsAttendeeAndSwagComboValid(winningAttendee, swag);
            }

            var winner = Winner.Create(swag, winningAttendee);
            _winners.Add(winner);

            return winner;
        }

        private bool IsAttendeeAndSwagComboValid(IAttendee attendeeToCheck, ISwag swagToCheck)
        {
            var swagCanBeAwarded = !_badSwagCombinations.Any(bs => bs.AwardedSwag.Thing == swagToCheck.Thing &&
                                                                    bs.WinningAttendee.Name == attendeeToCheck.Name);

            if (!swagCanBeAwarded)
            {
                _attendees.Add(attendeeToCheck);
                _swag.Add(swagToCheck);
            }

            return swagCanBeAwarded;
        }

        private IAttendee GetAttendee()
        {
            var randomNumberGenerator = new Random();
            IAttendee winningAttendee = null;
            var attendeeNotSelected = true;

            while (attendeeNotSelected)
            {
                var index = randomNumberGenerator.Next(0, _attendees.Count);
                winningAttendee = _attendees[index];

                attendeeNotSelected = false;
            }

            _attendees.Remove(winningAttendee);

            CheckCanSwag();

            return winningAttendee;
        }

        private ISwag GetSwag()
        {
            var randomNumber = new Random();
            ISwag awardedSwag = null;
            var swagNotAwarded = true;

            while (swagNotAwarded)
            {
                int index = randomNumber.Next(0, _swag.Count);
                awardedSwag = _swag[index];

                swagNotAwarded = false;
            }

            _swag.Remove(awardedSwag);

            CheckCanSwag();

            return awardedSwag;
        }

        private IWinner GetMatchedWinner()
        {
            var matchedWinner = _winners.FirstOrDefault(w => w.WinningAttendee == WinningAttendee);
            return matchedWinner;
        }

        public void AttendeeNotPresent()
        {
            _swag.Add(AwardedSwag);
            AwardedSwag = null;

            var matchedWinner = GetMatchedWinner();
            
            WinningAttendee = null;

            if (matchedWinner != null)
                _winners.Remove(matchedWinner);

            CheckCanSwag();
        }

        public void AttendeeDoesNotWantSwag()
        {
            var matchedWinner = GetMatchedWinner();

            WinningAttendee = null;
            AwardedSwag = null;

            CreateBadSwagCombinations(matchedWinner);

            _swag.Add(matchedWinner.AwardedSwag);
            _attendees.Add(matchedWinner.WinningAttendee);
            _winners.Remove(matchedWinner);

            CheckCanSwag();
        }

        private void CreateBadSwagCombinations(IWinner matchedWinner)
        {
            _badSwagCombinations.Add(matchedWinner);

            var duplicateSwag = _swag.Where(s => s.Company.ToLower() == matchedWinner.AwardedSwag.Company.ToLower() && s.Thing.ToLower() == matchedWinner.AwardedSwag.Thing.ToLower());

            foreach (var ds in duplicateSwag)
            {
                _badSwagCombinations.Add(Winner.Create(ds, matchedWinner.WinningAttendee));
            }
        }

        private bool AttendeeSwagCombinationStillExist()
        {
            var possibleCombinationsLeft = _attendees.Count() * _swag.Count();

            var attendeeNames = _attendees.Select(a => a.Name);

            var existingEntriesWithRemainingAttendees = _badSwagCombinations.Count(bs => attendeeNames.Contains(bs.WinningAttendee.Name));

            var combinationsLeft = possibleCombinationsLeft > existingEntriesWithRemainingAttendees;
            
            return combinationsLeft;
        }

        public void SaveWinners(IWinnersSource winnersSource)
        {
            if (_winners.Count > 0)
                winnersSource.Save(_winners);
        }

        public void RefreshData(IAttendeeSource attendeeSource, ISwagSource swagSource)
        {
            _winners.Clear();
            _swag = swagSource.Load(Properties.Settings.Default.FileLocation);
            _attendees = attendeeSource.Load(Properties.Settings.Default.FileLocation);
        }
    }
}
