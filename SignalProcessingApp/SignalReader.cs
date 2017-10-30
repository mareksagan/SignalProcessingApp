using System;
using System.IO.Ports;

namespace SignalProcessingApp
{
    sealed class SignalReader : ISignalManipulator
    {
        public SerialPort Port { get; set; }
        public string PortName { get; set; }

        private static readonly SignalReader Instance = new SignalReader();

        private SignalReader()
        {
            PortName = "COM100";
            OpenConnection();
        }

        ~SignalReader()
        {
            CloseConnection();
        }
        public static SignalReader GetSignalReader()
        {
            return Instance;
        }

        public double ReadValue()
        {
            return double.Parse(Port.ReadLine());
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
    }
}
