using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Reflection;
using System.ComponentModel;
using System.Globalization;
using System.Diagnostics;

namespace PACT.Globalization
{
    public class MarkupExtensionManager
    {
        #region Member Variables

        
        private List<ManagedMarkupExtension> _extensions = new List<ManagedMarkupExtension>();

        
        private int _cleanupCount;

        
        private int _cleanupInterval = 40;

        #endregion

        #region Public Interface

        public MarkupExtensionManager(int cleanupInterval)
        {
            _cleanupInterval = cleanupInterval;
        }


        public virtual void UpdateAllTargets()
        {
            foreach (ManagedMarkupExtension extension in _extensions)
            {
                extension.UpdateTarget();
            }
        }


        public List<ManagedMarkupExtension> ActiveExtensions
        {
            get { return _extensions; }
        }
        public void CleanupInactiveExtensions()
        {
            List<ManagedMarkupExtension> newExtensions = new List<ManagedMarkupExtension>(_extensions.Count);
            foreach (ManagedMarkupExtension ext in _extensions)
            {
                if (ext.IsTargetAlive)
                {
                    newExtensions.Add(ext);
                }
            }
            _extensions = newExtensions;
        }

        
        internal void RegisterExtension(ManagedMarkupExtension extension)
        {  
           
            if (_cleanupCount > _cleanupInterval)
            {
                CleanupInactiveExtensions();
                _cleanupCount = 0;
            }
            _extensions.Add(extension);
            _cleanupCount++;
        }

        #endregion
    }
}
