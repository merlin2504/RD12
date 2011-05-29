using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;
using Microsoft.Windows.Controls;
using System.Windows;

namespace PACT.COMMON
{
    public class PactGridData : PactControlData
    {
        public DataGrid Grid = null;
        public DataTable DataSource { get; set; }
        public ObservableCollection<DgColumn> Columns { get; set; }
    }

    public class DgColumn
    {
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
        public string ValueBinding { get; set; }

        public string Header { get; set; }
        public string Control { get; set; }
        public string DataType { get; set; }
        public string ID { get; set; }
        public string Param { get; set; }
        public int Width { get; set; }
        public bool ReadOnly { get; set; }
        public bool Mandatory { get; set; }
        public bool DisableTab { get; set; }        
        public string DefaultValue { get; set; }
        public string Formula { get; set; }


        //public DataView Source { get; set; }
       // public PactControlData Source { get; set; }
        //public List<ColumnDescriptor> Columns { get; set; }

        /// <summary>
        /// Gets the XML string for the serialized
        /// <see cref="PACTRequest" /> object.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public virtual string Xml
        {
            get
            {
                return PACTSerializer.ToXml(this, this.GetType(), false);
            }
        }

        /// <summary>
        /// Creates a new <see cref="PACTRequest" /> 
        /// object from an XML string.
        /// </summary>
        /// <param name="Xml">
        /// XML string to create the object from.</param>
        /// <returns>
        /// A <see cref="PACTRequest" /> object.
        /// </returns>
        public static DgColumn FromXml(string Xml)
        {
            return ((DgColumn)(PACTSerializer.FromXml(Xml, typeof(DgColumn))));
        }
    }

    //public class ColumnDescriptor
    //{
    //    public string HeaderText { get; set; }
    //    public string DisplayMember { get; set; }
    //}
    
}
