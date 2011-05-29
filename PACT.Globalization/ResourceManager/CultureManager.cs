using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Forms;

namespace PACT.Globalization
{
    public class CultureManager
    {
        #region Static Member Variables

       
        private static CultureInfo _uiCulture;

        
        private static NotifyIcon _cultureNotifyIcon = null;

       
        private static bool _synchronizeThreadCulture = true;

        #endregion

        #region Public Interface

       
        public static event EventHandler UICultureChanged;

        
        public static CultureInfo UICulture
        {
            get
            {
                if (_uiCulture == null)
                {
                    _uiCulture = Thread.CurrentThread.CurrentUICulture;
                }
                return _uiCulture;
            }
            set
            {
                if (value != UICulture)
                {
                    _uiCulture = value;
                    Thread.CurrentThread.CurrentUICulture = value;
                    if (SynchronizeThreadCulture)
                    {
                        SetThreadCulture(value);
                    }
                    UICultureExtension.UpdateAllTargets();
                    ResxExtension.UpdateAllTargets();
                    if (UICultureChanged != null)
                    {
                        UICultureChanged(null, EventArgs.Empty);
                    }
                    UpdateNotifyIconMenu();
                }
            }
        }

       
        public static bool SynchronizeThreadCulture
        {
            get { return _synchronizeThreadCulture; }
            set
            {
                _synchronizeThreadCulture = value;
                if (value)
                {
                    SetThreadCulture(UICulture);
                }
            }
        }

        #endregion

        #region Local Methods

        
        private static void SetThreadCulture(CultureInfo value)
        {
            if (value.IsNeutralCulture)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(value.Name);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = value;
            }
        }

       
        private static void AddUICultureMenuItem(ContextMenuStrip menuStrip)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem(UICulture.DisplayName);
            menuItem.Checked = true;
            menuItem.CheckOnClick = true;
            menuItem.Tag = UICulture;
            menuItem.CheckedChanged += new EventHandler(OnCultureMenuCheckChanged);
            menuStrip.Items.Insert(menuStrip.Items.Count - 2, menuItem);
        }

       
        private static void UpdateNotifyIconMenu()
        {
            if (_cultureNotifyIcon != null)
            {
                ContextMenuStrip menuStrip = _cultureNotifyIcon.ContextMenuStrip;
                bool cultureItemExists = false;
                foreach (ToolStripItem item in menuStrip.Items)
                {
                    ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                    if (menuItem != null)
                    {
                        menuItem.Checked = (menuItem.Tag == UICulture);
                        if (menuItem.Checked)
                        {
                            cultureItemExists = true;
                        }
                    }
                }
                if (!cultureItemExists)
                {
                    AddUICultureMenuItem(menuStrip);
                }
            }
        }

       
        private static void OnCultureNotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _cultureNotifyIcon.ContextMenuStrip.Show(Control.MousePosition);
            }
        }

        private static void OnCultureMenuCheckChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem.Checked)
            {
                UICulture = menuItem.Tag as CultureInfo;
            }
        }

        

        #endregion
    }
}
