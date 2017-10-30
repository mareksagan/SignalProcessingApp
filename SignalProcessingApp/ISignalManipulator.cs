using System.IO.Ports;

namespace SignalProcessingApp
{
    interface ISignalManipulator
    {
        SerialPort Port { get; set; }
        string PortName { get; set; }

        void OpenConnection();
        void CloseConnection();

    }
}