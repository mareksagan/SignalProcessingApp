# SignalProcessingApp
Sample WPF application generating/reading signal and displaying it in real time

## Description
The user picks the generated signal kind in GUI `RadioButtons`. The signal gets generated by a SignalGenerator instance,
which sends the signal via a virtual COM port. At the same time, the ViewModel utilises the SignalReader class instance
to read the values from the paired COM port. The values are displayed in real time on a scrollable plot.

## Installation
* Install this `https://www.eltima.com/download/vspd.exe`
* Go to the `Manage ports` tab and choose `COM99` in `First port:` and `COM100` in `Second port:`
* You can change the default COM ports in the `SignalGenerator`/`SignalReader` constructor/portName property and restart the connection
* Click the `Add Pair` button and start the application

## Technologies used
* Windows Presentation Foundation
* XAML
* Model-View-ViewModel design pattern (MVVM Light, Command, Data Binding)
* Singleton design pattern
* Factory design pattern
* Facade design pattern
* Multithreaded programming
* Serial ports communication (virtual COM ports via Eltima Virtual Serial Port Driver)
* Inversion of Control (SimpleIoc)
* OxyPlot (PlotModel, real-time plotting)
* Interfaces
* Lambda expressions
* Delegates
* Version control (Git for Visual Studio)
* JetBrains Resharper
* C# Documenter (XML)
* Sandcastle (Website)
