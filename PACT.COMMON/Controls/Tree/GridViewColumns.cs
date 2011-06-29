using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Reflection;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace PACT.COMMON
{
    public static class GridViewColumns
    {
        [AttachedPropertyBrowsableForType(typeof(GridView))]
        public static bool GetVerticalLines(DependencyObject obj)
        {
            return (bool)obj.GetValue(VerticalLinesProperty);
        }

        public static void SetVerticalLines(DependencyObject obj, object value)
        {
            obj.SetValue(VerticalLinesProperty, value);

        }


        [AttachedPropertyBrowsableForType(typeof(GridView))]
        public static bool GetHorizontalLines(DependencyObject obj)
        {
            return (bool)obj.GetValue(HorizontalLinesProperty);
        }

        public static void SetHorizontalLines(DependencyObject obj, object value)
        {
            obj.SetValue(HorizontalLinesProperty, value);

        }

        public static readonly DependencyProperty VerticalLinesProperty =
            DependencyProperty.RegisterAttached("VerticalLines", typeof(bool), typeof(GridViewColumns), new UIPropertyMetadata(false));


        public static readonly DependencyProperty HorizontalLinesProperty =
            DependencyProperty.RegisterAttached("HorizontalLines", typeof(bool), typeof(GridViewColumns), new UIPropertyMetadata(false));


        [AttachedPropertyBrowsableForType(typeof(GridView))]
        public static object GetColumnsSource(DependencyObject obj)
        {
            return (object)obj.GetValue(ColumnsSourceProperty);
        }

        public static void SetColumnsSource(DependencyObject obj, object value)
        {
            obj.SetValue(ColumnsSourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for ColumnsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsSourceProperty =
            DependencyProperty.RegisterAttached(
                "ColumnsSource",
                typeof(object),
                typeof(GridViewColumns),
                new UIPropertyMetadata(
                    null,
                    ColumnsSourceChanged));





        private static void ColumnsSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            GridView gridView = obj as GridView;
            if (gridView != null && e.NewValue != null)
            {
                CreateColumns(gridView, e.NewValue);
            }
        }

        private static void CreateColumns(GridView gridView, object view)
        {
            ObservableCollection<ColumnDescriptor> COLS = (ObservableCollection<ColumnDescriptor>)view;

            gridView.Columns.Clear();

            for (int i = 0; i < COLS.Count; i++)
            {

                TreeGridViewColumn column = new TreeGridViewColumn();
                column.Header = COLS[i].HeaderText;
                column.ColumnIndex = i;
                column.SortProperty = COLS[i].DisplayMember;
                column.SortStyle = "FamilyDataGridViewColumnHeader";
                DataTemplate d = new DataTemplate();
                if (i == 0)
                {

                    FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
                    borderFactory.SetValue(Border.MarginProperty, new Thickness(-6, -3, -6, -3));

                    if (GetHorizontalLines(gridView))
                        borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(0, 1, 0, 0));

                    borderFactory.SetValue(Border.BorderBrushProperty, System.Windows.Media.Brushes.Black);


                    FrameworkElementFactory spFactory = new FrameworkElementFactory(typeof(StackPanel));
                    spFactory.SetValue(StackPanel.MarginProperty, new Thickness(6, 2, 6, 2));

                    spFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
                    spFactory.SetValue(StackPanel.ToolTipProperty, COLS[i].align.ToString());
                    FrameworkElementFactory RowExpanderFactory = new FrameworkElementFactory(typeof(RowExpander));
                    FrameworkElementFactory TextblockFactory = new FrameworkElementFactory(typeof(TextBlock));
                    TextblockFactory.SetBinding(TextBlock.TextProperty, new Binding(COLS[i].DisplayMember));

                    spFactory.AppendChild(RowExpanderFactory);
                    spFactory.AppendChild(TextblockFactory);
                    borderFactory.AppendChild(spFactory);
                    d.VisualTree = borderFactory;
                }
                else
                {

                    FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
                    borderFactory.SetValue(Border.MarginProperty, new Thickness(-6, -3, -6, -3));

                    if (GetHorizontalLines(gridView) && GetVerticalLines(gridView))
                        borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(1, 1, 0, 0));
                    else if (GetHorizontalLines(gridView))
                        borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(0, 1, 0, 0));
                    else if (GetVerticalLines(gridView))
                        borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(1, 0, 0, 0));


                    borderFactory.SetValue(Border.BorderBrushProperty, System.Windows.Media.Brushes.Black);


                    //Border b;
                    //b.Margin
                    FrameworkElementFactory TextblockFactory = new FrameworkElementFactory(typeof(TextBlock));
                    TextblockFactory.SetBinding(TextBlock.TextProperty, new Binding(COLS[i].DisplayMember));
                    TextblockFactory.SetValue(TextBlock.MarginProperty, new Thickness(6, 2, 6, 2));

                    TextblockFactory.SetValue(TextBlock.TextAlignmentProperty, COLS[i].align);
                    TextblockFactory.SetValue(TextBlock.ToolTipProperty, COLS[i].align.ToString());
                    borderFactory.AppendChild(TextblockFactory);
                    d.VisualTree = borderFactory;
                }

                column.CellTemplate = d;
                column.Width = COLS[i].width;

                gridView.Columns.Add(column);


            }

        }


    }

    public class ColumnDescriptor
    {
        public string HeaderText
        {
            get;
            set;
        }
        public string DisplayMember
        {
            get;
            set;
        }
        public string DBName
        {
            get;
            set;
        }
        public int width
        {
            get;
            set;
        }

        public System.Windows.TextAlignment align
        {
            get;
            set;
        }
    }
    public class TreeGridViewColumn : GridViewColumn
    {
        public int ColumnIndex;

        public string SortProperty
        {
            get
            {
                return (string)GetValue(SortPropertyProperty);
            }
            set
            {
                SetValue(SortPropertyProperty, value);
            }
        }

        public static readonly DependencyProperty SortPropertyProperty =
            DependencyProperty.Register("SortProperty",
            typeof(string), typeof(TreeGridViewColumn));

        public string SortStyle
        {
            get
            {
                return (string)GetValue(SortStyleProperty);
            }
            set
            {
                SetValue(SortStyleProperty, value);
            }
        }

        public static readonly DependencyProperty SortStyleProperty =
            DependencyProperty.Register("SortStyle",
            typeof(string), typeof(TreeGridViewColumn));
    }
}
