using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Swagometer.Commands;
using Swagometer.Dialogs;
using Swagometer.Interfaces;

namespace Swagometer.ViewModels
{
    public abstract class CreateThingViewModel<TThing>
        where TThing : IThing<TThing>
    {
        public event EventHandler<ThingGoodEventArgs> ThingGood;

        public CreateThingViewModel()
        {
            CreateCommand = new DelegateCommand(_ => ExecuteCreate());
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
