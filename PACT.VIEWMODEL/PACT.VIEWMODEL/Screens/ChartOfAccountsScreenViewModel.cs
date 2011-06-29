using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;
using System.Windows.Resources;
using System.Windows;
using System.Data;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PACT.MODEL;
using PACT.COMMON;
using Microsoft.Practices.Prism.Commands;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Remoting.Messaging;

namespace PACT.VIEWMODEL
{
    public class ChartOfAccountsScreenViewModel : WorkspaceViewModel
    

    {
    public DataTable Data
        {
            get
            {
                return _data;
            }

            set
            {
                if (_data != value)
                {
                    _data = value;
                    base.OnPropertyChanged("Data");
                }
            }
        }
        private DataTable _data;

        public DataSet Dataset
        {
            get
            {
                return _dataset;
            }

            set
            {
                if (_dataset != value)
                {
                    _dataset = value;
                    base.OnPropertyChanged("Dataset");
                }
            }
        }
        private DataSet _dataset;


        public DataTable TempData
        {
            get
            {
                return _tempdata;
            }

            set
            {
                _tempdata = value;
                base.OnPropertyChanged("TempData");
            }
        }
        private DataTable _tempdata;

        public bool VerticalLine
        {
            get
            {
                return _verticalline;
            }

            set
            {
                if (_verticalline != value)
                {
                    _verticalline = value;
                    base.OnPropertyChanged("VerticalLine");
                }
            }
        }
        private bool _verticalline;

        public bool HorizontalLine
        {
            get
            {
                return _horizontalline;
            }

            set
            {
                if (_horizontalline != value)
                {
                    _horizontalline = value;
                    base.OnPropertyChanged("HorizontalLine");
                }
            }
        }
        private bool _horizontalline;


        public Color ItemColor
        {
            get
            {
                return _ItemColor;
            }

            set
            {
                if (_ItemColor != value)
                {
                    _ItemColor = value;
                    base.OnPropertyChanged("ItemColor");
                }
            }
        }
        private Color _ItemColor;


        public SolidColorBrush SelectedItemColorBrush
        {
            get
            {
                return new SolidColorBrush(SelectedItemColor);
            }
        }

        public Color SelectedItemColor
        {
            get
            {
                return _SelectedItemColor;
            }

            set
            {
                if (_SelectedItemColor != value)
                {
                    _SelectedItemColor = value;
                    base.OnPropertyChanged("SelectedItemColor");
                }
            }
        }
        private Color _SelectedItemColor;

        public Color MovingItemColor
        {
            get
            {
                return _MovingItemColor;
            }

            set
            {
                if (_MovingItemColor != value)
                {
                    _MovingItemColor = value;
                    base.OnPropertyChanged("MovingItemColor");
                }
            }
        }
        private Color _MovingItemColor;

        public Color GroupItemColor
        {
            get
            {
                return _GroupItemColor;
            }

            set
            {
                if (_GroupItemColor != value)
                {
                    _GroupItemColor = value;
                    base.OnPropertyChanged("GroupItemColor");
                }
            }
        }
        private Color _GroupItemColor;

        public string text
        {
            get;
            set;
        }
        
        private TreeNode _nodetoMove;
        public TreeNode TreeNodetoMove
        {
            get
            {
                return _nodetoMove;
            }

            set
            {
                if (_nodetoMove == value)
                    return;
                _nodetoMove = value;
                base.OnPropertyChanged("TreeNodetoMove");
            }
        }

        private TreeNode _selectedNode;

        public TreeNode SelectedNode
        {
            get
            {
                return _selectedNode;
            }

            set
            {
                if (_selectedNode == value)
                    return;
                _selectedNode = value;
                base.OnPropertyChanged("SelectedNode");
            }
        }


        public Color altColor1
        {
            get
            {
                return _altColor1;
            }

            set
            {
                if (_altColor1 != value)
                {
                    _altColor1 = value;
                    base.OnPropertyChanged("altColor1");
                }
            }
        }
        private Color _altColor1;

