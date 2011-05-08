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
        private static ShellWindow _Instance;
        ShellWindowViewModel objMainWindowViewModel;
        private static volatile object _Lock = new object();
        public static ShellWindow Instance()
        {
            if (_Instance == null)
            {
                try
                {
                    lock (_Lock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new ShellWindow();
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            return _Instance;
        }


        public ShellWindow()
        {
            objMainWindowViewModel = new ShellWindowViewModel();
            this.DataContext = objMainWindowViewModel;
            this.InitializeComponent();

            _Instance = this;

        }
    }
}
