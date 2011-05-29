using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Collections;
using System.Globalization;

namespace PACT.COMMON
{
    public class ViewResourceManager
    {
        public static string strCulture;
        public static Dictionary<string, string> UIResources = new Dictionary<string, string>();

        public static string GetResource(string strKey)
        {
            return GetResource(strKey, strKey);
        }

        public static string GetResource(string strKey,string strValue)
        {
            string strReturn = "RESOURCE KEY NOT AVAILABLE";
            try
            {
                if (UIResources.ContainsKey(strKey))
                    strReturn = UIResources[strKey];
                else
                {
                    //CommonService.CommonClient 
                }
            }
            catch (Exception ex)
            {

            }
            return strReturn;
        }

        public static string GetResource(string strKey, Dictionary<string, string> DbResources)
        {
            string strReturn = "RESOURCE KEY NOT AVAILABLE";
            try
            {
                if (DbResources.ContainsKey(strKey))
                    strReturn = DbResources[strKey];
                else if (UIResources.ContainsKey(strKey))
                    strReturn = UIResources[strKey];
                else
                {
                    //CommonService.CommonClient 
                }
            }
            catch (Exception ex)
            {

            }
            return strReturn;
        }

        public static bool IsKeyExists(string strKey)
        {
            return UIResources.ContainsKey(strKey);
        }

        public static void LoadResources(string strCulture)
        {
            try
            {
                System.Reflection.Assembly targetAsmbly = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "PACT.Globalization.dll");
                System.Reflection.Assembly satAsmbly = targetAsmbly.GetSatelliteAssembly(new CultureInfo(strCulture));
                string[] resources = satAsmbly.GetManifestResourceNames();
                string strName = "";
                string strValue = "";
                UIResources.Clear();
                foreach (string resource in resources)
                {
                    string baseName = resource.Substring(0, resource.LastIndexOf('.'));
                    ResourceManager resourceManager = new ResourceManager(baseName, satAsmbly);
                    ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
                    IDictionaryEnumerator enumerator = resourceSet.GetEnumerator();

                    while (enumerator.MoveNext())
                    {
                        strName = enumerator.Key.ToString();
                        strValue = enumerator.Value.ToString();
                        UIResources.Add(strName, strValue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
