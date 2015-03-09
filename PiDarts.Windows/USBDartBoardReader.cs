using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using PiDarts.Core.DartboardReaders;
using PiDarts.Core;
using System.Threading;
using System.IO.Ports;
using System.Media;
using System.IO;

namespace PiDarts.Windows
{
    class USBDartBoardReader : IDartboardReader
    {
        //Deadhit is returned by default when we haven't read a hit
        private static Hit deadHit = new Hit { modifier = -1, value = -1};
        private static Hit nextHit = deadHit;

        //lockObj synchronizes access to the 'nextHit' variable
        private static object lockObj = new object();

        //Runs Asynchronously to poll the USB Port
        private Thread pollThread;
        private SerialPort port;

        //Used to decode dartboard readings.
        //TODO: Store these as IHits to save performance
        public static string[] codes = new string[79];

        //Used for calibrating the dartboard
        private static int index = 0;
        private static int mod = 1;

        public USBDartBoardReader(string _port)
        {
            ConfigureUSBPort(_port);
            
            //Read in a previously calibrated text file.
            codes = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + @"\codes.txt");

        }

        /// <summary>
        /// Synchronously checks to see if a new hit has been read.
        /// When a new hit is read, next hit values will be > 0.
        /// Does not block;
        /// </summary>
        public Hit ReadDartboardHit()
        {
            lock(lockObj){
                if(nextHit.modifier > 0){
                    var tempHit = new Hit { modifier = nextHit.modifier, value = nextHit.value };
                    nextHit.modifier = -1;
                    nextHit.value = -1;  
                    return tempHit;
                }
            }
                return deadHit;
        }

        /// <summary>
        /// Registers an async handler to update the nextHit variable when byte is read from USB.
        /// When a new value is received from port, next hit values will be > 0.
        /// </summary>
        private void ConfigureUSBPort(string _port) {
            //TODO: Make these settings variable.

            port = new SerialPort(_port, 9600, Parity.None, 8, StopBits.One);
            port.DtrEnable = true;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            port.Open();
        }

        /// <summary>
        /// Calibrates the dartboard. No need for this method once it is calibrated once.
        /// </summary>
        private static void Calibrate(
                    object sender,
                    SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int val = sp.ReadByte();
            codes[val] = String.Format("{0}|{1}", mod, index + 1);
            mod++;
            if (mod > 3) {
                mod = 1;
                index++;
            }
            SystemSounds.Beep.Play();
            if (index == 20 && mod == 3) {
                System.IO.File.WriteAllLines(Directory.GetCurrentDirectory() + @"\codes.txt",codes);
            }
        }

        /// <summary>
        /// Invoked when new data arrives over USB port.
        /// </summary>
       private static void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e){
           lock (lockObj) {
               if (nextHit.modifier > 0) {
                   //Sleep to give other threads a chance to run.
                   Thread.Sleep(25);
                   return;
               }
           }  
       
            //Read value and get high/low bits
            SerialPort sp = (SerialPort)sender;
            int val = sp.ReadByte();
            int row = val & 15;     
            int col = (val >> 3);

            //Get the modifier and value for this code from our calibrated array
            string[] modAndVal = codes[val].Split('|'); 
            lock (lockObj) {
                if (nextHit.modifier < 0) {
                    nextHit.modifier = Convert.ToInt32(modAndVal[0]);
                    nextHit.value = Convert.ToInt32(modAndVal[1]);
                    return;
                }
            }


        }
    }
}

