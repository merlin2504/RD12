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
using System.ComponentModel;
using System.Windows.Markup;
using System.Collections.ObjectModel;
using Microsoft.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Windows.Controls.Primitives;
using System.Data;
using System.Xml;
using System.Collections;
using PACT.DBHandler;
namespace PACT.COMMON
{
    [DefaultProperty("Columns")]
    [ContentProperty("Columns")]
    public class PACTListView : ComboBox
    {
        string prevtext = "";
        const string partPopupDataGrid = "PART_PopupDataGrid";
        //Columns of DataGrid
        private ObservableCollection<Microsoft.Windows.Controls.DataGridTextColumn> columns;
        //Attached DataGrid control
        private Microsoft.Windows.Controls.DataGrid popupDataGrid;
        TextBox txt;

        public int UserID
        {
            get
            {
                return (int)GetValue(UserIDProperty);
            }
            set
            {
                SetValue(UserIDProperty, value);
            }
        }

        public static readonly DependencyProperty UserIDProperty =
            DependencyProperty.RegisterAttached("UserID", typeof(int), typeof(PACTListView), new UIPropertyMetadata(null));


        public int CostCenterID
        {
            get
            {
                return (int)GetValue(CostCenterIDProperty);
            }
            set
            {
                SetValue(CostCenterIDProperty, value);
            }
        }

        public static readonly DependencyProperty CostCenterIDProperty =
            DependencyProperty.RegisterAttached("CostCenterID", typeof(int), typeof(PACTListView), new UIPropertyMetadata(null));

        public int CompanyDBIndex
        {
            get
            {
                return (int)GetValue(CompanyDBIndexProperty);
            }
            set
            {
                SetValue(CompanyDBIndexProperty, value);
            }
        }

        public static readonly DependencyProperty CompanyDBIndexProperty =
            DependencyProperty.RegisterAttached("CompanyDBIndex", typeof(int), typeof(PACTListView), new UIPropertyMetadata(null));

        public bool IsPartiralData
        {
            get
            {
                return (bool)GetValue(IsPartiralDataProperty);
            }
            set
            {
                SetValue(IsPartiralDataProperty, value);
            }
        }

        public static readonly DependencyProperty IsPartiralDataProperty =
            DependencyProperty.RegisterAttached("IsPartiralData", typeof(bool), typeof(PACTListView), new UIPropertyMetadata(null));



        string strTable, strPrimaryKey;
        string strColumns = string.Empty;
        string _Where = string.Empty;
        string StrFilterColumn = string.Empty;
        int ListViewID = 0;
        int PageSize = 8;
        ArrayList ColumnsList;
        int SearchColumn
        {
            get;
            set;
        }
        int iWidth = 0;
        bool IstextChanged = false;
        bool IsintextChanged = false;
        bool valueSelected = false;
        bool IsPopupOpenFromTextChanged = false;

