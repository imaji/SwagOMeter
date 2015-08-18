using PinballSwagOMeter.Properties;
using PinballSwagOMeter.Transformers;
using Swagometer.Lib;
using Swagometer.Lib.Collections;
using Swagometer.Lib.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PinballSwagOMeter
{
    public partial class MainForm : Form, IView
    {
        public Presenter Presenter { get; set; }
        public event EventHandler NewWinnerRequested;
        public event EventHandler AttendeeLeft;
        public event EventHandler AttendeeRefused;
        public event EventHandler WinnersReportRequired;

        private Timer _timer;
        private MatrixTransformer _matrixTransformer;

        private readonly CharacterToBitMapConverter _characterToBitMapConverter = new CharacterToBitMapConverter(Resources.@on, Resources.off);
        private int _onOffImageWidth;
        private int _onOffImageHeight;
        private Bitmap _matrixBitmap;

        public MainForm()
        {
            InitializeComponent();
            PositionEverything();
            InitialiseTimer();
            BuildPresenter();
        }

        public void DisplayWinner(IWinner winner)
        {
            if (winner != null)
            {
                var currentBitPatterns = _characterToBitMapConverter.GetBitPattern(winner.WinningAttendee.Name, "*** wins ***", winner.AwardedSwag.Company, winner.AwardedSwag.Thing);
                _matrixTransformer = MatrixTransformer.Create<WinnerTransformer>(currentBitPatterns);
                StartTransform(100);
            }
            else
            {
                _matrixTransformer = MatrixTransformer.Create<GameOverTransformer>();
                StartTransform(1000);
            }
        }

        private void BuildPresenter()
        {
            var fileDetailProvider = FileDetailProvider.Create(Settings.Default.FileLocation, "Swag-Winners-{0}.xml");
            var errorMessage = new DisplayErrorMessages();
            Presenter = new Presenter(this, fileDetailProvider, errorMessage);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            DisplayCurrentBitPatterns(new BitMatrix());
            _matrixTransformer = MatrixTransformer.Create<StartupTransformer>();
            StartTransform(200);
        }

        private void InitialiseTimer()
        {
            _timer = new Timer
            {
                Enabled = false,
                Interval = 2000
            };

            _timer.Tick += _timer_Tick;
        }

        private void PositionEverything()
        {
            var screenArea = Screen.FromControl(this).Bounds;
            pictureBox.Width = (screenArea.Width / 5) * 3;
            pictureBox.Height = (screenArea.Width / Constants.Columns) * 20;

            _onOffImageWidth = pictureBox.Width / Constants.Columns;
            _onOffImageHeight = pictureBox.Height / Constants.Rows;

            pictureBox.Width = _onOffImageWidth * Constants.Columns;
            pictureBox.Height = _onOffImageHeight * Constants.Rows;
            pictureBox.BackColor = Color.Black;

            pictureBox.Left = (screenArea.Width - pictureBox.Width) / 2;
            pictureBox.Top = (screenArea.Height - pictureBox.Height) / 2;

            _matrixBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = _matrixBitmap;

            Cursor.Hide();
        }

        private void StartTransform(int initialDelayMs)
        {
            _timer.Interval = initialDelayMs;
            _timer.Enabled = true;
        }

        private void DisplayCurrentBitPatterns(BitMatrix currentBitPatterns)
        {
            using (var bitmapGraphics = Graphics.FromImage(_matrixBitmap))
            {
                _characterToBitMapConverter.BuildBitMapPicture(currentBitPatterns, _onOffImageWidth, _onOffImageHeight, bitmapGraphics);
            }
            pictureBox.Invalidate();
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                OnNewWinnerRequested();
            }
            else if (e.KeyCode == Keys.R)
            {
                OnAttendeeRefusedSwag();
                OnNewWinnerRequested();
            }
            else if (e.KeyCode == Keys.N)
            {
                OnAttendeeNotPresent();
                OnNewWinnerRequested();
            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            OnWinnersReportRequired();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Enabled = false;
            DisplayCurrentBitPatterns(_matrixTransformer.GetNextScreen());
            _timer.Enabled = _matrixTransformer.KeepTimerRunning;
            if (_timer.Enabled)
            {
                _timer.Interval = _matrixTransformer.SubsequentDelayMs;
            }
        }

        private void OnNewWinnerRequested()
        {
            if (NewWinnerRequested != null)
            {
                NewWinnerRequested(this, EventArgs.Empty);
            }
        }

        private void OnAttendeeRefusedSwag()
        {
            if (AttendeeRefused != null)
            {
                AttendeeRefused(this, EventArgs.Empty);
            }
        }

        private void OnAttendeeNotPresent()
        {
            if (AttendeeLeft != null)
            {
                AttendeeLeft(this, EventArgs.Empty);
            }
        }

        private void OnWinnersReportRequired()
        {
            if (WinnersReportRequired != null)
            {
                WinnersReportRequired(this, EventArgs.Empty);
            }
        }
    }
}