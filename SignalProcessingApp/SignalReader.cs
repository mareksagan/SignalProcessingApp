using System;
using System.IO.Ports;

namespace SignalProcessingApp
{
    /// <summary>
    /// Signal reading singleton class
    /// </summary>
    sealed class SignalReader : ISignalManipulator
    {
        /// <inheritdoc/>
        public SerialPort Port { get; set; }
        /// <inheritdoc/>;
        public string PortName { get; set; }
        /// <summary>
        /// Property used to implement the Singleton design pattern.
        /// Indicates if there is only an existing instance of the class
        /// </summary>
        private static readonly SignalReader Instance = new SignalReader();
        /// <summary>
        /// Private class constructor (Singleton pattern implementation)
        /// </summary>
        private SignalReader()
        {
            PortName = "COM100";
            OpenConnection();
        }
        /// <summary>
        /// Class destructor invoking the CloseConnection method
        /// </summary>
        ~SignalReader()
        {
            CloseConnection();
        }
        /// <summary>
        /// Factory method for the SignalReader class
        /// </summary>
        /// <returns></returns>
        public static SignalReader GetSignalReader()
        {
            return Instance;
        }
        /// <summary>
        /// Reads a value from the specified serial port
        /// </summary>
        /// <returns></returns>
        public double ReadValue()
        {
            return double.Parse(Port.ReadLine());
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public void CloseConnection()
        {
            Port.Close();
        }
    }
}
