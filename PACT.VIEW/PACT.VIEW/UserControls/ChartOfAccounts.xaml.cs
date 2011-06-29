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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using PACT.COMMON;

namespace PACT.VIEW
{
    /// <summary>
    /// Interaction logic for ChartOfAccounts.xaml
    /// </summary>
    public partial class ChartOfAccounts : UserControl
    {
        public ChartOfAccounts()
        {
            InitializeComponent();
        }
        internal ObservableCollectionAdv<TreeNode> Rows
        {
            get;
            private set;

        }

        internal List<TreeNode> AllTreeNodes
        {
            get;
            private set;

        }

        SolidColorBrush MovingItemColorBrush
        {
            get
            {
                return new SolidColorBrush(MovingItemColor);
            }
        }
        SolidColorBrush GroupColorBrush
        {
            get
            {
                return new SolidColorBrush(GroupItemColor);
            }
        }

        SolidColorBrush ItemColorBrush
        {
            get
            {
                return new SolidColorBrush(ItemColor);
            }
        }
        public Color GroupItemColor
        {
            get
            {
                return (Color)GetValue(GroupItemColorProperty);
            }
            set
            {
                SetValue(GroupItemColorProperty, value);
            }
        }

        public static readonly DependencyProperty GroupItemColorProperty =
            DependencyProperty.RegisterAttached("GroupItemColor", typeof(Color), typeof(ChartOfAccounts), new UIPropertyMetadata(null));


        public Color ItemColor
        {
            get
            {
                return (Color)GetValue(ItemColorProperty);
            }
            set
            {
                SetValue(ItemColorProperty, value);
            }
        }

        public static readonly DependencyProperty ItemColorProperty =
            DependencyProperty.RegisterAttached("ItemColor", typeof(Color), typeof(ChartOfAccounts), new UIPropertyMetadata(null));



