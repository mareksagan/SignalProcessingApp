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
            openConnection();
        }
        
        /// <summary>
        /// Class destructor invoking the CloseConnection method
        /// </summary>
        ~SignalReader()
        {
            closeConnection();
        }
        
        /// <summary>
        /// Factory method for the SignalReader class
        /// </summary>
        /// <returns></returns>
        public static SignalReader getSignalReader()
        {
            return Instance;
        }
        
        /// <summary>
        /// Reads a value from the specified serial port
        /// </summary>
        /// <returns></returns>
        public double readValue()
        {
            return double.Parse(Port.ReadLine());
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
    }
}
