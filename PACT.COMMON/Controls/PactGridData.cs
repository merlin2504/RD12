using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;

namespace PACT.COMMON
{
    public class PactGridData : PactControlData
    {
        public DataTable DataSource { get; set; }
        public ObservableCollection<DatagridCols> Columns { get; set; }       
    }

    public class DatagridCols
    {
        public string HeaderText { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
        public string ValueBinding { get; set; }
        public string ColumnType { get; set; }
        public int width { get; set; }
        public DataView Source { get; set; }
        //public List<ColumnDescriptor> Columns { get; set; }

    }

    //public class ColumnDescriptor
    //{
    //    public string HeaderText { get; set; }
    //    public string DisplayMember { get; set; }
    //}
    
}