        public Color altColor2
        {
            get
            {
                return _altColor2;
            }

            set
            {
                if (_altColor2 != value)
                {
                    _altColor2 = value;
                    base.OnPropertyChanged("altColor2");
                }
            }
        }
        public double RowHeight
        {
            get
            {
                return _rowheight;
            }

            set
            {
                if (_rowheight != value)
                {
                    _rowheight = value;
                    base.OnPropertyChanged("RowHeight");
                }
            }
        }
        private double _rowheight;
        private Color _altColor2;
        public DelegateCommand<string> DynamicCommand { get; set; }

        public List<string> CMenu { get; set; }
        int RowCount = 0;
        int CostCenterID, Companyindex, UserID;

        public ChartOfAccountsScreenViewModel(int costcenterID, int companyindex, int userID)
        {
            CostCenterID = costcenterID;
            Companyindex = companyindex;
            UserID = userID;

            Dataset = new Ptree().GetCostCenterGridViewList(Companyindex, CostCenterID, UserID);

            #region Commented


            //System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();

            //xDoc.LoadXml(dt.Rows[0]["TreeXML"].ToString());

            //System.Xml.XmlNodeList xList = xDoc.SelectNodes("GridXML/Columns/TreeColumn");
            //Columns = new ObservableCollection<ColumnDescriptor>();

            //for (short i = 0; i < xList.Count; i++)
            //{

            //    TreeColumn objPColumn = (TreeColumn)PACTSerializer.FromXml(xList[i].OuterXml, typeof(TreeColumn));

            //    if (i == 0)
            //        strCols = objPColumn.Name;
            //    else
            //        strCols = strCols + "," + objPColumn.Name;
            //    //  
            //    Columns.Add(new ColumnDescriptor() { align = objPColumn.Allignment, width = objPColumn.Width, HeaderText = objPColumn.Label, DisplayMember = "Field" + (i + 1).ToString() });
            //}
            #endregion

            DynamicCommand = new DelegateCommand<string>(CommandController);
            CMenu = new List<string>();
            CMenu.Add("Add");
            CMenu.Add("Edit");
            CMenu.Add("Move");
            CMenu.Add("delete");

            GroupItemColor = Colors.Black;
            ItemColor = Colors.Gray;
            SelectedItemColor = Colors.Green;
            MovingItemColor = Colors.Red;            
        }
         
        public int GridViewID
        {
            get
            {
                return gridviewid;
            }

            set
            {
                if (gridviewid != value)
                {
                    gridviewid = value;

                    GetData();

                    base.OnPropertyChanged("GridViewID");
                }
            }
        }
        private int gridviewid;

        public string strCols
        {
            get
            {
                return _strcols;
            }

            set
            {
                if (_strcols != value)
                {
                    _strcols = value;

                    base.OnPropertyChanged("strCols");
                }
            }
        } 
        private string _strcols;


        void GetData()
        {
            DataSet ds = new Ptree().GetCostCenterSummary(Companyindex, GridViewID, strCols, -1, 1001);
            Data = ds.Tables[0];

            RowCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            BackgroundWorker process = new BackgroundWorker();
            process.DoWork += new DoWorkEventHandler(process_DoWork);           
            process.RunWorkerAsync();
            TempData = Data;
        }

        void process_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= RowCount / 1000; i++)
            {
                DataTable dt = new Ptree().GetCostCenterSummary(Companyindex, GridViewID, strCols, i * 1000, 1000).Tables[0];
                Data.Merge(dt, true, MissingSchemaAction.Add);
                TempData = dt;
            }
        }


        public void CommandController(object sender)
        {
            if (SelectedNode != null)
            {
                switch ((string)sender)
                {
                    case "Add":
                        System.Windows.MessageBox.Show("Add Clicked for " + SelectedNode.Field1.ToString());

                        break;
                    case "Edit":
                        System.Windows.MessageBox.Show("Edit Clicked for " + SelectedNode.Field1.ToString());
                        break;
                    case "delete":
                        System.Windows.MessageBox.Show("delete Clicked for " + SelectedNode.Field1.ToString());
                        break;
                    case "Move":
                        if (TreeNodetoMove != null)
                        {
                            System.Windows.MessageBox.Show("Move Clicked for " + TreeNodetoMove.Field1.ToString() + "\nto " + SelectedNode.Field1.ToString());

                        }
                        else
                            System.Windows.MessageBox.Show("NO node to move");
                        break;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Select a node");
            }

        }
        

    }
}
