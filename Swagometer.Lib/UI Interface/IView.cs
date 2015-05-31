using System;
using Swagometer.Lib.Interfaces;

namespace Swagometer.Lib
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
