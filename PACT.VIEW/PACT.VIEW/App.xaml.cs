using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using PACT.VIEWMODEL;

namespace PACT.VIEW
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Login loginWindow = new Login();
            ShellWindow shell;
            if (loginWindow.ShowDialog() ?? false)
            {
                this.ShutdownMode = ShutdownMode.OnMainWindowClose;

                shell = ShellWindow.Instance();
                this.MainWindow = shell;
                this.MainWindow.Show();
            }
            else
            {
                this.Shutdown();
            }
        }
    }


}