        public Color SelectedItemColor
        {
            get
            {
                return (Color)GetValue(SelectedItemColorProperty);
            }
            set
            {
                SetValue(SelectedItemColorProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedItemColorProperty =
            DependencyProperty.RegisterAttached("SelectedItemColor", typeof(Color), typeof(ChartOfAccounts), new UIPropertyMetadata(null));

        public Double RowHeight
        {
            get
            {
                return (Double)GetValue(RowHeightProperty);
            }
            set
            {
                SetValue(RowHeightProperty, value);
            }
        }

        public static readonly DependencyProperty RowHeightProperty =
            DependencyProperty.RegisterAttached("RowHeight", typeof(Double), typeof(ChartOfAccounts), new UIPropertyMetadata(null));

        public Color AlternateBackground1
        {
            get
            {
                return (Color)GetValue(AlternateBackground1Property);
            }
            set
            {
                SetValue(AlternateBackground1Property, value);
            }
        }

        public static readonly DependencyProperty AlternateBackground1Property =
            DependencyProperty.RegisterAttached("AlternateBackground1", typeof(Color), typeof(ChartOfAccounts), new UIPropertyMetadata(null));


        public Color AlternateBackground2
        {
            get
            {
                return (Color)GetValue(AlternateBackground2Property);
            }
            set
            {
                SetValue(AlternateBackground2Property, value);
            }
        }

        public static readonly DependencyProperty AlternateBackground2Property =
            DependencyProperty.RegisterAttached("AlternateBackground2", typeof(Color), typeof(ChartOfAccounts), new UIPropertyMetadata(null));


        public Color MovingItemColor
        {
            get
            {
                return (Color)GetValue(MovingItemColorProperty);
            }
            set
            {
                SetValue(MovingItemColorProperty, value);
            }
        }

        public static readonly DependencyProperty MovingItemColorProperty =
            DependencyProperty.RegisterAttached("MovingItemColor", typeof(Color), typeof(ChartOfAccounts), new UIPropertyMetadata(null));



        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(ChartOfAccounts), new UIPropertyMetadata(null));


        public List<string> CMenu
        {
            get
            {
                return (List<string>)GetValue(CMenuProperty);
            }
            set
            {
                SetValue(CMenuProperty, value);
            }
        }

        public static readonly DependencyProperty CMenuProperty =
            DependencyProperty.RegisterAttached("CMenu", typeof(List<string>), typeof(ChartOfAccounts), new UIPropertyMetadata(null));


        public TreeNode SelectedNode
        {
            get
            {
                return (TreeNode)GetValue(SelectedNodeProperty);
            }
            set
            {
                SetValue(SelectedNodeProperty, value);
            }
        }
        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.RegisterAttached("SelectedNode", typeof(TreeNode), typeof(ChartOfAccounts), new UIPropertyMetadata(null));

        public string strCols
        {
            get
            {
                return (string)GetValue(strColsProperty);
            }
            set
            {
                SetValue(strColsProperty, value);
            }
        }
        public static readonly DependencyProperty strColsProperty =
            DependencyProperty.RegisterAttached("strCols", typeof(string), typeof(ChartOfAccounts), new UIPropertyMetadata(null));

        public int GridViewID
        {
            get
            {
                return (int)GetValue(GridViewIDProperty);
            }
            set
            {
                SetValue(GridViewIDProperty, value);
            }
        }
        public static readonly DependencyProperty GridViewIDProperty =
            DependencyProperty.RegisterAttached("GridViewID", typeof(int), typeof(ChartOfAccounts), new UIPropertyMetadata(null));


        public TreeNode TreeNodeToMove
        {
            get
            {
                return (TreeNode)GetValue(TreeNodeToMoveProperty);
            }
            set
            {
                SetValue(TreeNodeToMoveProperty, value);
            }
        }
        public static readonly DependencyProperty TreeNodeToMoveProperty =
                   DependencyProperty.RegisterAttached("TreeNodeToMove", typeof(TreeNode), typeof(ChartOfAccounts), new UIPropertyMetadata(null));



        public DataTable DataSource
        {
            get
            {
                return (DataTable)GetValue(DataSourceProperty);
            }
            set
            {
                SetValue(DataSourceProperty, value);
            }
        }

        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.RegisterAttached("DataSource", typeof(DataTable), typeof(ChartOfAccounts), new UIPropertyMetadata(
                    null
                    ));

        public DataSet Dataset
        {
            get
            {
                return (DataSet)GetValue(DatasetProperty);
            }
            set
            {
                SetValue(DatasetProperty, value);
            }
        }

        public static readonly DependencyProperty DatasetProperty =
            DependencyProperty.RegisterAttached("Dataset", typeof(DataSet), typeof(ChartOfAccounts), new UIPropertyMetadata(
                    null
                    ));


        public DataTable TempDataSource
        {
            get
            {
                return (DataTable)GetValue(TempDataSourceProperty);
            }
            set
            {
                SetValue(TempDataSourceProperty, value);
            }
        }

        public static readonly DependencyProperty TempDataSourceProperty =
            DependencyProperty.RegisterAttached("TempDataSource", typeof(DataTable), typeof(ChartOfAccounts), new UIPropertyMetadata(
                    null,
                    DataSourceChanged));


        private TreeNode _root;
        internal TreeNode Root
        {
            get
            {
                return _root;
            }
        }

        List<TreeNode> lstParents = new List<TreeNode>();
        public string SearchText
        {
            get;
            set;
        }
        public int SearchCol
        {
            get;
            set;
        }

        private static void DataSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ChartOfAccounts tree = obj as ChartOfAccounts;
            DataTable dt = tree.DataSource;
            if (tree.tree1 != null && dt != null)
            {
                tree.AddRows(tree, dt, (dt == ((DataTable)e.NewValue)));
            }

            tree.tree1.ItemsSource = tree.Rows;
        }

        void AddRows(ChartOfAccounts tree, DataTable dt, bool ClearRows)
        {
            if (ClearRows)
            {
                Rows = new ObservableCollectionAdv<TreeNode>();
                _root = new TreeNode();
                _root.Tree = this;
                _root.Children = new TreeNode.NodeCollection(_root);
                _root.Nodes = new ReadOnlyCollection<TreeNode>(_root.Children);
                tree.lstParents = new List<TreeNode>();
                tree.AllTreeNodes = new List<TreeNode>();
            }

            int ColumnCount = dt.Columns.Count - 4;
            int i = tree.Rows.Count;
            for (; i < dt.Rows.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Tree = tree;
                node.Children = new TreeNode.NodeCollection(node);
                node.Nodes = new ReadOnlyCollection<TreeNode>(node.Children);


                node.IsExpandedOnce = true;
                node.AssignIsExpanded(true);


                node.Id = Convert.ToInt32(dt.Rows[i][0].ToString());

                node.IsGroup = Convert.ToBoolean(dt.Rows[i]["IsGroup"].ToString());
                node.LevelNo = Convert.ToInt16(dt.Rows[i]["Depth"].ToString());
                //node.NodeIndex = dt.Rows[i]["NodeIndex"].ToString();

                if (ColumnCount > 0)
                    node.Field1 = dt.Rows[i][1].ToString();
                if (ColumnCount > 1)
                    node.Field2 = dt.Rows[i][2].ToString();
                if (ColumnCount > 2)
                    node.Field3 = dt.Rows[i][3].ToString();
                if (ColumnCount > 3)
                    node.Field4 = dt.Rows[i][4].ToString();
                if (ColumnCount > 4)
                    node.Field5 = dt.Rows[i][5].ToString();
                if (ColumnCount > 5)
                    node.Field6 = dt.Rows[i][6].ToString();


                if (node.IsGroup)
                    node.ColorBrush = tree.GroupColorBrush;
                else
                    node.ColorBrush = tree.ItemColorBrush;

                tree.Rows.Add(node);
                tree.AllTreeNodes.Add(node);

                if (node.IsGroup)
                {
                    if (tree.lstParents.Count > 0)
                    {
                        if (node.LevelNo > tree.lstParents[tree.lstParents.Count - 1].LevelNo)
                        {
                            tree.lstParents[tree.lstParents.Count - 1].Children.Add(node);
                            tree.lstParents.Add(node);
                        }
                        else if (node.LevelNo == tree.lstParents[tree.lstParents.Count - 1].LevelNo)
                        {

                            tree.lstParents.RemoveAt(tree.lstParents.Count - 1);
                            tree.lstParents[tree.lstParents.Count - 1].Children.Add(node);
                            tree.lstParents.Add(node);
                        }
                        else if (node.LevelNo < tree.lstParents[tree.lstParents.Count - 1].LevelNo)
                        {
                            for (int j = tree.lstParents.Count - 1; j >= 0; j--)
                            {
                                if (node.LevelNo <= tree.lstParents[j].LevelNo)
                                {
                                    tree.lstParents.RemoveAt(tree.lstParents.Count - 1);
                                }
                                else
                                    break;
                            }
                            tree.lstParents[tree.lstParents.Count - 1].Children.Add(node);
                            tree.lstParents.Add(node);
                        }
                    }
                    else
                        tree.lstParents.Add(node);
                }
                else
                {
                    if (node.LevelNo > tree.lstParents[tree.lstParents.Count - 1].LevelNo)
                    {
                        tree.lstParents[tree.lstParents.Count - 1].Children.Add(node);
                    }
                    else if (node.LevelNo == tree.lstParents[tree.lstParents.Count - 1].LevelNo)
                    {
                        for (int j = tree.lstParents.Count - 1; j >= 0; j--)
                        {
                            if (node.LevelNo <= tree.lstParents[j].LevelNo)
                            {
                                tree.lstParents.RemoveAt(tree.lstParents.Count - 1);
                            }
                            else
                                break;
                        }
                        tree.lstParents[tree.lstParents.Count - 1].Children.Add(node);
                    }
                    else if (node.LevelNo < tree.lstParents[tree.lstParents.Count - 1].LevelNo)
                    {
                        for (int j = tree.lstParents.Count - 1; j >= 0; j--)
                        {
                            if (node.LevelNo <= tree.lstParents[j].LevelNo)
                            {
                                tree.lstParents.RemoveAt(tree.lstParents.Count - 1);
                            }
                            else
                                break;
                        }
                        tree.lstParents[tree.lstParents.Count - 1].Children.Add(node);
                    }

                }

            }
            if (tree.Root.Children.Count == 0 && tree.lstParents.Count() > 0)
                tree.Root.Children.Add(tree.lstParents[0]);
        }

        public bool VerticalLines
        {
            get
            {
                return (bool)GetValue(VerticalLinesProperty);
            }
            set
            {
                SetValue(VerticalLinesProperty, value);
            }
        }

        public static readonly DependencyProperty VerticalLinesProperty =
            DependencyProperty.RegisterAttached("VerticalLines", typeof(bool), typeof(ChartOfAccounts), new UIPropertyMetadata(null));

        public bool HorizontalLines
        {
            get
            {
                return (bool)GetValue(HorizontalLinesProperty);
            }
            set
            {
                SetValue(HorizontalLinesProperty, value);
            }
        }

        public static readonly DependencyProperty HorizontalLinesProperty =
            DependencyProperty.RegisterAttached("HorizontalLines", typeof(bool), typeof(ChartOfAccounts), new UIPropertyMetadata(null));

        public ObservableCollection<ColumnDescriptor> ColumnsSource
        {
            get
            {
                return (ObservableCollection<ColumnDescriptor>)GetValue(ColumnsSourceProperty);
            }
            set
            {
                SetValue(ColumnsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ColumnsSourceProperty =
           DependencyProperty.RegisterAttached(
               "ColumnsSource",
               typeof(ObservableCollection<ColumnDescriptor>),
               typeof(ChartOfAccounts),
               new UIPropertyMetadata(null));





        internal void SetIsExpanded(TreeNode node, bool value)
        {
            if (value)
            {
                node.AssignIsExpanded(value);
                CreateChildrenRows(node);
            }
            else
            {
                DropChildrenRows(node, false);
                node.AssignIsExpanded(value);
            }
        }

        private void CreateChildrenRows(TreeNode node)
        {
            int index = Rows.IndexOf(node);
            if (index >= 0 || node == _root) // ignore invisible nodes
            {
                var nodes = node.AllVisibleChildren.ToArray();
                Rows.InsertRange(index + 1, nodes);
            }
        }

        internal void DropChildrenRows(TreeNode node, bool removeParent)
        {
            int start = Rows.IndexOf(node);
            if (start >= 0 || node == _root) // ignore invisible nodes
            {
                int count = node.VisibleChildrenCount;
                if (removeParent)
                    count++;
                else
                    start++;
                Rows.RemoveRange(start, count);
            }
        }

        private void PactTree_Loaded(object sender, EventArgs e)
        {
            tree1.AlternationCount = 2;

            Style Containerstyle = new Style();
            Containerstyle.Setters.Add(new Setter(HorizontalContentAlignmentProperty, System.Windows.HorizontalAlignment.Stretch));
            Containerstyle.Setters.Add(new Setter(VerticalContentAlignmentProperty, System.Windows.VerticalAlignment.Center));
            Containerstyle.Setters.Add(new Setter(ListViewItem.HeightProperty, RowHeight));


            Trigger t1 = new Trigger();
            t1.Property = ItemsControl.AlternationIndexProperty;
            t1.Value = 0;
            t1.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(AlternateBackground1)));

            Trigger t3 = new Trigger();
            t3.Property = ItemsControl.AlternationIndexProperty;
            t3.Value = 1;
            t3.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(AlternateBackground2)));


            Trigger t2 = new Trigger();
            t2.Property = TreeListItem.IsSelectedProperty;
            t2.Value = true;
            t2.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(SelectedItemColor)));


