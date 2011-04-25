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
using FeserWard.Controls;

namespace PACT.VIEW
{
    /// <summary>
    /// Interaction logic for DefaultScreen.xaml
    /// </summary>
    public partial class DefaultScreen : UserControl
    {

        public IIntelliboxResultsProvider SingleColumnResults
        {
            get;
            private set;
        }

        public DefaultScreen()
        {
            SingleColumnResults = new SingleColumnResultsProvider();
            this.InitializeComponent();
        }
    }
}
public class SingleColumnResultsProvider : IIntelliboxResultsProvider
{

    private List<string> _results;
    private int _numEach = 10;

    private void ConstructDataSource()
    {
        if (_results == null)
        {

            var temp = Enumerable.Range(0, 26 * _numEach).Select(i =>
            {
                var count = i % _numEach + 1;
                var charNum = (i / _numEach) % 26;
                char ch = Convert.ToChar(charNum + Convert.ToInt32('a'));
                return "".PadRight(count, ch);
            });

            _results = Sort(temp).ToList();
        }
    }

    protected virtual IEnumerable<string> Sort(IEnumerable<string> preResults)
    {
        return preResults.OrderByDescending(s => s.Length);
    }

    public IEnumerable<object> DoSearch(string searchTerm, int maxResults, object tag)
    {
        ConstructDataSource();
        return _results.Where(term => term.StartsWith(searchTerm)).Take(maxResults).Cast<object>();
    }
}