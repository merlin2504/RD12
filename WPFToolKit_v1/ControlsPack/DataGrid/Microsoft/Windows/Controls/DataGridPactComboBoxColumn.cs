//---------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
//
//---------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;

namespace Microsoft.Windows.Controls
{
    /// <summary>
    ///     A column that displays editable text.
    /// </summary>
    public class DataGridPactComboBoxColumn : DataGridBoundColumn
    {
        static DataGridPactComboBoxColumn()
        {
            ElementStyleProperty.OverrideMetadata(typeof(DataGridPactComboBoxColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
            EditingElementStyleProperty.OverrideMetadata(typeof(DataGridPactComboBoxColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
        }

        #region Binding
        private bool _selectedValueBindingEnsured = false;
        private BindingBase _selectedValueBinding;

        /// <summary>
        ///     The binding that will be applied to the SelectedValue property of the ComboBox.  This works in conjunction with SelectedValuePath  
        /// </summary>
        /// <remarks>
        ///     This isn't a DP because if it were getting the value would evaluate the binding.
        /// </remarks>
        public virtual BindingBase SelectedValueBinding
        {
            get
            {
                if (!_selectedValueBindingEnsured)
                {
                    if (!IsReadOnly)
                    {
                        DataGridHelper.EnsureTwoWay(_selectedValueBinding);
                    }

                    _selectedValueBindingEnsured = true;
                }

                return _selectedValueBinding;
            }

            set
            {
                if (_selectedValueBinding != value)
                {
                    BindingBase oldBinding = _selectedValueBinding;
                    _selectedValueBinding = value;
                    CoerceValue(SortMemberPathProperty);
                    _selectedValueBindingEnsured = false;
                    OnSelectedValueBindingChanged(oldBinding, _selectedValueBinding);
                }
            }
        }

        /// <summary>
        ///     Called when SelectedValueBinding changes.
        /// </summary>
        /// <param name="oldBinding">The old binding.</param>
        /// <param name="newBinding">The new binding.</param>
        protected virtual void OnSelectedValueBindingChanged(BindingBase oldBinding, BindingBase newBinding)
        {
            NotifyPropertyChanged("SelectedValueBinding");
        }
        #endregion

        #region Styles

        /// <summary>
        ///     The default value of the ElementStyle property.
        ///     This value can be used as the BasedOn for new styles.
        /// </summary>
        public static Style DefaultElementStyle
        {
            get
            {
                if (_defaultElementStyle == null)
                {
                    Style style = new Style(typeof(TextBlock));

                    // Use the same margin used on the TextBox to provide space for the caret
                    style.Setters.Add(new Setter(TextBlock.MarginProperty, new Thickness(2.0, 0.0, 2.0, 0.0)));

                    style.Seal();
                    _defaultElementStyle = style;
                }

                return _defaultElementStyle;
            }
        }

        /// <summary>
        ///     The default value of the EditingElementStyle property.
        ///     This value can be used as the BasedOn for new styles.
        /// </summary>
        public static Style DefaultEditingElementStyle
        {
            get
            {
                if (_defaultEditingElementStyle == null)
                {
                    Style style = new Style(typeof(TextBox));

                    style.Setters.Add(new Setter(TextBox.BorderThicknessProperty, new Thickness(0.0)));
                    style.Setters.Add(new Setter(TextBox.PaddingProperty, new Thickness(0.0)));

                    style.Seal();
                    _defaultEditingElementStyle = style;
                }

                return _defaultEditingElementStyle;
            }
        }

        #endregion

        #region Element Generation

        /// <summary>
        ///     Creates the visual tree for text based cells.
        /// </summary>
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            TextBlock textBlock = new TextBlock();

            SyncProperties(textBlock);

            ApplyStyle(/* isEditing = */ false, /* defaultToElementStyle = */ false, textBlock);
            ApplyBinding(textBlock, TextBlock.TextProperty);

            return textBlock;
        }

        PactComboBox pcb =null;//= new PactComboBox() { FeatureID=1 };
        /// <summary>
        ///     Creates the visual tree for text based cells.
        /// </summary>
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            //if (pcb == null)
                pcb = new PactComboBox() { FeatureID = this.FeatureID };
                
           
         //   SyncProperties(pcb);

            //ApplyStyle(/* isEditing = */ true, /* defaultToElementStyle = */ false, textBox);
            ApplyBinding(pcb, PactComboBox.TextProperty);
            DataGridComboBoxColumn.ApplyBinding(SelectedValueBinding, pcb, PactComboBox.SelectedValueProperty);
            pcb.SelectedValue = ((System.Data.DataRowView)(cell.RowDataItem)).Row[((System.Windows.Data.Binding)(SelectedValueBinding)).Path.Path].ToString();

            if (pcb.SelectedValue.ToString().Length == 0)
                pcb.SelectedValue = null;
            return pcb;
        }

        private void SyncProperties(FrameworkElement e)
        {
            DataGridHelper.SyncColumnProperty(this, e, TextElement.FontFamilyProperty, FontFamilyProperty);
            DataGridHelper.SyncColumnProperty(this, e, TextElement.FontSizeProperty, FontSizeProperty);
            DataGridHelper.SyncColumnProperty(this, e, TextElement.FontStyleProperty, FontStyleProperty);
            DataGridHelper.SyncColumnProperty(this, e, TextElement.FontWeightProperty, FontWeightProperty);
            DataGridHelper.SyncColumnProperty(this, e, TextElement.ForegroundProperty, ForegroundProperty);
        }

        protected internal override void RefreshCellContent(FrameworkElement element, string propertyName)
        {
            DataGridCell cell = element as DataGridCell;

            if (cell != null)
            {
                FrameworkElement textElement = cell.Content as FrameworkElement;

                if (textElement != null)
                {
                    switch (propertyName)
                    {
                        case "FontFamily":
                            DataGridHelper.SyncColumnProperty(this, textElement, TextElement.FontFamilyProperty, FontFamilyProperty);
                            break;
                        case "FontSize":
                            DataGridHelper.SyncColumnProperty(this, textElement, TextElement.FontSizeProperty, FontSizeProperty);
                            break;
                        case "FontStyle":
                            DataGridHelper.SyncColumnProperty(this, textElement, TextElement.FontStyleProperty, FontStyleProperty);
                            break;
                        case "FontWeight":
                            DataGridHelper.SyncColumnProperty(this, textElement, TextElement.FontWeightProperty, FontWeightProperty);
                            break;
                        case "Foreground":
                            DataGridHelper.SyncColumnProperty(this, textElement, TextElement.ForegroundProperty, ForegroundProperty);
                            break;
                    }
                }
            }

            base.RefreshCellContent(element, propertyName);
        }

        #endregion

        #region Editing

        /// <summary>
        ///     Called when a cell has just switched to edit mode.
        /// </summary>
        /// <param name="editingElement">A reference to element returned by GenerateEditingElement.</param>
        /// <param name="editingEventArgs">The event args of the input event that caused the cell to go into edit mode. May be null.</param>
        /// <returns>The unedited value of the cell.</returns>
        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            PactComboBox textBox = editingElement as PactComboBox;
            if (textBox != null)
            {
                textBox.Focus();

                string originalValue = textBox.Text;

                //TextCompositionEventArgs textArgs = editingEventArgs as TextCompositionEventArgs;
                //if (textArgs != null)
                //{
                //    // If text input started the edit, then replace the text with what was typed.
                //    string inputText = textArgs.Text;
                //    textBox.Text = inputText;

                //    // Place the caret after the end of the text.
                //    textBox.Select(inputText.Length, 0);
                //}
                //else
                //{
                //    // If a mouse click started the edit, then place the caret under the mouse.
                //    MouseButtonEventArgs mouseArgs = editingEventArgs as MouseButtonEventArgs;
                //    if ((mouseArgs == null) || !PlaceCaretOnTextBox(textBox, Mouse.GetPosition(textBox)))
                //    {
                //        // If the mouse isn't over the textbox or something else started the edit, then select the text.
                //        textBox.SelectAll();
                //    }
                //}

                return originalValue;
            }

            return null;
        }

        private static bool PlaceCaretOnTextBox(TextBox textBox, Point position)
        {
            int characterIndex = textBox.GetCharacterIndexFromPoint(position, /* snapToText = */ false);
            if (characterIndex >= 0)
            {
                textBox.Select(characterIndex, 0);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Called when a cell's value is to be committed, just before it exits edit mode.
        /// </summary>
        /// <param name="editingElement">A reference to element returned by GenerateEditingElement.</param>
        /// <returns>false if there is a validation error. true otherwise.</returns>
        protected override bool CommitCellEdit(FrameworkElement editingElement)
        {
            PactComboBox textBox = editingElement as PactComboBox;
            if (textBox != null)
            {
                DataGridHelper.UpdateSource(textBox, PactComboBox.TextProperty);
                DataGridHelper.UpdateSource(textBox, PactComboBox.SelectedValueProperty);
                return !Validation.GetHasError(textBox);
            }

            return true;
        }

        /// <summary>
        ///     Called when a cell's value is to be cancelled, just before it exits edit mode.
        /// </summary>
        /// <param name="editingElement">A reference to element returned by GenerateEditingElement.</param>
        /// <param name="uneditedValue">UneditedValue</param>
        protected override void CancelCellEdit(FrameworkElement editingElement, object uneditedValue)
        {
            PactComboBox textBox = editingElement as PactComboBox;
            if (textBox != null)
            {
                DataGridHelper.UpdateTarget(textBox, PactComboBox.TextProperty);
                DataGridHelper.UpdateTarget(textBox, PactComboBox.SelectedValueProperty);
            }
        }

        internal override void OnInput(InputEventArgs e)
        {
            // Text input will start an edit
            if (e is TextCompositionEventArgs)
            {
                BeginEdit(e);
            }
        }

        #endregion

        #region Element Properties

        /// <summary>
        ///     The DependencyProperty for the FontFamily property.
        ///     Flags:              Can be used in style rules
        ///     Default Value:      System Dialog Font
        /// </summary>
        public static readonly DependencyProperty FontFamilyProperty =
                TextElement.FontFamilyProperty.AddOwner(
                        typeof(DataGridPactComboBoxColumn),
                        new FrameworkPropertyMetadata(SystemFonts.MessageFontFamily, FrameworkPropertyMetadataOptions.Inherits, DataGridColumn.NotifyPropertyChangeForRefreshContent));

        /// <summary>
        ///     The font family of the desired font.
        ///     This will only affect controls whose template uses the property
        ///     as a parameter. On other controls, the property will do nothing.
        /// </summary>
        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        /// <summary>
        ///     The DependencyProperty for the FontSize property.
        ///     Flags:              Can be used in style rules
        ///     Default Value:      System Dialog Font Size
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty =
                TextElement.FontSizeProperty.AddOwner(
                        typeof(DataGridPactComboBoxColumn),
                        new FrameworkPropertyMetadata(SystemFonts.MessageFontSize, FrameworkPropertyMetadataOptions.Inherits, DataGridColumn.NotifyPropertyChangeForRefreshContent));

        /// <summary>
        ///     The size of the desired font.
        ///     This will only affect controls whose template uses the property
        ///     as a parameter. On other controls, the property will do nothing.
        /// </summary>
        [TypeConverter(typeof(FontSizeConverter))]
        [Localizability(LocalizationCategory.None)]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        ///     The DependencyProperty for the FontStyle property.
        ///     Flags:              Can be used in style rules
        ///     Default Value:      System Dialog Font Style
        /// </summary>
        public static readonly DependencyProperty FontStyleProperty =
                TextElement.FontStyleProperty.AddOwner(
                        typeof(DataGridPactComboBoxColumn),
                        new FrameworkPropertyMetadata(SystemFonts.MessageFontStyle, FrameworkPropertyMetadataOptions.Inherits, DataGridColumn.NotifyPropertyChangeForRefreshContent));

        /// <summary>
        ///     The style of the desired font.
        ///     This will only affect controls whose template uses the property
        ///     as a parameter. On other controls, the property will do nothing.
        /// </summary>
        public FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        /// <summary>
        ///     The DependencyProperty for the FontWeight property.
        ///     Flags:              Can be used in style rules
        ///     Default Value:      System Dialog Font Weight
        /// </summary>
        public static readonly DependencyProperty FontWeightProperty =
                TextElement.FontWeightProperty.AddOwner(
                        typeof(DataGridPactComboBoxColumn),
                        new FrameworkPropertyMetadata(SystemFonts.MessageFontWeight, FrameworkPropertyMetadataOptions.Inherits, DataGridColumn.NotifyPropertyChangeForRefreshContent));

        /// <summary>
        ///     The weight or thickness of the desired font.
        ///     This will only affect controls whose template uses the property
        ///     as a parameter. On other controls, the property will do nothing.
        /// </summary>
        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        /// <summary>
        ///     The DependencyProperty for the Foreground property.
        ///     Flags:              Can be used in style rules
        ///     Default Value:      System Font Color
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
                TextElement.ForegroundProperty.AddOwner(
                        typeof(DataGridPactComboBoxColumn),
                        new FrameworkPropertyMetadata(SystemColors.ControlTextBrush, FrameworkPropertyMetadataOptions.Inherits, DataGridColumn.NotifyPropertyChangeForRefreshContent));

        /// <summary>
        ///     An brush that describes the foreground color.
        ///     This will only affect controls whose template uses the property
        ///     as a parameter. On other controls, the property will do nothing.
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        #endregion

        #region Data

        private static Style _defaultElementStyle;
        private static Style _defaultEditingElementStyle;
        public int FeatureID = 0;

        #endregion
    }
}