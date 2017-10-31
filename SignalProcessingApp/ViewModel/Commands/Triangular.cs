namespace SignalProcessingApp.ViewModel
{

    public partial class MainViewModel
    {
        /// <summary>
        /// Method invoked by the TriangularRadioButtonCommand
        /// </summary>
        public void Triangular()
        {
            SignalGen.SetTriangular();
        }
    }

}
