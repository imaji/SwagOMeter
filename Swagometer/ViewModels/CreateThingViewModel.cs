using System;
using System.Windows.Input;
using Swagometer.Commands;
using Swagometer.Dialogs;
using Swagometer.Lib.Interfaces;

namespace Swagometer.ViewModels
{
    public abstract class CreateThingViewModel<TThing>
        where TThing : IThing<TThing>
    {
        public event EventHandler<ThingGoodEventArgs> ThingGood;

        protected CreateThingViewModel()
        {
            CreateCommand = new DelegateCommand(ExecuteCreate);
        }

        public ICommand CreateCommand { get; set; }

        public TThing NewThing { get; set; }

        protected abstract void ExecuteCreate();

        protected void FireThingGood(bool isGood)
        {
            if (ThingGood != null)
                ThingGood(this, new ThingGoodEventArgs(isGood));
        }
    }
}
