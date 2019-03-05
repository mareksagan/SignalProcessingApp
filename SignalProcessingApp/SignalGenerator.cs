using System;
using System.Globalization;
using System.IO.Ports;
using System.Threading;
using SignalProcessingApp.Properties;

namespace SignalProcessingApp
{
    /// <summary>
    /// Signal generating singleton class
    /// </summary>
    sealed class SignalGenerator : ISignalManipulator
    {
        /// <summary>
        /// Sample rate property used for signal generation
        /// </summary>
        public int SampleRate { get; set; }

        /// <summary>
        /// Amplitude property used for signal generation
        /// </summary>
        public int Amplitude { get; set; }
        
        /// <summary>
        /// Frequency property used for signal generation
        /// </summary>
        public int Frequency { get; set; }
        
        /// <summary>
        /// Property indicating the number of serial port write operations,
        /// used for signal generation (concept of flowing time)
        /// </summary>
        private int Time { get; set; }
        
        /// <summary>
        /// Delegate describing the mathematical function currently used
        /// for signal generation
        /// </summary>
        private Func<double> Signal { get; set; }
        
        /// <inheritdoc/>
        public SerialPort Port { get; set; }
        
        /// <inheritdoc/>
        public string PortName { get; set; }
        
        /// <summary>
        /// Property used to implement the Singleton design pattern.
        /// Indicates if there is only an existing instance of the class
        /// </summary>
        private static readonly SignalGenerator Instance = new SignalGenerator();
        
        /// <summary>
        /// Private class constructor (Singleton pattern implementation)
        /// </summary>
        private SignalGenerator()
        {
            SampleRate = 8000;
            Amplitude = 2;
            Frequency = 1000;
            Time = 0;
            PortName = "COM99";
            openConnection();
            setSine();
            generateSignal();
        }
        
        /// <summary>
        /// Class destructor invoking the CloseConnection method
        /// </summary>
        ~SignalGenerator()
        {
            closeConnection();
        }
        
        /// <summary>
        /// Factory method of the SignalReader class
        /// </summary>
        /// <returns></returns>
        public static SignalGenerator getSignalGenerator()
        {
            return Instance;
        }
        
        /// <inheritdoc/>
        public void openConnection()
        {
            Port = new SerialPort();

            try
            {
                Port.PortName = PortName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            Port.Open();
        }
        
        /// <inheritdoc/>
        public void closeConnection()
        {
            Port.Close();
        }
        
        /// <summary>
        /// Runs a thread which continuously generates the signal to the specified serial port
        /// </summary>
        private void generateSignal()
        {
            if (Port.IsOpen)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    do
                    {
                        Port.WriteLine(Signal().ToString(CultureInfo.InvariantCulture));
                        Time++;

                    } while (true);
                }).Start();

            }
            else
            {
                Console.WriteLine(Resources.SignalGenerator_GenerateSignal_COM+PortName);
            }
        }
        
        /// <summary>
        /// Sets the Signal delegate to a sine function
        /// </summary>
        public void setSine()
        {
            Signal = null;
            Signal = () => Amplitude * Math.Sin(2 * Math.PI * Time * Frequency / SampleRate);
        }
        
        /// <summary>
        /// Sets the Signal delegate to a triangular function
        /// </summary>
        public void setTriangular()
        {
            Signal = null;
            Signal = () => 1.3 * Math.Abs(Time % 6 - 3) - 2;
        }
        
        /// <summary>
        /// Sets the Signal delegate to a square function
        /// </summary>
        public void setSquare()
        {
            Signal = null;
            Signal = () => 256 * Time / Frequency % 2 == 0 ? -Amplitude : Amplitude;
        }

    }
}
