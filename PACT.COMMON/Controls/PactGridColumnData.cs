﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using Microsoft.Windows.Controls;
using System.Reflection;


namespace PACT.COMMON
{
    public class PactGridColumnData
    {
        [AttachedPropertyBrowsableForType(typeof(DataGrid))]
        public static object GetColumnsSource(DependencyObject obj)
        {
            return (object)obj.GetValue(ColumnsSourceProperty);
        }

        public static void SetColumnsSource(DependencyObject obj, object value)
        {
            obj.SetValue(ColumnsSourceProperty, value);
        }

        public static readonly DependencyProperty ColumnsSourceProperty =
           DependencyProperty.RegisterAttached(
               "ColumnsSource",
               typeof(object),
               typeof(PactGridColumnData),
               new UIPropertyMetadata(
                   null,
                   ColumnsSourceChanged));

        private static void ColumnsSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            DataGrid gridView = obj as DataGrid;
            if (gridView != null)
            {
                gridView.Columns.Clear();
                if (e.NewValue != null)
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(e.NewValue);
                    if (view != null)
                    {
                        CreateColumns(gridView, view);
                    }
                }
            }
        }

        private static void CreateColumns(Microsoft.Windows.Controls.DataGrid gridView, ICollectionView view)
        {
            foreach (var item in view)
            {
                DgColumn obj = item as DgColumn;
                DataGridColumn column = CreateColumn(gridView, item);
                column.IsReadOnly = obj.ReadOnly;
                column.Width = obj.Width;
                column.DisableTab = obj.DisableTab;
                column.Header = obj.Header;
                gridView.Columns.Add(column);
            }
        }

        private static DataGridColumn CreateColumn(DataGrid gridView, object columnSource)
        {
            DgColumn col = columnSource as DgColumn;
            if (col.Control == "TextBlock")
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Binding = new Binding(col.DisplayMember);
                return column;
            }
            else if (col.Control == "PactComboBox")
            {
                DataGridPactComboBoxColumn column = new DataGridPactComboBoxColumn();
                column.Binding = new Binding(col.DisplayMember);
                column.FeatureID = Convert.ToInt32(col.Param);
                column.SelectedValueBinding = new Binding(col.DisplayMember + "_Key");
                return column;
            }
            else if (col.Control == "DatePicker")
            {
                DataGridDateColumn column = new DataGridDateColumn();
                column.Binding = new Binding(col.DisplayMember);
               // column.SelectedValueBinding = new Binding(col.DisplayMember + "_Key");
                return column;
            }
            else//if (col.ColumnType == "TextBox")
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Binding = new Binding(col.DisplayMember);
                return column;
            }
            //if (col.ColumnType == "MultiCombo")
            //{
            //    CustDataGridComboBoxColumn combcolumn = new CustDataGridComboBoxColumn();
            //    combcolumn.Header = col.HeaderText;
            //    combcolumn.DisplayMemberPath = col.DisplayMember;
            //    combcolumn.SelectedValuePath = col.ValueMember;
            //    combcolumn.SelectedValueBinding = new Binding(col.ValueBinding);

            //    combcolumn.ItemsSource = col.Source;
            //    combcolumn.Width = col.width;

            //    for (int i = 0; i < col.Columns.Count; i++)
            //    {
            //        GridViewColumn e = new GridViewColumn();
            //        e.Header = col.Columns[i].HeaderText;
            //        e.DisplayMemberBinding = new Binding(col.Columns[i].DisplayMember);
            //        combcolumn.Columns.Add(e);
            //    }

            //    return combcolumn;

            //}
          
            return null;

        }
    }

    public static class DataGridViewColumns
    {


        // Using a DependencyProperty as the backing store for ColumnsSource.  This enables animation, styling, binding, etc...


        private static object GetPropertyValue(object obj, string propertyName)
        {
            if (obj != null)
            {
                PropertyInfo prop = obj.GetType().GetProperty(propertyName);
                if (prop != null)
                    return prop.GetValue(obj, null);
            }
            return null;
        }
    }

}
