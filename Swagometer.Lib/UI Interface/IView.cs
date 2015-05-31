using Swagometer.Lib.Interfaces;
using System;

namespace Swagometer.Lib.UI_Interface
{
    public interface IView
    {
        event EventHandler NewWinnerRequested;
        event EventHandler AttendeeLeft;
        event EventHandler AttendeeRefused;
        event EventHandler WinnersReportRequired;
        void DisplayWinner(IWinner winner);
        Presenter Presenter { set; }
    }
}
