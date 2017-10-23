using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessingApp
{
    class SignalGenerator
    {
        int sampleRate = 8000;
        double[] inputBuffer = new double[8000];
        String[] outputBuffer = new String[8000];
        double amplitude = 0.25 * short.MaxValue;
        double frequency = 1000;


    }
}
