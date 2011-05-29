using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using PACT.VIEWMODEL;
using System.Globalization;
using System.Threading;
using System.Windows.Markup;
using PACT.COMMON;

namespace PACT.VIEW
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string strCulture = "fa-IR"; //Arabic
          //  strCulture = "en-US"; //English
            CultureInfo culture = null;
            culture = new CultureInfo(strCulture);
            if (culture != null)
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            ViewResourceManager oViewResourceManager = new ViewResourceManager();
            ViewResourceManager.LoadResources(strCulture);
            ViewResourceManager.strCulture = strCulture;

            // Ensure the current culture passed into bindings is the OS culture.
            // By default, WPF uses en-US as the culture, regardless of the system settings.
            FrameworkElement.LanguageProperty.OverrideMetadata(
              typeof(FrameworkElement),
              new FrameworkPropertyMetadata(
                  XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

           
            base.OnStartup(e);
        }
    }
}
