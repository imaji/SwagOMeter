using System;
using System.Windows;
using Swagometer.Lib.Data;
using Swagometer.Objects;
using Swagometer.ViewModels;
using Swagometer.Lib;
using Swagometer.Properties;

namespace Swagometer.Views
{
    public partial class SwagOMeterView
    {
        public SwagOMeterView()
        {
            var fileDetailProvider = FileDetailProvider.Create(Settings.Default.FileLocation, (string)Resources["SwagWinnersFile"]);

            var errorMessage = new DisplayErrorMessages();

            var attendeeSource = new AttendeeSource(errorMessage);
            var swagSource = new SwagSource(errorMessage);

            var viewModel = new SwagOMeterViewModel(attendeeSource, swagSource, new WinnersSource(fileDetailProvider), new SwagOMeterAwardEngine(attendeeSource, swagSource));

            viewModel.Close += (s, e) => Close();
            viewModel.PlayMusic += (s, e) => mediaElement.Play();
            viewModel.StopMusic += (s, e) => mediaElement.Pause();

            DataContext = viewModel;

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