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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PACT.VIEWMODEL;

namespace PACT.VIEW
{
    /// <summary>
    /// Interaction logic for RibbonControl.xaml
    /// </summary>

    public partial class RibbonControl : UserControl
    {
        RibbonViewModel objRibbonViewModel; 

        public RibbonControl()
        {
            this.InitializeComponent();
            objRibbonViewModel = new RibbonViewModel();
            this.Ribbon.DataContext = objRibbonViewModel;
        }
    }
}
