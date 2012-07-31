using System;
using System.Collections.Generic;
using Swagometer.Interfaces;
using Swagometer.ViewModels;

namespace Swagometer.Dialogs
{
    public interface IDialogFactory<TThing>
        where TThing : IThing<TThing>
    {
        ICreateNewThings<TThing> CreateDialog();
    }
}
