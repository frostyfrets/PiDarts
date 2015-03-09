using PiDarts.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PiDarts.Core.Interfaces;
using System.Runtime.InteropServices;
using System.Threading;
using PiDarts.Core.DartboardReaders;
using PiDarts.Core.Enums;

namespace PiDarts.Windows.Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PiDartsMainMenuWindow : Window
    {
        //Populated w/ USB ports
        private ObservableCollection<string> portList;
        private IDartboardReader dbReader;
        private Thread gameThread;
        //Actual game object
        private PiDartsGame piDartsGame;
        private bool isRunning = false;

        public PiDartsMainMenuWindow()
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\codes.txt"))
            {
                MessageBox.Show("File 'codes.txt' not found in working directory!\nPlease place calibration file next to executable and restart.", "PiDarts");
                Application.Current.Shutdown();
            }

            InitializeComponent();
            comboComPort.ItemsSource = detectAvailablePorts();
        }
        /// <summary>
        /// Queries for all available ports.
        /// </summary>
        private ObservableCollection<string> detectAvailablePorts()
        {
            portList = new ObservableCollection<string>();
            foreach (string port in SerialPort.GetPortNames()) {
                portList.Add(port);
            }
            return portList;
        }

        /// <summary>
        /// Start a new dartgame
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lock (this)
            {
                if (gameThread == null)
                {
                    comboComPort.IsEnabled = false;
                    string port = comboComPort.Text;
                    int numPlayers = Convert.ToInt32(comboNumPlayers.Text);
                    GameSelection gameSelection = GameSelectionUtilities.convertPrettyNameToEnum(comboGameType.Text);

                    try
                    {
                        dbReader = new USBDartBoardReader(port);
                    }
                    catch (ArgumentException _exception)
                    {
                        comboComPort.IsEnabled = true;
                        MessageBox.Show("Invalid port specified!");
                        return;
                    }
                    catch (System.IO.IOException _exception) {
                        comboComPort.IsEnabled = true;
                        MessageBox.Show(String.Format("Could not open port: {0}",port));
                        return;
                    }
                    
                    gameThread = new Thread(() => initializeAndRun(port, numPlayers, gameSelection));
                    gameThread.Start();
                }
                else
                {
                    if (isRunning)
                    {
                        piDartsGame.SetUpNewGame(GameSelectionUtilities.convertPrettyNameToEnum(comboGameType.Text), Convert.ToInt32(comboNumPlayers.Text));
                    }
                }
            }
        }

        /// <summary>
        /// Game must be initialized and ran on the same thread.
        /// </summary>
        private void initializeAndRun(string _port, int _numPlayers,GameSelection _gameSelection) {
 
            piDartsGame = new PiDartsGame(dbReader);
            piDartsGame.SetUpNewGame(_gameSelection,_numPlayers);
            isRunning = true;
            piDartsGame.Run();
        }
    }
}
