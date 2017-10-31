using GalaSoft.MvvmLight.Command;

namespace SignalProcessingApp.ViewModel
{
    public partial class MainViewModel
    {
        /// <summary>
        /// Command bound to the SineRadioButton
        /// </summary>
        public RelayCommand SineRadioButtonCommand { get; set; }
        /// <summary>
        /// Command bound to the TriangularRadioButton
        /// </summary>
        public RelayCommand TriangularRadioButtonCommand { get; set; }
        /// <summary>
        /// Command bound to the SquareRadioButton
        /// </summary>
        public RelayCommand SquareRadioButtonCommand { get; set; }

    }

}