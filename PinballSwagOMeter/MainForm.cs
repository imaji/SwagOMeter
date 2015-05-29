using PinballSwagOMeter.Properties;
using Swagometer.Lib;
using Swagometer.Lib.Data;
using Swagometer.Lib.Interfaces;
using Swagometer.Lib.Objects;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace PinballSwagOMeter
{
    public partial class MainForm : Form
    {
        private Timer _timer;
        private MatrixTransformer _matrixTransformer;
        private BigInteger[] _currentBitPatterns = new BigInteger[35];

        private SwagOMeterAwardEngine _swagOMeterAwardEngine;
        private CharacterToBitMapConverter _characterToBitMapConverter;
        private IWinnersSource _winnersSource;
        private int _onOffImageWidth;
        private int _onOffImageHeight;
        private Bitmap _bitmap;

        public MainForm()
        {
            InitializeComponent();

            BuildSwagOMeterEngine();
            PositionEverything();

            _characterToBitMapConverter = new CharacterToBitMapConverter(Properties.Resources.on as Bitmap, Properties.Resources.off as Bitmap, pictureBox.Width / Constants.Columns, pictureBox.Height / 35);
            _bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = _bitmap;

            InitialiseTimer();

            Cursor.Hide();
        }

        private void BuildSwagOMeterEngine()
        {
            var fileDetailProvider = FileDetailProvider.Create(Settings.Default.FileLocation, "Swag-Winners-{0}.xml");
            var errorMessage = new DisplayErrorMessages();
            var attendeeSource = new AttendeeSource(errorMessage);
            var swagSource = new SwagSource(errorMessage);
            _winnersSource = new WinnersSource(fileDetailProvider);
            _swagOMeterAwardEngine = new Swagometer.Lib.Objects.SwagOMeterAwardEngine(Settings.Default.FileLocation, attendeeSource, swagSource, "attendees.xml", "swag.xml");
        }

        private void Form_Load(object sender, System.EventArgs e)
        {
            DisplayCurrentBitPatterns();
            _matrixTransformer = MatrixTransformer.Create<StartupTransformer>(_currentBitPatterns);
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
            _onOffImageHeight = pictureBox.Height / 35;

            pictureBox.Width = _onOffImageWidth * Constants.Columns;
            pictureBox.Height = _onOffImageHeight * 35;
            pictureBox.BackColor = Color.Black;

            pictureBox.Left = (screenArea.Width - pictureBox.Width) / 2;
            pictureBox.Top = (screenArea.Height - pictureBox.Height);
        }

        private void PickWinnerAndDisplay()
        {
            var winner = _swagOMeterAwardEngine.AwardSwag();
            if (winner != null)
            {
                _currentBitPatterns = _characterToBitMapConverter.GetBitPattern(winner.WinningAttendee.Name, "*** wins ***", winner.AwardedSwag.Company, winner.AwardedSwag.Thing);
                _matrixTransformer = MatrixTransformer.Create<WinnerTransformer>(_currentBitPatterns);
                StartTransform(100);
            }
            else
            {
                _matrixTransformer = MatrixTransformer.Create<GameOverTransformer>();
                StartTransform(1000);
            }
        }

        private void StartTransform(int initialDelayMS)
        {
            _timer.Interval = initialDelayMS;
            _timer.Enabled = true;
        }

        private void DisplayCurrentBitPatterns()
        {
            using (var bitmapGraphics = Graphics.FromImage(_bitmap))
            {
                _characterToBitMapConverter.BuildBitMapPicture(_currentBitPatterns, _onOffImageWidth, _onOffImageHeight, bitmapGraphics);
            }
            pictureBox.Invalidate();
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            PickWinnerAndDisplay();
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            _swagOMeterAwardEngine.SaveWinners(_winnersSource);
        }

        void _timer_Tick(object sender, System.EventArgs e)
        {
            _timer.Enabled = false;
            _currentBitPatterns = _matrixTransformer.Transform();
            DisplayCurrentBitPatterns();
            if (_timer.Enabled = _matrixTransformer.KeepTimerRunning)
            {
                _timer.Interval = _matrixTransformer.SubsequentDelayMs;
            }
        }
    }
}