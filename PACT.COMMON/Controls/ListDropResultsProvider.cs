using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeserWard.Controls;

namespace PACT.COMMON
{
    public class ListDropResultsProvider : IIntelliboxResultsProvider
    {

        private List<Person> _results;
        private int _numEach = 10;

        private void ConstructDataSource()
        {
            if (_results == null)
            {

                var temp = Enumerable.Range(0, 26 * _numEach).Select(i =>
                {
                    //var count = i % _numEach + 1;
                    //var charNum = (i / _numEach) % 26;
                    //char ch = Convert.ToChar(charNum + Convert.ToInt32('a'));
                    //return "".PadRight(count, ch);
                    var r = new Random(i);
                    return new Person()
                    {
                        PersonID = i,
                        FirstName = r.Next().ToString() + "'s firstname",
                        LastName = r.Next().ToString() + "'s lastname",
                        Age = r.Next(10, 200),
                        NetWorth = r.Next(10000, 10000000),
                        Weight = r.Next(100, 400)
                    };

                });

                _results = temp.ToList();
            }
        }

        protected virtual IEnumerable<string> Sort(IEnumerable<string> preResults)
        {
            return preResults.OrderByDescending(s => s.Length);
        }

        public IEnumerable<object> DoSearch(string searchTerm, int maxResults, object tag)
        {
            ConstructDataSource();
            return _results.Where(term => term.FirstName.Contains(searchTerm)).Take(10).Cast<object>();
            //return _results.Cast<object>();
        }
    }

    public class Person
    {
        public int PersonID
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public decimal NetWorth
        {
            get;
            set;
        }

        public int Weight
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("ID:{0}, FName:{1}, LName:{2}, Age:{3}, NetWorth:{4}, Weight:{5}",
                PersonID,
                FirstName ?? string.Empty,
                LastName ?? string.Empty,
                Age,
                NetWorth,
                Weight);
        }
    }
}
