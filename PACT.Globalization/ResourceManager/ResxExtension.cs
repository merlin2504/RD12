using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Reflection;
using System.Resources;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "PACT.Globalization")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2007/xaml/presentation", "PACT.Globalization")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2008/xaml/presentation", "PACT.Globalization")]

namespace PACT.Globalization
{
   
    public delegate object GetResourceHandler(string resxName, string key, CultureInfo culture);

   
    [MarkupExtensionReturnType(typeof(object))]
    public class ResxExtension : ManagedMarkupExtension
    {
        #region Member Variables

   
        private string _resxName;

   
        private string _key;

   
        private string _defaultValue;

   
        private ResourceManager _resourceManager;

   
        private static Dictionary<string, WeakReference> _resourceManagers = new Dictionary<string, WeakReference>();

   
        private static MarkupExtensionManager _markupManager = new MarkupExtensionManager(40);


        #endregion

        #region Public Interface

   
        public static event GetResourceHandler GetResource;

   
        public ResxExtension()
            : base(_markupManager)
        {
        }

   
        public ResxExtension(string resxName, string key, string defaultValue)
            : base(_markupManager)
        {
            this._resxName = resxName;
            this._key = key;
            if (!string.IsNullOrEmpty(defaultValue))
            {
                this._defaultValue = defaultValue;
            }
        }


   
        public string ResxName
        {
            get { return _resxName; }
            set { _resxName = value; }
        }

   
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

   
        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

   
        public static MarkupExtensionManager MarkupManager
        {
            get { return _markupManager; }
        }

   
        public static void UpdateAllTargets()
        {
            _markupManager.UpdateAllTargets();
        }

   
        public static void UpdateTarget(string key)
        {
            foreach (ResxExtension target in _markupManager.ActiveExtensions)
            {
                if (target.Key == key)
                {
                    target.UpdateTarget();
                }
            }
        }

        #endregion

        #region Local Methods

   
        private bool HasEmbeddedResx(Assembly assembly, string resxName)
        {
            try
            {
                string[] resources = assembly.GetManifestResourceNames();
                string searchName = resxName.ToLower() + ".resources";
                foreach (string resource in resources)
                {
                    if (resource.ToLower() == searchName) return true;
                }
            }
            catch
            {
   
            }
            return false;
        }

   
        private Assembly FindResourceAssembly()
        {
            Assembly assembly = Assembly.GetEntryAssembly();

               if (assembly != null && HasEmbeddedResx(assembly, ResxName)) return assembly;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly searchAssembly in assemblies)
            {
   
                string name = searchAssembly.FullName;
                if (!name.StartsWith("Microsoft.") &&
                    !name.StartsWith("System.") &&
                    !name.StartsWith("System,") &&
                    !name.StartsWith("mscorlib,") &&
                    !name.StartsWith("PresentationFramework,") &&
                    !name.StartsWith("WindowsBase,"))
                {
                    if (HasEmbeddedResx(searchAssembly, ResxName)) return searchAssembly;
                }
            }
            return null;
        }

   
        private ResourceManager GetResourceManager(string resxName)
        {
            WeakReference reference = null;
            ResourceManager result = null;
            if (_resourceManagers.TryGetValue(resxName, out reference))
            {
                result = reference.Target as ResourceManager;
                   
                if (result == null)
                {
                    _resourceManagers.Remove(resxName);
                }
            }

            if (result == null)
            {
                Assembly assembly = FindResourceAssembly();
                if (assembly != null)
                {
                    result = new ResourceManager(resxName, assembly);
                }
                _resourceManagers.Add(resxName, new WeakReference(result));
            }
            return result;
        }

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        
        private object ConvertValue(object value)
        {
            object result = null;
            BitmapSource bitmapSource = null;
                   
            if (value is Icon)
            {
                Icon icon = value as Icon;

            
                using (MemoryStream iconStream = new MemoryStream())
                {
                    icon.Save(iconStream);
                    iconStream.Seek(0, SeekOrigin.Begin);
                    bitmapSource = BitmapFrame.Create(iconStream);
                }
            }
            else if (value is Bitmap)
            {
                Bitmap bitmap = value as Bitmap;
                IntPtr bitmapHandle = bitmap.GetHbitmap();
                bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                    bitmapHandle, IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
                bitmapSource.Freeze();
                DeleteObject(bitmapHandle);
            }

            if (bitmapSource != null)
            {
            
                if (TargetPropertyType == typeof(object))
                {
                    System.Windows.Controls.Image imageControl = new System.Windows.Controls.Image();
                    imageControl.Source = bitmapSource;
                    imageControl.Width = bitmapSource.Width;
                    imageControl.Height = bitmapSource.Height;
                    result = imageControl;
                }
                else
                {
                    result = bitmapSource;
                }
            }
            else
            {
                result = value;

                Type targetType = TargetPropertyType;
                if (value is String && targetType != typeof(String) && targetType != typeof(object))
                {
                    TypeConverter tc = TypeDescriptor.GetConverter(targetType);
                    result = tc.ConvertFromInvariantString(value as string);
                }
            }

            return result;
        }

        
        private object GetDefaultValue(string key)
        {
            object result = _defaultValue;
            Type targetType = TargetPropertyType;
            if (_defaultValue == null)
            {
                if (targetType == typeof(String) || targetType == typeof(object))
                {
                    result = "#" + key;
                }
            }
            else if (targetType != null)
            {
                
                if (targetType != typeof(String) && targetType != typeof(object))
                {
                    try
                    {
                        TypeConverter tc = TypeDescriptor.GetConverter(targetType);
                        result = tc.ConvertFromInvariantString(_defaultValue);
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }

        
        protected override object GetValue()
        {
            if (string.IsNullOrEmpty(ResxName))
                throw new ArgumentException("ResxName cannot be null");
            if (string.IsNullOrEmpty(Key))
                throw new ArgumentException("Key cannot be null");

                  

            object result = null;

            try
            {
                object resource = null;
                if (GetResource != null)
                {
                    resource = GetResource(ResxName, Key, CultureManager.UICulture);
                }
                if (resource == null)
                {
                    if (_resourceManager == null)
                    {
                        _resourceManager = GetResourceManager(ResxName);
                    }
                    if (_resourceManager != null)
                    {
                        resource = _resourceManager.GetObject(Key, CultureManager.UICulture);
                    }
                }
                result = ConvertValue(resource);
            }
            catch
            {
            }

            if (result == null)
            {
                result = GetDefaultValue(Key);
            }
            return result;
        }

        #endregion
    }
}
