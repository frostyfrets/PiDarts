using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PiDarts.Windows.Menu
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class PiDartsApp : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            // Application is running 
            // Process command line args 
            bool startMinimized = false;
            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i] == "/StartMinimized")
                {
                    startMinimized = true;
                }
            }

            // Create main application window, starting minimized if specified
            PiDartsMainMenuWindow mainWindow = new PiDartsMainMenuWindow();
            if (startMinimized)
            {
                mainWindow.WindowState = WindowState.Minimized;
            }
            mainWindow.Show();
        }
    }
}
