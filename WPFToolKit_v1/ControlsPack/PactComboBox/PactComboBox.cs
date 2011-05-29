using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using Microsoft.Windows.Controls.CommonService;
using Microsoft.Windows.Controls.Primitives;

namespace Microsoft.Windows.Controls
{

    [DefaultProperty("Columns")]
    [ContentProperty("Columns")]
    public class PactComboBox : ComboBox
    {
        const string partPopupDataGrid = "PART_PopupDataGrid";
        //Columns of DataGrid
        private ObservableCollection<Microsoft.Windows.Controls.DataGridTextColumn> columns;
        //Attached DataGrid control
        private Microsoft.Windows.Controls.DataGrid popupDataGrid;
        TextBox txt;


        [AttachedPropertyBrowsableForType(typeof(PactComboBox))]
        public static int GetFeatureID(DependencyObject obj)
        {
            if (((PactComboBox)obj).FeatureID > 0)
                return ((PactComboBox)obj).FeatureID;
            else
                return (int)obj.GetValue(FeatureIDProperty);
        }

        public static void SetFeatureID(DependencyObject obj, string value)
        {
            obj.SetValue(FeatureIDProperty, value);
        }

        public static readonly DependencyProperty FeatureIDProperty =
            DependencyProperty.RegisterAttached("FeatureID", typeof(int), typeof(PactComboBox), new UIPropertyMetadata(null));



        [AttachedPropertyBrowsableForType(typeof(PactComboBox))]
        public static int GetCompanyDBIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(CompanyDBIndexProperty);
        }

        public static void SetCompanyDBIndex(DependencyObject obj, string value)
        {
            obj.SetValue(CompanyDBIndexProperty, value);
        }

        public static readonly DependencyProperty CompanyDBIndexProperty =
            DependencyProperty.RegisterAttached("CompanyDBIndex", typeof(int), typeof(PactComboBox), new UIPropertyMetadata(null));


        [AttachedPropertyBrowsableForType(typeof(PactComboBox))]
        public static bool GetIsPartiralData(DependencyObject obj)
        {
            return true;
            // return (bool)obj.GetValue(IsPartiralDataProperty);
        }

        public static void SetIsPartiralData(DependencyObject obj, string value)
        {
            obj.SetValue(IsPartiralDataProperty, value);
        }

        public static readonly DependencyProperty IsPartiralDataProperty =
            DependencyProperty.RegisterAttached("IsPartiralData", typeof(bool), typeof(PactComboBox), new UIPropertyMetadata(null));



        string strTable, strPrimaryKey;
        string _Query = string.Empty;
        string _Where = string.Empty;
        string _FilterCol = string.Empty;
        int PageSize = 8;
        List<TreeColumn> oColumns;
        int SearchColumn { get; set; }
        int iWidth = 0;
        bool IstextChanged = false;
        bool IsintextChanged = false;
        bool valueSelected = false;
        bool IsPopupOpenFromTextChanged = false;
        public int FeatureID = 0;

