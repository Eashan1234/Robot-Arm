using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    class Init
    {
        private SerialPort arduino;

        private MainWindow MainForm;

        private string StatusTXT;

        public Init(SerialPort arduino1, MainWindow MainForm1, string StatusTXT1)
        {
            arduino = arduino1;
            MainForm = MainForm1;
            StatusTXT = StatusTXT1;
        }

        public SerialPort getArduino()
        {
            return this.arduino;
        }

        public MainWindow getMainForm()
        {
            return this.MainForm;
        }

        public string getStatusTXT()
        {
            return this.StatusTXT;
        }

        public string initArd()
        {

            try
            {
                arduino.Open();
                StatusTXT = "Connected succesfully!";
            }
            catch
            {
                StatusTXT = "Cannot find arduino.";
            }

            return StatusTXT;
        }

    }
}

