using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;
using PACT.COMMON.CommonService;
using System.Xml;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;

namespace PACT.COMMON.Controls
{
     
     [DefaultProperty("Columns")]
    [ContentProperty("Columns")]
    public class PactComboBox : ComboBox
    {
        const string partPopupDataGrid = "PART_PopupDataGrid";
        //Columns of DataGrid
        private ObservableCollection<DataGridTextColumn> columns;
        //Attached DataGrid control
        private DataGrid popupDataGrid;

        public int CompanyDBIndex { get; set; }
        public string FeatureID { get; set; }

        string strTable, strPrimaryKey;
        string _Query = string.Empty;
        string _Where = string.Empty;
        string _FilterCol = string.Empty;
        int PageSize = 8;
        List<TreeColumn> oColumns;
        int SearchColumn { get; set; }
        int iWidth = 0;
        bool IstextChanged = false;

        bool valueSelected = false;

        static PactComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PactComboBox), new FrameworkPropertyMetadata(typeof(PactComboBox)));
        }

        //The property is default and Content property for PactComboBox
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<DataGridTextColumn> Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = new ObservableCollection<DataGridTextColumn>();
                }
                return this.columns;
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            
            CommonClient oDMCommon = new CommonClient();
            DataTable dt = oDMCommon.GetDataTable("SELECT * FROM AdmnLayoutLists WHERE ID=" + FeatureID, CompanyDBIndex.ToString());
            oDMCommon.Close();
            strTable = dt.Rows[0]["TableName"].ToString();
            strPrimaryKey = dt.Rows[0]["PrimaryKey"].ToString();
            _Where = dt.Rows[0]["WhereClause"].ToString();

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(dt.Rows[0]["LayoutXML"].ToString());

            XmlNodeList xList = xDoc.SelectNodes("XML/Columns/TreeColumn");
            TreeColumn oCol;
            oColumns = new List<TreeColumn>();

            _Query = "SELECT " + strPrimaryKey;

            this.IsEditable = true;
            DataGridTextColumn dgCol;

            dgCol = new DataGridTextColumn();
            dgCol.Header = strPrimaryKey;
            dgCol.Binding = new Binding(strPrimaryKey);
            dgCol.Visibility = System.Windows.Visibility.Hidden;
            this.Columns.Add(dgCol);


            for (int i = 0; i < xList.Count; i++)
            {
                oCol = (TreeColumn)PACTSerializer.FromXml(xList[i].OuterXml, typeof(TreeColumn));
                _Query += "," + oCol.Name;

                dgCol = new DataGridTextColumn();
                dgCol.Header = oCol.Label;
                dgCol.Width = oCol.Width;
                dgCol.Binding = new Binding(oCol.Name);
                iWidth += oCol.Width;
                this.Columns.Add(dgCol);
                oColumns.Add(oCol);
            }

            _Query += " FROM " + strTable;

            if (this.SelectedValue != null)
                valueSelected = true;

            //this.SelectedValue = new Binding(strPrimaryKey);
            this.SelectedValuePath = strPrimaryKey;

        }

        void SetSearchColumn(int Index)
        {

            SearchColumn = Index;
            if (oColumns[Index - 1].Name != _FilterCol)
            {
                _FilterCol = oColumns[Index - 1].Name;
                this.DisplayMemberPath = oColumns[Index - 1].Name;

                FillListBox();

                //TODO Styling
                //for (int i = 1; i < this.Columns.Count; i++)
                //{
                //    //  pnlPopDg.Columns[i].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 8);
                //    popupDataGrid.Columns[i].CellStyle.
                //}
                //pnlPopDg.Columns[Index].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

            }
        }

        //Apply theme and attach columns to DataGrid popupo control
        public override void OnApplyTemplate()
        {
           
            Popup partpopup = this.Template.FindName("PART_Popup", this) as Popup;
            partpopup.Width = iWidth + 10;


            if (popupDataGrid == null)
            {
                popupDataGrid = this.Template.FindName("PART_PopupDataGrid", this) as DataGrid;


                if (popupDataGrid != null)
                {
                    for (int i = 0; i < columns.Count; i++)
                        popupDataGrid.Columns.Add(columns[i]);

                    SetSearchColumn(1);

                    popupDataGrid.ItemsSource = this.ItemsSource;

                    //Add event handler for DataGrid popup
                    popupDataGrid.MouseUp += new MouseButtonEventHandler(popupDataGrid_MouseDown);
                    popupDataGrid.SelectionChanged += new SelectionChangedEventHandler(popupDataGrid_SelectionChanged);


                }
            }

            this.AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(OnTextChanged));
            
            IstextChanged = true;
            //Call base class method
            base.OnApplyTemplate();

            IstextChanged = false;
        }


        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {

            base.OnPreviewKeyDown(e);

            IstextChanged = true;

            if (e.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case System.Windows.Input.Key.D1:
                        SetSearchColumn(1);
                        break;
                    case System.Windows.Input.Key.D2:
                        if (columns.Count() > 2) SetSearchColumn(2);
                        break;
                    case System.Windows.Input.Key.D3:
                        if (columns.Count() > 3) SetSearchColumn(3);
                        break;
                    case System.Windows.Input.Key.D4:
                        if (columns.Count() > 4) SetSearchColumn(4);
                        break;
                    case System.Windows.Input.Key.D5:
                        if (columns.Count() > 5) SetSearchColumn(5);
                        break;
                    case System.Windows.Input.Key.D6:
                        if (columns.Count() > 6) SetSearchColumn(6);
                        break;
                    case System.Windows.Input.Key.D7:
                        if (columns.Count() > 7) SetSearchColumn(7);
                        break;
                    case System.Windows.Input.Key.D8:
                        if (columns.Count() > 8) SetSearchColumn(8);
                        break;
                    case System.Windows.Input.Key.D9:
                        if (columns.Count() > 9) SetSearchColumn(9);
                        break;
                }

                IstextChanged = false;
                return;
            }

            if (e.Key == Key.Down && this.SelectedIndex == PageSize - 1)
                getNextRow();
            else if (e.Key == Key.Up && this.SelectedIndex == 0)
                getPrevRow();

            IstextChanged = false;

        }

        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IstextChanged)
                return;

            FillListBox();

            Popup popup = this.Template.FindName("PART_Popup", this) as Popup;
            popup.IsOpen = true;

            if (popupDataGrid.SelectedItem != null && this.Text != ((DataRowView)popupDataGrid.SelectedItem).Row.ItemArray[SearchColumn].ToString())
            {
                for (int i = 0; i < popupDataGrid.Items.Count; i++)
                {
                    if (this.Text == ((DataRowView)popupDataGrid.Items[i]).Row.ItemArray[SearchColumn].ToString())
                    {
                        popupDataGrid.SelectedItem = popupDataGrid.Items[i];
                        return;
                    }

                }
            }
        }

        void FillListBox()
        {
            IstextChanged = true;

            DataTable Dt = null;
            SearchCriteria objSearch = new SearchCriteria();
            objSearch.Query = _Query;
            objSearch.WhereString = _Where;
            objSearch.SearchOn = _FilterCol;
            objSearch.SearchValue = this.Text;
            objSearch.MaximumRows = PageSize;
            objSearch.IsMoveUp = false;

            if (valueSelected)
            {
                objSearch.SelectedValueQuery = "select @SearchValue=" + _FilterCol + " from " + strTable + " where " + strPrimaryKey + " = " + this.SelectedValue.ToString();
                valueSelected = false;
            }

            CommonClient oDMCommon = new CommonClient();
            Dt = oDMCommon.GetDataTable_Search(objSearch, CompanyDBIndex.ToString());
            oDMCommon.Close();



            if (Dt != null && Dt.Rows.Count > 0)
            {
                if (this.ItemsSource == null)
                {
                    this.ItemsSource = Dt.AsDataView();
                    ((DataView)this.ItemsSource).Table.PrimaryKey = new DataColumn[] { ((DataView)this.ItemsSource).Table.Columns[0] };
                }
                else
                {

                    Dt.PrimaryKey = new DataColumn[] { Dt.Columns[0] };


                    for (int i = 0; i < ((DataView)this.ItemsSource).Table.Rows.Count; i++)
                    {
                        if (!Dt.Rows.Contains(((DataView)this.ItemsSource).Table.Rows[i][0]))
                        {
                            ((DataView)this.ItemsSource).Table.Rows.RemoveAt(i);
                            i--;
                        }
                    }

                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        if (((DataView)this.ItemsSource).Table.Rows.Contains(Dt.Rows[i][0]))
                            continue;

                        DataRow dr = ((DataView)this.ItemsSource).Table.NewRow();

                        for (int j = 0; j < ((DataView)this.ItemsSource).Table.Columns.Count; j++)
                        {
                            dr[j] = Dt.Rows[i][j];
                        }
                        if (((DataView)this.ItemsSource).Table.Rows.Count == i)
                            ((DataView)this.ItemsSource).Table.Rows.Add(dr);
                        else
                            ((DataView)this.ItemsSource).Table.Rows.InsertAt(dr, i);
                    }


                    //if (popupDataGrid.Items.Count > 0)
                    //    popupDataGrid.SelectedItem = popupDataGrid.Items[0];
                }




            }

            IstextChanged = false;
        }

        int getNextRow()
        {
            int i = 0;
            DataTable Dt = null;
            SearchCriteria objSearch = new SearchCriteria();
            objSearch.Query = _Query;
            objSearch.WhereString = _Where;
            objSearch.SearchOn = _FilterCol;
            objSearch.SearchValue = this.Text;
            objSearch.MaximumRows = 2;
            objSearch.IsMoveUp = false;
            CommonClient oDMCommon = new CommonClient();
            Dt = oDMCommon.GetDataSet_Search(objSearch, CompanyDBIndex.ToString()).Tables[0];
            oDMCommon.Close();

            if (Dt.Rows.Count > 1)
            {
                if (!((DataView)this.ItemsSource).Table.Rows.Contains(Dt.Rows[1][0]))
                {
                    ((DataView)this.ItemsSource).Table.Rows.RemoveAt(0);

                    DataRow dr = ((DataView)this.ItemsSource).Table.NewRow();

                    for (int j = 0; j < ((DataView)this.ItemsSource).Table.Columns.Count; j++)
                    {
                        dr[j] = Dt.Rows[1][j];
                    }
                    ((DataView)this.ItemsSource).Table.Rows.Add(dr);
                }
            }

            return i;
        }

        int getPrevRow()
        {
            int i = 0;
            DataTable Dt = null;
            SearchCriteria objSearch = new SearchCriteria();
            objSearch.Query = _Query;
            objSearch.WhereString = _Where;
            objSearch.SearchOn = _FilterCol;
            objSearch.SearchValue = this.Text;
            objSearch.MaximumRows = 2;
            objSearch.IsMoveUp = true;

            CommonClient oDMCommon = new CommonClient();
            Dt = oDMCommon.GetDataSet_Search(objSearch, CompanyDBIndex.ToString()).Tables[0];
            oDMCommon.Close();
            if (Dt.Rows.Count > 0)
            {

                if (!((DataView)this.ItemsSource).Table.Rows.Contains(Dt.Rows[0][0]))
                {
                    DataRow dr = ((DataView)this.ItemsSource).Table.NewRow();

                    for (int j = 0; j < ((DataView)this.ItemsSource).Table.Columns.Count; j++)
                    {
                        dr[j] = Dt.Rows[0][j];
                    }

                    ((DataView)this.ItemsSource).Table.Rows.InsertAt(dr, 0);

                    ((DataView)this.ItemsSource).Table.Rows.RemoveAt(((DataView)this.ItemsSource).Table.Rows.Count - 1);
                }
            }

            return i;
        }


        //Synchronize selection between Combo and DataGrid popup
        void popupDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            IstextChanged = true;
            //When open in Blend prevent raising exception 
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null)
                {
                    this.SelectedItem = dg.SelectedItem;
                    //this.Text = ((System.Data.DataRowView)(dg.SelectedItem)).Row.ItemArray[1].ToString();
                    this.SelectedValue = dg.SelectedValue;
                    this.SelectedIndex = dg.SelectedIndex;
                    this.SelectedValuePath = dg.SelectedValuePath;

                }
            }
            IstextChanged = false;

        }
        //Event for DataGrid popup MouseDown
        void popupDataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

            IstextChanged = true;

            DataGrid dg = sender as DataGrid;
            if (dg != null)
            {
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                // iteratively traverse the visual tree and stop when dep is one of ..
                while ((dep != null) &&
                        !(dep is DataGridCell) &&
                        !(dep is DataGridColumnHeader))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                {
                    IstextChanged = false;
                    return;
                }
                if (dep is DataGridColumnHeader)
                {
                    // do something
                }
                //When user clicks to DataGrid cell, popup have to be closed
                if (dep is DataGridCell)
                {
                    this.IsDropDownOpen = false;
                }
            }

            IstextChanged = false;
        }

        //When selection changed in combobox (pressing  arrow key down or up) must be synchronized with opened DataGrid popup
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {

            IstextChanged = true;

            base.OnSelectionChanged(e);
            if (popupDataGrid == null)
            {
                IstextChanged = false;
                return;
            }
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (IsDropDownOpen)
                {
                    popupDataGrid.SelectedItem = this.SelectedItem;

                }
            }

            IstextChanged = false;

        }
        protected override void OnDropDownOpened(EventArgs e)
        {

            IstextChanged = true;

            popupDataGrid.SelectedItem = this.SelectedItem;

            base.OnDropDownOpened(e);



            IstextChanged = false;
        }

 

    }
}