        static PactComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PactComboBox), new FrameworkPropertyMetadata(typeof(PactComboBox)));
        }

        //The property is default and Content property for PactComboBox
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<Microsoft.Windows.Controls.DataGridTextColumn> Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = new ObservableCollection<Microsoft.Windows.Controls.DataGridTextColumn>();
                }
                return this.columns;
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (GetFeatureID(this) == 0)
                return;

            CommonClient oDMCommon = new CommonClient();
            DataTable dt = oDMCommon.GetDataTable("SELECT * FROM AdmnLayoutLists WHERE ID=" + GetFeatureID(this).ToString(), GetCompanyDBIndex(this).ToString());
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
            Microsoft.Windows.Controls.DataGridTextColumn dgCol;

            dgCol = new Microsoft.Windows.Controls.DataGridTextColumn();
            dgCol.Header = strPrimaryKey;
            dgCol.Binding = new Binding(strPrimaryKey);
            dgCol.Visibility = System.Windows.Visibility.Hidden;
            this.Columns.Add(dgCol);


            for (int i = 0; i < xList.Count; i++)
            {
                oCol = (TreeColumn)PACTSerializer.FromXml(xList[i].OuterXml, typeof(TreeColumn));
                _Query += "," + oCol.Name;

                dgCol = new Microsoft.Windows.Controls.DataGridTextColumn();
                dgCol.Header = oCol.Label;

                if (xList.Count == 1)
                {
                    if (this.Width > 0)
                        dgCol.Width = this.Width;
                    else
                        dgCol.Width = oCol.Width;
                }
                else
                    dgCol.Width = oCol.Width;

                dgCol.CanUserResize = false;
                dgCol.CanUserSort = false;
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

        void SetSearchColumn(int Index, bool IsFromApplytemplate)
        {
            if (GetFeatureID(this) == 0)
                return;

            SearchColumn = Index;
            if (oColumns[Index - 1].Name != _FilterCol)
            {
                _FilterCol = oColumns[Index - 1].Name;
                this.DisplayMemberPath = oColumns[Index - 1].Name;

                if (IsFromApplytemplate)
                    FillListBox();
                else if (GetIsPartiralData(this) && this.Text != "")
                    FillListBox();

                Style selectedColumnStyle = new Style();
                selectedColumnStyle.Setters.Add(new Setter() { Property = ForegroundProperty, Value = new SolidColorBrush(Color.FromRgb(0, 0, 0)) });

                Style ColumnStyle = new Style();
                ColumnStyle.Setters.Add(new Setter() { Property = ForegroundProperty, Value = new SolidColorBrush(Color.FromRgb(190, 190, 190)) });


                //TODO Styling
                for (int i = 1; i < this.Columns.Count; i++)
                {
                    popupDataGrid.Columns[i].CellStyle = ColumnStyle;
                }
                popupDataGrid.Columns[Index].CellStyle = selectedColumnStyle;

            }
        }

        //Apply theme and attach columns to DataGrid popupo control
        public override void OnApplyTemplate()
        {
            if (GetFeatureID(this) == 0)
                return;

            if (popupDataGrid == null)
            {

                popupDataGrid = this.Template.FindName("PART_PopupDataGrid", this) as Microsoft.Windows.Controls.DataGrid;
                popupDataGrid.GridLinesVisibility = Microsoft.Windows.Controls.DataGridGridLinesVisibility.None;

                

                Style ColumnHeaderStyle = new System.Windows.Style();
                ColumnHeaderStyle.Setters.Add(new Setter() { Property = FontSizeProperty, Value = Convert.ToDouble(12) });
                ColumnHeaderStyle.Setters.Add(new Setter() { Property = FontWeightProperty, Value = FontWeights.Bold });
                ColumnHeaderStyle.Setters.Add(new Setter() { Property = BackgroundProperty, Value = Brushes.SteelBlue});
                ColumnHeaderStyle.Setters.Add(new Setter() { Property = ForegroundProperty, Value = Brushes.White });
                popupDataGrid.ColumnHeaderStyle = ColumnHeaderStyle;


                if (popupDataGrid != null)
                {

                    for (int i = 0; i < columns.Count; i++)
                        popupDataGrid.Columns.Add(columns[i]);

                    popupDataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

                    SetSearchColumn(1, true);

                    popupDataGrid.ItemsSource = this.ItemsSource;

                    //Add event handler for DataGrid popup
                    popupDataGrid.MouseUp += new MouseButtonEventHandler(popupDataGrid_MouseDown);
                    popupDataGrid.SelectionChanged += new SelectionChangedEventHandler(popupDataGrid_SelectionChanged);


                }
            }


            //Call base class method
            base.OnApplyTemplate();

            if (txt == null)
            {
                txt = this.Template.FindName("PART_EditableTextBox", this) as TextBox;
                if (txt != null)
                {
                    txt.TextChanged += new TextChangedEventHandler(txt_TextChanged);
                }
            }
        }

        void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsintextChanged = true;
            if (this.Template == null)
                return;

            Popup popup = this.Template.FindName("PART_Popup", this) as Popup;
            if (!popup.IsOpen)
            {
                IsPopupOpenFromTextChanged = true;
                popup.IsOpen = true;
            }

            string text = "";
            int index = txt.CaretIndex;
            if (txt.SelectedText != "" && txt.CaretIndex > 0)
                text = txt.Text.Substring(0, txt.CaretIndex);
            else
                text = txt.Text;
            string stext = txt.SelectedText;

            if (IstextChanged || GetFeatureID(this) == 0 || (index == 0 && text != "") || !GetIsPartiralData(this))
            {
                if (popupDataGrid.SelectedItem != null && this.SelectedItem != null && this.Text != ((DataRowView)popupDataGrid.SelectedItem).Row.ItemArray[SearchColumn].ToString())
                    for (int i = 0; i < popupDataGrid.Items.Count; i++)
                    {
                        if (this.Text == ((DataRowView)popupDataGrid.Items[i]).Row.ItemArray[SearchColumn].ToString())
                        {
                            popupDataGrid.SelectedItem = popupDataGrid.Items[i];
                            IsintextChanged = false;
                            IstextChanged = false;
                            return;
                        }

                    }
                return;
            }

            FillListBox();


            IstextChanged = true;



            if (txt.Text != text)
                txt.Text = text;
            if (txt.CaretIndex != index)
                txt.CaretIndex = index;
            if (txt.SelectedText != stext)
                txt.SelectedText = stext;

            if (popupDataGrid.SelectedItem != null && this.SelectedItem != null && this.Text != ((DataRowView)popupDataGrid.SelectedItem).Row.ItemArray[SearchColumn].ToString())
                for (int i = 0; i < popupDataGrid.Items.Count; i++)
                {
                    if (this.Text == ((DataRowView)popupDataGrid.Items[i]).Row.ItemArray[SearchColumn].ToString())
                    {
                        popupDataGrid.SelectedItem = popupDataGrid.Items[i];
                        IsintextChanged = false;
                        IstextChanged = false;
                        return;
                    }

                }

            IsintextChanged = false;
            IstextChanged = false;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            IstextChanged = true;

            base.OnPreviewKeyDown(e);

            if (GetFeatureID(this) == 0)
            {
                IstextChanged = false;
                return;
            }
            if (e.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case System.Windows.Input.Key.D1:
                        SetSearchColumn(1, false);
                        break;
                    case System.Windows.Input.Key.D2:
                        if (columns.Count() > 2) SetSearchColumn(2, false);
                        break;
                    case System.Windows.Input.Key.D3:
                        if (columns.Count() > 3) SetSearchColumn(3, false);
                        break;
                    case System.Windows.Input.Key.D4:
                        if (columns.Count() > 4) SetSearchColumn(4, false);
                        break;
                    case System.Windows.Input.Key.D5:
                        if (columns.Count() > 5) SetSearchColumn(5, false);
                        break;
                    case System.Windows.Input.Key.D6:
                        if (columns.Count() > 6) SetSearchColumn(6, false);
                        break;
                    case System.Windows.Input.Key.D7:
                        if (columns.Count() > 7) SetSearchColumn(7, false);
                        break;
                    case System.Windows.Input.Key.D8:
                        if (columns.Count() > 8) SetSearchColumn(8, false);
                        break;
                    case System.Windows.Input.Key.D9:
                        if (columns.Count() > 9) SetSearchColumn(9, false);
                        break;
                }

                IstextChanged = false;
                return;
            }

            if (e.Key == Key.Down && this.SelectedIndex == PageSize - 1 && GetIsPartiralData(this))
                getNextRow();
            else if (e.Key == Key.Up && this.SelectedIndex == 0 && GetIsPartiralData(this))
                getPrevRow();

            IstextChanged = false;

        }

        void FillListBox()
        {

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
            else
                objSearch.SelectedValueQuery = "";
            if (_Where.Trim() != "")
                _Query = _Query + " where " + _Where;

            CommonClient oDMCommon = new CommonClient();
            if (GetIsPartiralData(this))
                Dt = oDMCommon.GetDataTable_Search(objSearch, GetCompanyDBIndex(this).ToString());
            else
                Dt = oDMCommon.GetDataTable(_Query, GetCompanyDBIndex(this).ToString());

            oDMCommon.Close();


            IstextChanged = true;

            if (Dt != null && Dt.Rows.Count > 0)
            {
                if (this.ItemsSource == null)
                {
                    this.ItemsSource = Dt.DefaultView;
                    //  ((DataView)this.ItemsSource).Table.PrimaryKey = new DataColumn[] { ((DataView)this.ItemsSource).Table.Columns[0] };
                }
                else
                {

                    ((DataView)this.ItemsSource).Table.Rows.Clear();
                    //Dt.PrimaryKey = new DataColumn[] { Dt.Columns[0] };

                    //for (int i = 0; i < ((DataView)this.ItemsSource).Table.Rows.Count; i++)
                    //{
                    //    if (!Dt.Rows.Contains(((DataView)this.ItemsSource).Table.Rows[i][0]))
                    //    {
                    //        ((DataView)this.ItemsSource).Table.Rows.RemoveAt(i);
                    //        i--;
                    //    }
                    //}

                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        //if (((DataView)this.ItemsSource).Table.Rows.Contains(Dt.Rows[i][0]))
                        //    continue;

                        DataRow dr = ((DataView)this.ItemsSource).Table.NewRow();

                        for (int j = 0; j < ((DataView)this.ItemsSource).Table.Columns.Count; j++)
                        {
                            dr[j] = Dt.Rows[i][j];
                        }
                        //  if (((DataView)this.ItemsSource).Table.Rows.Count == i)
                        ((DataView)this.ItemsSource).Table.Rows.Add(dr);
                        //else
                        //    ((DataView)this.ItemsSource).Table.Rows.InsertAt(dr, i);
                    }
                    //if (popupDataGrid.Items.Count > 0)
                    //    popupDataGrid.SelectedItem = popupDataGrid.Items[0];
                }

                //if (txt != this.Text)
                //{
                //    IstextChanged = true;
                //    this.Text = txt;

                //    for (int i = 0; i < popupDataGrid.Items.Count; i++)
                //    {
                //        if (this.Text == ((DataRowView)popupDataGrid.Items[i]).Row.ItemArray[SearchColumn].ToString())
                //        {
                //            popupDataGrid.SelectedItem = popupDataGrid.Items[i];
                //            return;
                //        }

                //    }

                //}

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
            Dt = oDMCommon.GetDataSet_Search(objSearch, GetCompanyDBIndex(this).ToString()).Tables[0];
            oDMCommon.Close();

            if (Dt.Rows.Count > 1)
            {
                //if (!((DataView)this.ItemsSource).Table.Rows.Contains(Dt.Rows[1][0]))
                //{
                ((DataView)this.ItemsSource).Table.Rows.RemoveAt(0);

                DataRow dr = ((DataView)this.ItemsSource).Table.NewRow();

                for (int j = 0; j < ((DataView)this.ItemsSource).Table.Columns.Count; j++)
                {
                    dr[j] = Dt.Rows[1][j];
                }
                ((DataView)this.ItemsSource).Table.Rows.Add(dr);
                // }
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
            Dt = oDMCommon.GetDataSet_Search(objSearch, GetCompanyDBIndex(this).ToString()).Tables[0];
            oDMCommon.Close();
            if (Dt.Rows.Count > 0)
            {

                //if (!((DataView)this.ItemsSource).Table.Rows.Contains(Dt.Rows[0][0]))
                //{
                DataRow dr = ((DataView)this.ItemsSource).Table.NewRow();

                for (int j = 0; j < ((DataView)this.ItemsSource).Table.Columns.Count; j++)
                {
                    dr[j] = Dt.Rows[0][j];
                }

                ((DataView)this.ItemsSource).Table.Rows.InsertAt(dr, 0);

                ((DataView)this.ItemsSource).Table.Rows.RemoveAt(((DataView)this.ItemsSource).Table.Rows.Count - 1);
                // }
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
                Microsoft.Windows.Controls.DataGrid dg = sender as Microsoft.Windows.Controls.DataGrid;
                if (dg != null)
                {
                    this.SelectedItem = dg.SelectedItem;
                    //this.Text = ((System.Data.DataRowView)(dg.SelectedItem)).Row.ItemArray[1].ToString();
                    this.SelectedValue = dg.SelectedValue;
                    this.SelectedIndex = dg.SelectedIndex;
                    this.SelectedValuePath = dg.SelectedValuePath;

                }
            }
            if (!IsintextChanged)
                IstextChanged = false;

        }
        //Event for DataGrid popup MouseDown
        void popupDataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

            IstextChanged = true;

            Microsoft.Windows.Controls.DataGrid dg = sender as Microsoft.Windows.Controls.DataGrid;
            if (dg != null)
            {
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                // iteratively traverse the visual tree and stop when dep is one of ..
                while ((dep != null) &&
                        !(dep is Microsoft.Windows.Controls.DataGridCell) &&
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
                if (dep is Microsoft.Windows.Controls.DataGridCell)
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
                    if (this.SelectedItem != null)
                        popupDataGrid.SelectedItem = this.SelectedItem;

                }
            }

            if (!IsintextChanged)
                IstextChanged = false;

        }
        protected override void OnDropDownOpened(EventArgs e)
        {
            IstextChanged = true;
            if (this.SelectedItem != null)
                popupDataGrid.SelectedItem = this.SelectedItem;
            else if (this.Text != "" && !IsPopupOpenFromTextChanged)
            {
                for (int i = 0; i < popupDataGrid.Items.Count; i++)
                {
                    if (this.Text == ((DataRowView)popupDataGrid.Items[i]).Row.ItemArray[SearchColumn].ToString())
                    {
                        popupDataGrid.SelectedItem = popupDataGrid.Items[i];
                        IsintextChanged = false;
                        IstextChanged = false;
                        return;
                    }
                }
            }
            base.OnDropDownOpened(e);

            IsPopupOpenFromTextChanged = false;
            IstextChanged = false;

            //System.Windows.Controls.Primitives.Popup oPop = this.Template.FindName("PART_Popup", this) as System.Windows.Controls.Primitives.Popup;
            //oPop.IsOpen = true;
            //oPop.Visibility = System.Windows.Visibility.Visible;
            //oPop.UpdateLayout();
            //popupDataGrid.UpdateLayout();
            //popupDataGrid.Width = 200;
            //popupDataGrid.Height = 200;
            //popupDataGrid.Background = Brushes.Orange;

            //foreach (var item in popupDataGrid.ItemsSource)
            //{
            //    var oRow = popupDataGrid.ItemContainerGenerator.ItemFromContainer(item) ;//as Microsoft.Windows.Controls.DataGridRow;
            //}
          //DataGridRow[] ARR= GetRows(popupDataGrid).ToArray() ;
          //ARR[1].Height = 40;
          //  GetRows(popupDataGrid)
        }

        public IEnumerable<DataGridRow> GetRows(DataGrid grid)
        {
            var itemsource = grid.ItemsSource as System.Collections.IEnumerable;
            if (itemsource == null) yield return null;
            foreach (var item in itemsource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null) yield return row;
            }
        }
        //object dt = null;
    }
}