        static PACTListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PACTListView), new FrameworkPropertyMetadata(typeof(PACTListView)));
        }

        //The property is default and Content property for PACTListView
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

            if (CostCenterID == 0)
                return;
            ArrayList Param = new ArrayList();
            Param.Add(CostCenterID);
            Param.Add(UserID);

            DataSet ds = new PACT.DBHandler.DBHandler().GetCostCenterListView(CompanyDBIndex, Param);
            ListViewID = Convert.ToInt32(ds.Tables[0].Rows[0]["ListViewID"].ToString());
            this.IsEditable = true;
            Microsoft.Windows.Controls.DataGridTextColumn dgCol;

            dgCol = new Microsoft.Windows.Controls.DataGridTextColumn();
            dgCol.Header = strPrimaryKey;
            dgCol.Binding = new Binding(strPrimaryKey);
            dgCol.Visibility = System.Windows.Visibility.Hidden;
            this.Columns.Add(dgCol);

            ColumnsList = new ArrayList();

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (i == 0)
                    strColumns = ds.Tables[1].Rows[i]["ColumnName"].ToString();
                else
                    strColumns += "," + ds.Tables[1].Rows[i]["ColumnName"].ToString();

                ColumnsList.Add(ds.Tables[1].Rows[i]["ColumnName"].ToString());

                dgCol = new Microsoft.Windows.Controls.DataGridTextColumn();
                dgCol.Header = ds.Tables[1].Rows[i]["ColumnName"].ToString();

                if (ds.Tables[1].Rows.Count == 1)
                    dgCol.Width = this.Width;
                else
                    dgCol.Width = Convert.ToInt32(ds.Tables[1].Rows[i]["ColumnWidth"].ToString());

                dgCol.CanUserResize = false;
                dgCol.CanUserSort = false;
                dgCol.Binding = new Binding(ds.Tables[1].Rows[i]["ColumnName"].ToString());
                iWidth += Convert.ToInt32(ds.Tables[1].Rows[i]["ColumnWidth"].ToString());
                this.Columns.Add(dgCol);

            }


            if (this.SelectedValue != null)
                valueSelected = true;

            //this.SelectedValue = new Binding(strPrimaryKey);
            this.SelectedValuePath = strPrimaryKey;

        }

        void SetSearchColumn(int Index, bool IsFromApplytemplate)
        {
            if (Convert.ToInt32(CostCenterID) == 0)
                return;

            SearchColumn = Index;
            if (ColumnsList[Index - 1].ToString() != StrFilterColumn)
            {
                StrFilterColumn = ColumnsList[Index - 1].ToString();
                this.DisplayMemberPath = ColumnsList[Index - 1].ToString();

                if (IsFromApplytemplate)
                    FillListBox();
                else if (IsPartiralData && this.Text != "")
                    FillListBox();


                Style selectedColumnStyle = new Style();
                selectedColumnStyle.Setters.Add(new Setter()
                {
                    Property = ForegroundProperty,
                    Value = new SolidColorBrush(Color.FromRgb(0, 0, 0))
                });

                Style ColumnStyle = new Style();
                ColumnStyle.Setters.Add(new Setter()
                {
                    Property = ForegroundProperty,
                    Value = new SolidColorBrush(Color.FromRgb(190, 190, 190))
                });


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
            if (Convert.ToInt32(CostCenterID) == 0)
                return;

            if (popupDataGrid == null)
            {

                popupDataGrid = this.Template.FindName("PART_PopupDataGrid", this) as Microsoft.Windows.Controls.DataGrid;
                popupDataGrid.GridLinesVisibility = Microsoft.Windows.Controls.DataGridGridLinesVisibility.None;

                Style ColumnHeaderStyle = new System.Windows.Style();
                ColumnHeaderStyle.Setters.Add(new Setter()
                {
                    Property = FontSizeProperty,
                    Value = Convert.ToDouble(12)
                });
                ColumnHeaderStyle.Setters.Add(new Setter()
                {
                    Property = FontWeightProperty,
                    Value = FontWeights.Bold
                });
                ColumnHeaderStyle.Setters.Add(new Setter()
                {
                    Property = BackgroundProperty,
                    Value = new SolidColorBrush(Color.FromRgb(139, 137, 137))
                });
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

            if (IstextChanged || Convert.ToInt32(CostCenterID) == 0 || !IsPartiralData)
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
            if (prevtext == "")
            {
                txt.CaretIndex = 1;
                txt.SelectedText = "";
            }
            else
            {
                if (txt.CaretIndex != index)
                    txt.CaretIndex = index;
                if (txt.SelectedText != stext)
                    txt.SelectedText = stext;
            }
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

            prevtext = this.Text;
        }


        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            IstextChanged = true;

            base.OnPreviewKeyDown(e);

            if (Convert.ToInt32(CostCenterID) == 0)
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
                        if (columns.Count() > 2)
                            SetSearchColumn(2, false);
                        break;
                    case System.Windows.Input.Key.D3:
                        if (columns.Count() > 3)
                            SetSearchColumn(3, false);
                        break;
                    case System.Windows.Input.Key.D4:
                        if (columns.Count() > 4)
                            SetSearchColumn(4, false);
                        break;
                    case System.Windows.Input.Key.D5:
                        if (columns.Count() > 5)
                            SetSearchColumn(5, false);
                        break;
                    case System.Windows.Input.Key.D6:
                        if (columns.Count() > 6)
                            SetSearchColumn(6, false);
                        break;
                    case System.Windows.Input.Key.D7:
                        if (columns.Count() > 7)
                            SetSearchColumn(7, false);
                        break;
                    case System.Windows.Input.Key.D8:
                        if (columns.Count() > 8)
                            SetSearchColumn(8, false);
                        break;
                    case System.Windows.Input.Key.D9:
                        if (columns.Count() > 9)
                            SetSearchColumn(9, false);
                        break;
                }

                IstextChanged = false;
                return;
            }

            if (e.Key == Key.Down && this.SelectedIndex == PageSize - 1 && IsPartiralData)
                getNextRow();
            else if (e.Key == Key.Up && this.SelectedIndex == 0 && IsPartiralData)
                getPrevRow();

            IstextChanged = false;

        }

        void FillListBox()
        {
            ArrayList Param = new ArrayList();
            Param.Add(ListViewID);
            Param.Add(strColumns);
            Param.Add(StrFilterColumn);
            Param.Add(Text);
            if (IsPartiralData)
                Param.Add(PageSize);
            else
                Param.Add(0);
            Param.Add(false);
            Param.Add("");
            if (valueSelected)
            {
                Param.Add(this.SelectedValue.ToString());
            }

            DataTable Dt = new PACT.DBHandler.DBHandler().GetCostCenterListViewData(CompanyDBIndex, Param).Tables[0];


            IstextChanged = true;

            if (Dt != null && Dt.Rows.Count > 0)
            {
                if (this.ItemsSource == null)
                {
                    this.ItemsSource = Dt.AsDataView();
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

            ArrayList Param = new ArrayList();
            Param.Add(ListViewID);
            Param.Add(strColumns);
            Param.Add(StrFilterColumn);
            Param.Add(Text);
            Param.Add(2);
            Param.Add(false);

            DataTable Dt = new PACT.DBHandler.DBHandler().GetCostCenterListViewData(CompanyDBIndex, Param).Tables[0];


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

            ArrayList Param = new ArrayList();
            Param.Add(ListViewID);
            Param.Add(strColumns);
            Param.Add(StrFilterColumn);
            Param.Add(Text);
            Param.Add(2);
            Param.Add(true);
            DataTable Dt = new PACT.DBHandler.DBHandler().GetCostCenterListViewData(CompanyDBIndex, Param).Tables[0];

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
                        !(dep is Microsoft.Windows.Controls.Primitives.DataGridColumnHeader))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                {
                    IstextChanged = false;
                    return;
                }
                if (dep is Microsoft.Windows.Controls.Primitives.DataGridColumnHeader)
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

                    if (popupDataGrid.SelectedItem != null && !IsPartiralData)
                        popupDataGrid.ScrollIntoView(popupDataGrid.SelectedItem);

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

            if (popupDataGrid.SelectedItem != null && !IsPartiralData)
                popupDataGrid.ScrollIntoView(popupDataGrid.SelectedItem);

            IsPopupOpenFromTextChanged = false;
            IstextChanged = false;
        }




    }
}
