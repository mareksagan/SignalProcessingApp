namespace SignalProcessingApp.ViewModel
{

    public partial class MainViewModel
    {
        /// <summary>
        /// Method invoked by the SquareRadioButtonCommand
        /// </summary>
        public void Square()
        {
            SignalGen.SetSquare();
        }
    }

}