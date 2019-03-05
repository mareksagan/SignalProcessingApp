namespace SignalProcessingApp.ViewModel
{

    public partial class MainViewModel
    {
        /// <summary>
        /// Method invoked by the SineRadioButtonCommand
        /// </summary>
        public void Sine()
        {
            SignalGen.setSine();
        }
    }
    
}
