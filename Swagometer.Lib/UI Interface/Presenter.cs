using System;
using Swagometer.Lib.Collections;
using Swagometer.Lib.Data;
using Swagometer.Lib.Interfaces;

namespace Swagometer.Lib
{
    public class Presenter
    {
        private readonly IView _view;
        private readonly Model _model;

        public Presenter(IView view, IFileDetailProvider fileDetailProvider, IDisplayErrorMessages errorMessage)
        {
            var attendeeSource = new AttendeeSource(errorMessage);
            var swagSource = new SwagSource(errorMessage);
            var winnersSource = new WinnersSource(fileDetailProvider);

            _model = new Model(attendeeSource, swagSource, winnersSource, fileDetailProvider.FileLocation);
            _model.WinnerAvailable += _model_WinnerAvailable;

            _view = view;
            view.AttendeeLeft += view_AttendeeLeft;
            view.AttendeeRefused += view_AttendeeRefused;
            view.NewWinnerRequested += view_NewWinnerRequested;
            view.WinnersReportRequired += view_WinnersReportRequired;
        }

        void view_WinnersReportRequired(object sender, EventArgs e)
        {
            _model.SaveWinners();
        }

        void _model_WinnerAvailable(object sender, WinnerAvailableEventArgs e)
        {
            _view.DisplayWinner(e.Winner);
        }

        void view_NewWinnerRequested(object sender, EventArgs e)
        {
            _model.GetWinner();
        }

        void view_AttendeeRefused(object sender, EventArgs e)
        {
            _model.ReturnSwag();
        }

        void view_AttendeeLeft(object sender, EventArgs e)
        {
            _model.MarkAttendeeAsLeft();
        }
    }
}
