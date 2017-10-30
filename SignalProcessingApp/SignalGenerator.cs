using System;
using System.Globalization;
using System.IO.Ports;
using System.Threading;
using SignalProcessingApp.Properties;

namespace SignalProcessingApp
{
    sealed class SignalGenerator : ISignalManipulator
    {
        public int SampleRate { get; set; }
        public int Amplitude { get; set; }
        public int Frequency { get; set; }
        private int Time { get; set; }

        private Func<double> Signal { get; set; }

        public SerialPort Port { get; set; }
        public string PortName { get; set; }
       
        private static readonly SignalGenerator Instance = new SignalGenerator();

        private SignalGenerator()
        {
            SampleRate = 8000;
            Amplitude = 2;
            Frequency = 1000;
            Time = 0;
            PortName = "COM99";
            OpenConnection();
            SetSine();
            GenerateSignal();
        }

        ~SignalGenerator()
        {
            CloseConnection();
        }

        public static SignalGenerator GetSignalGenerator()
        {
            return Instance;
        }

        public void OpenConnection()
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

        public void CloseConnection()
        {
            Port.Close();
        }

        private void GenerateSignal()
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

        public void SetSine()
        {
            Signal = null;
            Signal = () => Amplitude * Math.Sin(2 * Math.PI * Time * Frequency / SampleRate);
        }

        public void SetTriangular()
        {
            Signal = null;
            Signal = () => 1.3 * Math.Abs(Time % 6 - 3) - 2;
        }

        public void SetSquare()
        {
            Signal = null;
            Signal = () => 256 * Time / Frequency % 2 == 0 ? -Amplitude : Amplitude;
        }

    }
}
