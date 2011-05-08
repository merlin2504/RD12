using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PACT.VIEW
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        ShellWindow shell;
        public Login()
        {
            InitializeComponent();
            LoginProcessing.Visibility = Visibility.Hidden;
            LoginProcessing.Processing_BeginStoryboard.Storyboard.Stop(LoginProcessing);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Write code here to authenticate user
            // If authenticated, then set DialogResult=true

            LoginProcessing.Visibility = Visibility.Visible;
            LoginScreen.Visibility = Visibility.Hidden;
            shell = ShellWindow.Instance();
            shell.Show();
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
