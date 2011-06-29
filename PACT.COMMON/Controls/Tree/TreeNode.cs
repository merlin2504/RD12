using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Collections.Specialized;
using System.Windows.Media;

namespace PACT.COMMON
{

    public sealed class TreeNode : INotifyPropertyChanged
    {
        public int Id;
        public bool IsGroup;
        public int LevelNo;
        public int NodeNo;


        public string Field1
        {
            get;
            set;
        }
        private string _Index;

        public string NodeIndex
        {
            get
            {
                return _Index;
            }

            set
            {
                if (_Index == value)
                    return;
                _Index = value;
                OnPropertyChanged("Index");
            }
        }

        private string _Field2;

        public string Field2
        {
            get
            {
                return _Field2;
            }

            set
            {
                if (_Field2 == value)
                    return;
                _Field2 = value;
                OnPropertyChanged("Field2");
            }
        }


        private string _Field3;

        public string Field3
        {
            get
            {
                return _Field3;
            }

            set
            {
                if (_Field3 == value)
                    return;
                _Field3 = value;
                OnPropertyChanged("Field3");
            }
        }



        private string _Field4;

        public string Field4
        {
            get
            {
                return _Field4;
            }

            set
            {
                if (_Field4 == value)
                    return;
                _Field4 = value;
                OnPropertyChanged("Field4");
            }
        }



        private string _Field5;

        public string Field5
        {
            get
            {
                return _Field5;
            }

            set
            {
                if (_Field5 == value)
                    return;
                _Field5 = value;
                OnPropertyChanged("Field5");
            }
        }


        private string _Field6;

        public string Field6
        {
            get
            {
                return _Field6;
            }

            set
            {
                if (_Field6 == value)
                    return;
                _Field6 = value;
                OnPropertyChanged("Field6");
            }
        }

        #region NodeCollection
        public class NodeCollection : Collection<TreeNode>
        {



            private TreeNode _owner;

            public NodeCollection(TreeNode owner)
            {
                _owner = owner;
            }

            protected override void ClearItems()
            {
                while (this.Count != 0)
                    this.RemoveAt(this.Count - 1);
            }

            protected override void InsertItem(int index, TreeNode item)
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                if (item.Parent != _owner)
                {
                    if (item.Parent != null)
                        item.Parent.Children.Remove(item);
                    item._parent = _owner;
                    item._index = index;
                    for (int i = index; i < Count; i++)
                        this[i]._index++;
                    base.InsertItem(index, item);
                }
            }

            protected override void RemoveItem(int index)
            {
                TreeNode item = this[index];
                item._parent = null;
                item._index = -1;
                for (int i = index + 1; i < Count; i++)
                    this[i]._index--;
                base.RemoveItem(index);
            }

            protected override void SetItem(int index, TreeNode item)
            {
                if (item == null)
                    throw new ArgumentNullException("item");
                RemoveAt(index);
                InsertItem(index, item);
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Properties

        private object _tree;
        public object Tree
        {
            get
            {
                return _tree;
            }
            set
            {
                _tree = value;
            }
        }

        private INotifyCollectionChanged _childrenSource;
        internal INotifyCollectionChanged ChildrenSource
        {
            get
            {
                return _childrenSource;
            }
            set
            {
                if (_childrenSource != null)
                    _childrenSource.CollectionChanged -= ChildrenChanged;

                _childrenSource = value;

                if (_childrenSource != null)
                    _childrenSource.CollectionChanged += ChildrenChanged;
            }
        }

        private int _index = -1;
        public int Index
        {
            get
            {
                return _index;
            }
        }

        /// <summary>
        /// Returns true if all parent nodes of this node are expanded.
        /// </summary>
        internal bool IsVisible
        {
            get
            {
                TreeNode node = _parent;
                while (node != null)
                {
                    if (!node.IsExpanded)
                        return false;
                    node = node.Parent;
                }
                return true;
            }
        }

        public bool IsExpandedOnce
        {
            get;
            set;
        }

        public bool HasChildren
        {
            get;
            internal set;
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                if (value != IsExpanded)
                {
                    dynamic objtree = Tree;
                    objtree.SetIsExpanded(this, value);
                    OnPropertyChanged("IsExpanded");
                    OnPropertyChanged("IsExpandable");
                }
            }
        }

        public void AssignIsExpanded(bool value)
        {
            _isExpanded = value;
        }