            Containerstyle.Triggers.Add(t1);
            Containerstyle.Triggers.Add(t3);
            Containerstyle.Triggers.Add(t2);

            tree1.ItemContainerStyle = Containerstyle;

            if (CMenu != null && Command != null)
            {
                ContextMenu menu = new ContextMenu();
                foreach (var item in CMenu)
                {
                    MenuItem CmenuItem = new MenuItem();
                    CmenuItem.Command = Command;
                    CmenuItem.Header = item;
                    CmenuItem.CommandParameter = item;
                    //CmenuItem.Icon = new Image() { Source="" };


                    menu.Items.Add(CmenuItem);
                }

                tree1.ContextMenu = menu;
            }

            cmbViews.ItemsSource = Dataset.Tables[0].AsDataView();
            cmbViews.DisplayMemberPath = "ViewName";
            cmbViews.SelectedValuePath = "GridViewID";

            cmbViews.SelectedItem = cmbViews.Items[0];


        }
        private TreeGridViewColumn sortColumn;

        // The previous column that was sorted.
        private TreeGridViewColumn previousSortColumn;

        // The current direction the header is sorted;
        private ListSortDirection sortDirection;

        private void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {

            // Make sure the column is really being sorted.
            GridViewColumnHeader header = e.OriginalSource as GridViewColumnHeader;

            if (header != null && header.Role != GridViewColumnHeaderRole.Padding)
            {

                TreeGridViewColumn column = header.Column as TreeGridViewColumn;
                if (column == null)
                    return;

                // See if a new column was clicked, or the same column was clicked.
                if (sortColumn != column)
                {
                    // A new column was clicked.
                    previousSortColumn = sortColumn;
                    sortColumn = column;
                    sortDirection = ListSortDirection.Ascending;
                }
                else
                {
                    // The same column was clicked, change the sort order.
                    previousSortColumn = null;
                    sortDirection = (sortDirection == ListSortDirection.Ascending) ?
                        ListSortDirection.Descending : ListSortDirection.Ascending;
                }

                // Sort the data.
                SortList(column.ColumnIndex);

                // Update the column header based on the sort column and order.
                UpdateHeaderTemplate();
            }
            else if (e.OriginalSource is Button)
            {
                //Filter objfilter = new Filter(ColumnsSource);
                //objfilter.Show();
            }

        }

        /// <summary>
        /// Sort the data.
        /// </summary>
        private void SortList(int ColumnIndex)
        {
            Rows.Clear();
            Sort(Root, ColumnIndex);
        }


        void Sort(TreeNode TN, int ColumnIndex)
        {
            int index = Rows.IndexOf(TN);

            TreeNode[] nodes = null;
            if (sortDirection == ListSortDirection.Ascending)
            {
                if (ColumnIndex == 0)
                    nodes = TN.Children.OrderBy(a => a.Field1).ToArray();
                if (ColumnIndex == 1)
                    nodes = TN.Children.OrderBy(a => a.Field2).ToArray();
                if (ColumnIndex == 2)
                    nodes = TN.Children.OrderBy(a => a.Field3).ToArray();
                if (ColumnIndex == 3)
                    nodes = TN.Children.OrderBy(a => a.Field4).ToArray();
                if (ColumnIndex == 4)
                    nodes = TN.Children.OrderBy(a => a.Field5).ToArray();
                if (ColumnIndex == 5)
                    nodes = TN.Children.OrderBy(a => a.Field6).ToArray();
            }
            else
            {
                if (ColumnIndex == 0)
                    nodes = TN.Children.OrderByDescending(a => a.Field1).ToArray();
                if (ColumnIndex == 1)
                    nodes = TN.Children.OrderByDescending(a => a.Field2).ToArray();
                if (ColumnIndex == 2)
                    nodes = TN.Children.OrderByDescending(a => a.Field3).ToArray();
                if (ColumnIndex == 3)
                    nodes = TN.Children.OrderByDescending(a => a.Field4).ToArray();
                if (ColumnIndex == 4)
                    nodes = TN.Children.OrderByDescending(a => a.Field5).ToArray();
                if (ColumnIndex == 5)
                    nodes = TN.Children.OrderByDescending(a => a.Field6).ToArray();
            }

            Rows.InsertRange(index + 1, nodes);

            for (int i = 0; i < TN.Children.Count; i++)
            {
                if (TN.Children[i].IsGroup)
                    Sort(TN.Children[i], ColumnIndex);
            }
        }
        /// <summary>
        /// Update the column header based on the sort column and order.
        /// </summary>
        private void UpdateHeaderTemplate()
        {
            Style headerStyle;

            // Restore the previous header.
            if (previousSortColumn != null && previousSortColumn.SortStyle != null)
            {
                headerStyle = this.TryFindResource(previousSortColumn.SortStyle) as Style;
                if (headerStyle != null)
                    previousSortColumn.HeaderContainerStyle = headerStyle;
            }

            // Update the current header.
            if (sortColumn.SortStyle != null)
            {
                // The name of the resource to use for the header.
                string resourceName = sortColumn.SortStyle + sortDirection.ToString();

                headerStyle = this.TryFindResource(resourceName) as Style;
                if (headerStyle != null)
                    sortColumn.HeaderContainerStyle = headerStyle;
            }
        }

        private void tree1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tree1.SelectedItems.Count > 0)
                SelectedNode = tree1.SelectedItems[0] as TreeNode;

        }

        private void tree1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (tree1.SelectedItem != null)
            {
                if (TreeNodeToMove != null)
                    ChangeColor(TreeNodeToMove, GroupColorBrush, ItemColorBrush);

                if (TreeNodeToMove == null || TreeNodeToMove != (TreeNode)tree1.SelectedItem)
                {
                    ChangeColor((TreeNode)tree1.SelectedItem, MovingItemColorBrush, MovingItemColorBrush);
                    TreeNodeToMove = (TreeNode)tree1.SelectedItem;
                }
                else
                    TreeNodeToMove = null;

            }
        }
        void ChangeColor(TreeNode TNode, System.Windows.Media.SolidColorBrush GroupBrush, System.Windows.Media.SolidColorBrush ItemBrush)
        {
            if (TNode.IsGroup)
            {
                TNode.ColorBrush = GroupBrush;
            }
            else
            {
                TNode.ColorBrush = ItemBrush;
            }

            if (TNode.Owner != null)
            {
                if (TNode.IsGroup)
                {
                    TNode.Owner.Foreground = GroupBrush;
                }
                else
                {
                    TNode.Owner.Foreground = ItemBrush;
                }

            }
            else
            {

            }
            foreach (var childnode in TNode.Children)
            {
                ChangeColor(childnode, GroupBrush, ItemBrush);
            }
        }

        public void Search()
        {
            int j = tree1.SelectedIndex;
            if (j == -1)
                j = 0;
            for (int i = 0; i < AllTreeNodes.Count; i++)
            {
                if (j < AllTreeNodes.Count - 1)
                    j++;
                else
                    j = 0;

                string name = "";
                if (SearchCol == 1)
                {

                    name = AllTreeNodes[j].Field1;
                }
                else if (SearchCol == 2)
                {

                    name = AllTreeNodes[j].Field2;
                }
                else if (SearchCol == 3)
                {
                    name = AllTreeNodes[j].Field3;
                }
                else if (SearchCol == 4)
                {

                    name = AllTreeNodes[j].Field4;
                }
                else if (SearchCol == 4)
                {
                    name = AllTreeNodes[j].Field5;
                }
                if ((SearchCol == 0 && ((AllTreeNodes[j].Field1 != null && AllTreeNodes[j].Field1.ToLower().Contains(SearchText.ToLower()))
                    || (AllTreeNodes[j].Field2 != null && AllTreeNodes[j].Field2.ToLower().Contains(SearchText.ToLower()))
                    || (AllTreeNodes[j].Field3 != null && AllTreeNodes[j].Field3.ToLower().Contains(SearchText.ToLower()))
                    || (AllTreeNodes[j].Field4 != null && AllTreeNodes[j].Field4.ToLower().Contains(SearchText.ToLower()))
                    || (AllTreeNodes[j].Field5 != null && AllTreeNodes[j].Field5.ToLower().Contains(SearchText.ToLower()))
                    ))
                || name.ToLower().Contains(SearchText.ToLower()))
                {
                    if (!Rows.Contains(AllTreeNodes[j]))
                    {
                        collapsedParents.Clear();
                        SetVisible(AllTreeNodes[j]);
                    }
                    tree1.SelectedItem = AllTreeNodes[j];
                    tree1.ScrollIntoView(AllTreeNodes[j]);
                    return;
                }

            }
        }

        List<TreeNode> collapsedParents = new List<TreeNode>();
        void SetVisible(TreeNode Tn)
        {
            collapsedParents.Insert(0, Tn);
            if (Tn.Parent != null)
            {
                if (!Rows.Contains(Tn.Parent))
                    SetVisible(Tn.Parent);
                else
                {
                    if (!Tn.Parent.IsExpanded)
                        SetIsExpanded(Tn.Parent, true);
                    for (int i = 0; i < collapsedParents.Count(); i++)
                    {
                        if (!collapsedParents[i].IsExpanded)
                            SetIsExpanded(collapsedParents[i], true);
                    }
                }
            }
            else
            {
                SetIsExpanded(Root, true);
                for (int i = 0; i < collapsedParents.Count(); i++)
                {
                    if (!collapsedParents[i].IsExpanded)
                        SetIsExpanded(collapsedParents[i], true);
                }
            }

        }

        private void tree1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control)
            {
                if (e.Key == Key.F)
                {
                    //Search frmsearch = new Search();
                    //frmsearch.Tag = this;
                    //frmsearch.ShowDialog();
                }

            }
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.F3 && SearchText != null)
            {
                Search();
            }
        }


        void CreateColumns(int gridviewid)
        {
            ObservableCollection<ColumnDescriptor> cs = new ObservableCollection<ColumnDescriptor>();

            DataRow[] drr = Dataset.Tables[1].Select("GridViewID=" + gridviewid.ToString());
            ColumnDescriptor cd;
            strCols = "";

            for (short i = 0; i < drr.Count(); i++)
            {
                if (i == 0)
                    strCols = drr[i]["SysColumnName"].ToString();
                else
                    strCols = strCols + "," + drr[i]["SysColumnName"].ToString();

                cd = new ColumnDescriptor()
                {
                    align = System.Windows.TextAlignment.Left,
                    width = Convert.ToInt32(drr[i]["ColumnWidth"]),
                    HeaderText = drr[i]["UserColumnName"].ToString(),
                    DisplayMember = "Field" + (i + 1).ToString()
                };
                cs.Add(cd);
            }

            ColumnsSource = cs;
            GridViewID = gridviewid;

        }

        public void Filter(string Condition)
        {
            List<TreeNode> templstParents = new List<TreeNode>();
            int ColumnCount = DataSource.Columns.Count - 19;
            DataRow[] drr = DataSource.Select(Condition);
            Rows.Clear();
            for (int i = 0; i < drr.Count(); i++)
            {
                TreeNode node = new TreeNode();
                node.Tree = tree1;
                node.Children = new TreeNode.NodeCollection(node);
                node.Nodes = new ReadOnlyCollection<TreeNode>(node.Children);


                node.IsExpandedOnce = true;
                node.AssignIsExpanded(true);

                node.NodeNo = i;

                node.Id = Convert.ToInt32(drr[i][0].ToString());

                node.IsGroup = Convert.ToBoolean(drr[i]["IsGroup"].ToString());
                node.LevelNo = Convert.ToInt16(drr[i]["LevelNo"].ToString());
                node.NodeIndex = drr[i]["NodeIndex"].ToString();

                if (ColumnCount > 0)
                    node.Field1 = drr[i][1].ToString();
                if (ColumnCount > 1)
                    node.Field2 = drr[i][2].ToString();
                if (ColumnCount > 2)
                    node.Field3 = drr[i][3].ToString();
                if (ColumnCount > 3)
                    node.Field4 = drr[i][4].ToString();
                if (ColumnCount > 4)
                    node.Field5 = drr[i][5].ToString();
                if (ColumnCount > 5)
                    node.Field6 = drr[i][6].ToString();


                if (node.IsGroup)
                    node.ColorBrush = GroupColorBrush;
                else
                    node.ColorBrush = ItemColorBrush;

                Rows.Add(node);
                AllTreeNodes.Add(node);

                if (node.IsGroup)
                {
                    if (templstParents.Count > 0)
                    {
                        if (node.LevelNo > templstParents[templstParents.Count - 1].LevelNo)
                        {
                            templstParents[templstParents.Count - 1].Children.Add(node);
                            templstParents.Add(node);
                        }
                        else if (node.LevelNo == templstParents[templstParents.Count - 1].LevelNo)
                        {

                            templstParents.RemoveAt(templstParents.Count - 1);
                            templstParents[templstParents.Count - 1].Children.Add(node);
                            templstParents.Add(node);
                        }
                        else if (node.LevelNo < templstParents[templstParents.Count - 1].LevelNo)
                        {
                            for (int j = templstParents.Count - 1; j >= 0; j--)
                            {
                                if (node.LevelNo <= templstParents[j].LevelNo)
                                {
                                    templstParents.RemoveAt(templstParents.Count - 1);
                                }
                                else
                                    break;
                            }
                            templstParents[templstParents.Count - 1].Children.Add(node);
                            templstParents.Add(node);
                        }
                    }
                    else
                        templstParents.Add(node);
                }
                else
                {
                    if (templstParents.Count == 0)
                        continue;

                    if (node.LevelNo > templstParents[templstParents.Count - 1].LevelNo)
                    {
                        templstParents[templstParents.Count - 1].Children.Add(node);
                    }
                    else if (node.LevelNo == templstParents[templstParents.Count - 1].LevelNo)
                    {
                        for (int j = templstParents.Count - 1; j >= 0; j--)
                        {
                            if (node.LevelNo <= templstParents[j].LevelNo)
                            {
                                templstParents.RemoveAt(templstParents.Count - 1);
                            }
                            else
                                break;
                        }
                        templstParents[templstParents.Count - 1].Children.Add(node);
                    }
                    else if (node.LevelNo < templstParents[templstParents.Count - 1].LevelNo)
                    {
                        for (int j = templstParents.Count - 1; j >= 0; j--)
                        {
                            if (node.LevelNo <= templstParents[j].LevelNo)
                            {
                                templstParents.RemoveAt(templstParents.Count - 1);
                            }
                            else
                                break;
                        }
                        templstParents[templstParents.Count - 1].Children.Add(node);
                    }

                }

            }
            if (templstParents.Count > 0)
            {
                Root.Children.Clear();
                Root.Children.Add(templstParents[0]);
            }
        }

        private void cmbViews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbViews.SelectedValue != null)
            {
                CreateColumns(Convert.ToInt32(cmbViews.SelectedValue.ToString()));

                tree1.ItemsSource = Rows;

                Style headercontainerStyle = this.TryFindResource("FamilyDataGridViewColumnHeader") as Style;
                if (headercontainerStyle != null)
                    foreach (var item in ((System.Windows.Controls.GridView)(((System.Windows.Controls.ListView)(tree1)).View)).Columns)
                    {
                        item.HeaderContainerStyle = headercontainerStyle;

                    }
            }
        }


    }
}
