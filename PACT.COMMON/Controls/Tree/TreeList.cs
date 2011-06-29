using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Data;

namespace PACT.COMMON
{
    public class TreeList : ListView
    {

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
            DependencyProperty.RegisterAttached("UserID", typeof(int), typeof(TreeList), new UIPropertyMetadata(null));

        public int TypeID
        {
            get
            {
                return (int)GetValue(TypeIDProperty);
            }
            set
            {
                SetValue(TypeIDProperty, value);
            }
        }

        public static readonly DependencyProperty TypeIDProperty =
            DependencyProperty.RegisterAttached("TypeID", typeof(int), typeof(TreeList), new UIPropertyMetadata(null));


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
            DependencyProperty.RegisterAttached("CostCenterID", typeof(int), typeof(TreeList), new UIPropertyMetadata(null));




        public TreeList()
        {
            ItemContainerGenerator.StatusChanged += ItemContainerGeneratorStatusChanged;
        }

        void ItemContainerGeneratorStatusChanged(object sender, EventArgs e)
        {
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {

            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeListItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeListItem;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var ti = element as TreeListItem;
            var node = item as TreeNode;
            if (ti != null && node != null)
            {
                ti.Node = item as TreeNode;
                ti.Node.Owner = ti;
                ti.Tree = this;

                base.PrepareContainerForItemOverride(element, node);
            }
        }

    }


}