        public bool IsExpandable
        {
            get
            {
                return IsGroup;// (HasChildren && !IsExpandedOnce) || Nodes.Count > 0;
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }


        private TreeNode _parent;
        public TreeNode Parent
        {
            get
            {
                return _parent;
            }
        }

        public int Level
        {
            get
            {
                if (_parent == null)
                    return -1;
                else
                    return _parent.Level + 1;
                // return LevelNo;
            }
        }

        public TreeNode PreviousNode
        {
            get
            {
                if (_parent != null)
                {
                    int index = Index;
                    if (index > 0)
                        return _parent.Nodes[index - 1];
                }
                return null;
            }
        }

        public TreeNode NextNode
        {
            get
            {
                if (_parent != null)
                {
                    int index = Index;
                    if (index < _parent.Nodes.Count - 1)
                        return _parent.Nodes[index + 1];
                }
                return null;
            }
        }

        public TreeNode Next
        {
            get
            {
                if (Nodes.Count > 0)
                    return Nodes[0];

                if (_parent != null)
                {
                    int index = Index;
                    if (index < _parent.Nodes.Count - 1)
                        return _parent.Nodes[index + 1];
                    else if (index == _parent.Nodes.Count && _parent._parent != null && _parent.Index < _parent._parent.Nodes.Count - 1)
                        return _parent._parent.Nodes[_parent.Index + 1];
                }
                return null;
            }
        }

        internal TreeNode BottomNode
        {
            get
            {
                TreeNode parent = this.Parent;
                if (parent != null)
                {
                    if (parent.NextNode != null)
                        return parent.NextNode;
                    else
                        return parent.BottomNode;
                }
                return null;
            }
        }

        internal TreeNode NextVisibleNode
        {
            get
            {
                if (IsExpanded && Nodes.Count > 0)
                    return Nodes[0];
                else
                {
                    TreeNode nn = NextNode;
                    if (nn != null)
                        return nn;
                    else
                        return BottomNode;
                }
            }
        }

        public int VisibleChildrenCount
        {
            get
            {
                return AllVisibleChildren.Count();
            }
        }

        public IEnumerable<TreeNode> AllVisibleChildren
        {
            get
            {
                int level = this.Level;
                TreeNode node = this;
                while (true)
                {
                    node = node.NextVisibleNode;
                    if (node != null && node.Level > level)
                        yield return node;
                    else
                        break;
                }
            }
        }

        private object _tag;
        public object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        private Collection<TreeNode> _children;
        public Collection<TreeNode> Children
        {
            get
            {
                return _children;
            }
            set
            {
                _children = value;
            }
        }

        private ReadOnlyCollection<TreeNode> _nodes;
        public ReadOnlyCollection<TreeNode> Nodes
        {
            get
            {
                return _nodes;
            }
            set
            {
                _nodes = value;
            }
        }

        #endregion


        public override string ToString()
        {
            if (Tag != null)
                return Tag.ToString();
            else
                return base.ToString();
        }

        void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        int index = e.NewStartingIndex;
                        //int rowIndex = Tree.Rows.IndexOf(this);
                        //foreach (object obj in e.NewItems)
                        //{
                        //    //Tree.InsertNewNode(this, obj, rowIndex, index);
                        //    index++;
                        //}
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (Children.Count > e.OldStartingIndex)
                        RemoveChildAt(e.OldStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    while (Children.Count > 0)
                        RemoveChildAt(0);
                    //Tree.CreateChildrenNodes(this);
                    break;
            }
            HasChildren = Children.Count > 0;
            OnPropertyChanged("IsExpandable");
        }

        private void RemoveChildAt(int index)
        {
            var child = Children[index];
            // Tree.DropChildrenRows(child, true);
            ClearChildrenSource(child);
            Children.RemoveAt(index);
        }

        private void ClearChildrenSource(TreeNode node)
        {
            node.ChildrenSource = null;
            foreach (var n in node.Children)
                ClearChildrenSource(n);
        }

        public SolidColorBrush ColorBrush
        {
            get
            {
                return Brush;
            }
            set
            {
                if (value != Brush)
                {
                    Brush = value;
                    OnPropertyChanged("ColorBrush");
                }
            }
        }

        SolidColorBrush Brush;

        public TreeListItem Owner
        {
            get;
            set;

        }


    }
}
