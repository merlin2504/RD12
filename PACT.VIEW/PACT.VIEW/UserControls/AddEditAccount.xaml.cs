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
    /// Interaction logic for AddEditAccount.xaml
    /// </summary>
    public partial class AddEditAccount : UserControl
    {
        public AddEditAccount()
        {
            InitializeComponent();

            DetailsAddContact.Visibility = Visibility.Collapsed;
            DetailsAttachments.Visibility = Visibility.Collapsed;
        }

        private void ExpandDetailsEdit_StoryboardCompleted(object sender, EventArgs e)
        {

        }
    }
}
