using System;
using System.Windows;
using Swagometer.ViewModels;
using Swagometer.Data;

namespace Swagometer
{
    public partial class SwagOMeterView
    {
        private readonly SwagOMeterViewModel _viewModel;
        
        public SwagOMeterView()
        {
            var errorMessage = new DisplayErrorMessages();

            var attendeeSource = new AttendeeSource(errorMessage);
            var swagSource = new SwagSource(errorMessage);

            _viewModel = new SwagOMeterViewModel(attendeeSource, swagSource, new WinnersSource(), new SwagOMeterAwardEngine(attendeeSource, swagSource));

            _viewModel.Close += (s, e) => Close();
            _viewModel.PlayMusic += (s, e) => mediaElement.Play();
            _viewModel.StopMusic += (s, e) => mediaElement.Pause();

            DataContext = _viewModel;

            InitializeComponent();
        }

        private void formMain_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as SwagOMeterViewModel).ViewReady();
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = new TimeSpan();
            mediaElement.Play();
        }
    }
}