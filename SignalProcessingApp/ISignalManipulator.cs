using System.IO.Ports;

namespace SignalProcessingApp
{
    /// <summary>
    /// Signal manipulation interface
    /// </summary>
    interface ISignalManipulator
    {
        /// <summary>
        /// Field used for handling serial port IO operations
        /// </summary>
        SerialPort Port { get; set; }
        
        /// <summary>
        /// Property describing the used serial port's name
        /// </summary>
        string PortName { get; set; }
        
        /// <summary>
        /// Wrapper method which opens the connection to the specified serial port
        /// </summary>
        void openConnection();

        /// <summary>
        /// Wrapper method which closes the connection to the specified serial port
        /// </summary>
        void closeConnection();

    }
}