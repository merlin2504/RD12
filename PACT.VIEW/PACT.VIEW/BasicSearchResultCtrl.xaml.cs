using System.Windows;
using System.Windows.Controls;
using FeserWard.Controls;

namespace PACT.VIEW
{
    /// <summary>
    /// Interaction logic for BasicSearchResultCtrl.xaml
    /// </summary>
    public partial class BasicSearchResultCtrl : UserControl
    {
        public static readonly DependencyProperty ProviderProperty =
            DependencyProperty.Register("Provider", typeof(IIntelliboxResultsProvider), typeof(BasicSearchResultCtrl), new UIPropertyMetadata(null));

        public IIntelliboxResultsProvider Provider
        {
            get
            {
                return (IIntelliboxResultsProvider)GetValue(ProviderProperty);
            }
            set
            {
                SetValue(ProviderProperty, value);
            }
        }

        public BasicSearchResultCtrl()
        {
            InitializeComponent();
        }
    }
}
