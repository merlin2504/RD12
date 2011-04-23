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
using PACT.VIEWMODEL;


namespace PACT.VIEW
{
    /// <summary>
    /// Interaction logic for BindRibbon.xaml
    /// </summary>
    public partial class ShellWindow
    {
        ShellWindowViewModel objMainWindowViewModel;
        public ShellWindow()
        {
            this.InitializeComponent();
            objMainWindowViewModel = new ShellWindowViewModel();
            this.DataContext = objMainWindowViewModel;
        }
    }
}
