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

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "PACT.Globalization")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2007/xaml/presentation", "PACT.Globalization")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2008/xaml/presentation", "PACT.Globalization")]

namespace PACT.Globalization
{
   
    public abstract class ManagedMarkupExtension : MarkupExtension
    {

        #region Member Variables
                
        private List<WeakReference> _targetObjects = new List<WeakReference>();

        
        private object _targetProperty;
        #endregion

        #region Public Interface

        
        public ManagedMarkupExtension(MarkupExtensionManager manager)
        {
            manager.RegisterExtension(this);
        }

        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var targetHelper = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (targetHelper.TargetObject != null)
            {
                _targetProperty = targetHelper.TargetProperty;

                if (targetHelper.TargetObject is DependencyObject || !(_targetProperty is DependencyProperty))
                {
                    _targetObjects.Add(new WeakReference(targetHelper.TargetObject));
                    return GetValue();
                }
                else
                {
                   return this;
                }
            }
            return null;
        }

        
        public void UpdateTarget()
        {
            if (_targetProperty != null)
            {
                foreach (WeakReference reference in _targetObjects)
                {
                    if (_targetProperty is DependencyProperty)
                    {
                        DependencyObject target = reference.Target as DependencyObject;
                        if (target != null)
                        {
                            target.SetValue(_targetProperty as DependencyProperty, GetValue());
                        }
                    }
                    else if (_targetProperty is PropertyInfo)
                    {
                        object target = reference.Target;
                        if (target != null)
                        {
                            (_targetProperty as PropertyInfo).SetValue(target, GetValue(), null);
                        }
                    }
                }
            }
        }

        
        public bool IsTargetAlive
        {
            get
            {
                foreach (WeakReference reference in _targetObjects)
                {
                    if (reference.IsAlive) return true;
                }
                return false;
            }
        }

        public bool IsInDesignMode
        {
            get
            {
                foreach (WeakReference reference in _targetObjects)
                {
                    DependencyObject element = reference.Target as DependencyObject;
                    if (element != null && DesignerProperties.GetIsInDesignMode(element)) return true;
                }
                return false;
            }
        }

        #endregion

        #region Protected Methods

        
        protected object TargetProperty
        {
            get { return _targetProperty as DependencyProperty; }
        }

        
        protected Type TargetPropertyType
        {
            get
            {
                Type propertyType = null;
                if (_targetProperty is DependencyProperty)
                    propertyType = (_targetProperty as DependencyProperty).PropertyType;
                else if (_targetProperty is PropertyInfo)
                    propertyType = (_targetProperty as PropertyInfo).PropertyType;
                return propertyType;
            }
        }

        
        protected abstract object GetValue();

        #endregion
    }
}
