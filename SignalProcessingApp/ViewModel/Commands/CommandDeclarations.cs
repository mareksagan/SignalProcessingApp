using GalaSoft.MvvmLight.Command;

namespace SignalProcessingApp.ViewModel
{
    public partial class MainViewModel
    {
        /// <summary>
        /// A class used for an easier access to the plot properties
        /// </summary>
        public RelayCommand SineRadioButtonCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand TriangularRadioButtonCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand SquareRadioButtonCommand { get; set; }

    }

}