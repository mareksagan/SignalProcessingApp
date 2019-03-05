using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OxyPlot;
using System;
using System.Threading;
using System.IO.Ports;
using OxyPlot.Axes;
using System.Diagnostics;
using OxyPlot.Series;

namespace SignalProcessingApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public partial class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Property describing the update interval of the plot refreshing methods
        /// </summary>
        private const int UpdateInterval = 10;

        /// <summary>
        /// Field used for measuring the elapsing time in plot refreshing
        /// </summary>
        private readonly Timer Timer;
        
        /// <summary>
        /// Property representing the LineSeries of the plot
        /// </summary>
        public LineSeries Series { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the MainViewModel class
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            Timer = new Timer(OnTimerElapsed);

            SetupModel();

            try
            {
                SignalGen = StartSignalGenerator();
                SignalRed = StartSignalReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            SineRadioButtonCommand = new RelayCommand(Sine);
            TriangularRadioButtonCommand = new RelayCommand(Triangular);
            SquareRadioButtonCommand = new RelayCommand(Square);
        }
        
        /// <summary>
        /// Method which sets up the PlotModel of the plot
        /// </summary>
        private void SetupModel()
        {
            Timer.Change(Timeout.Infinite, Timeout.Infinite);

            PlotModel = new PlotModel();

            PlotModel.Title = "Input signal plot";

            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -3,
                Maximum = 3,
                IsZoomEnabled = false
            });
            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false
            });

            PlotModel.Series.Add(new LineSeries
            {
                LineStyle = LineStyle.Solid,
                StrokeThickness = 0.5,
                Color = OxyColors.Red,
                MarkerType = MarkerType.Diamond
            });

            Series = (LineSeries) PlotModel.Series[0];

            RaisePropertyChanged("PlotModel");

            Timer.Change(1000, UpdateInterval);
        }

        /// <summary>
        /// Event handler for the Timer class.
        /// Refreshes the plot at a specified rate
        /// </summary>
        private void OnTimerElapsed(object state)
        {
            lock (PlotModel.SyncRoot)
            {
                update();
            }

            PlotModel.InvalidatePlot(true);
        }

        /// <summary>
        /// Method which updates the plot with new DataPoints
        /// </summary>
        private void update()
        {
            int n = 0;

            Series = (LineSeries)PlotModel.Series[0];

            double x = Series.Points.Count > 0 ? Series.Points[Series.Points.Count - 1].X + 1 : 0;
            if (Series.Points.Count >= 50)
                Series.Points.RemoveAt(0);
            double y = SignalRed.readValue();
            
            Series.Points.Add(new DataPoint(x, y));

            n += Series.Points.Count;

            if (TotalNumberOfPoints != n)
            {
                TotalNumberOfPoints = n;
                RaisePropertyChanged("TotalNumberOfPoints");
            }
        }

        /// <summary>
        /// SignalGenerator class field
        /// </summary>
        private SignalGenerator SignalGen;

        /// <summary>
        /// SignalReader class field
        /// </summary>
        private SignalReader SignalRed;

        /// <summary>
        /// Wrapper method instantiating an object of the SignalGenerator class
        /// </summary>
        private SignalGenerator StartSignalGenerator()
        {
            return SignalGenerator.getSignalGenerator();
        }

        /// <summary>
        /// Wrapper method instantiating an object of the SignalReader class
        /// </summary>
        private SignalReader StartSignalReader()
        {
            return SignalReader.getSignalReader();
        }

        /// <summary>
        /// Field used for an easier access to the plot properties (MVVM)
        /// </summary>
        public PlotModel PlotModel { get; private set; }

        /// <summary>
        /// Property describing the number of total points on the plot
        /// </summary>
        public int TotalNumberOfPoints { get; private set; }
    }
}