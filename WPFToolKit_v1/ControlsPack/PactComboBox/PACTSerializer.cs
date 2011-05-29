// ---------------------------------------------------------------------------------------------------------
// File             : PACTSerializer.cs
// Date             : 26-Dec-2009
// Author           : Adil Syed
// -------------------------------------------------------------------------------------------------------
// History
// Name             Date            Reason
// -------------------------------------------------------------------------------------------------------
//   
// ---------------------------------------------------------------------------------------------------------
namespace Microsoft.Windows.Controls
{
    using System;
    using System.IO;
    using System.Text;
    using System.Collections;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Provides serialization and file IO support for all domain
    /// classes in this namespace.
    /// </summary>
    public class PACTSerializer
    {
        /// <summary>
        /// Returns the target namespace for the serializer.
        /// </summary>
        public static string TargetNamespace
        {
            get
            {
                return "http://www.w3.org/2001/XMLSchema";
            }
        }
        /// <summary>
        /// Returns the set of included namespaces for the serializer.
        /// </summary>
        /// <returns>
        /// The set of included namespaces for the serializer.
        /// </returns>
        public static XmlSerializerNamespaces GetNamespaces()
        {
            XmlSerializerNamespaces ns;
            ns = new XmlSerializerNamespaces();
            ns.Add("xs", "http://www.w3.org/2001/XMLSchema");
            ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            return ns;
        }
        /// <summary>
        /// Serializes the <i>Obj</i> to an XML string.
        /// </summary>
        /// <param name="Obj">
        /// The object to serialize.</param>
        /// <param name="ObjType">The object type.</param>
        /// <returns>
        /// The serialized object XML string.
        /// </returns>
        /// <remarks>
        /// The <see cref="PrettyPrint" /> property provides
        /// an easy-to-read formatted XML string.
        /// </remarks>
        public static string ToXml(object Obj, System.Type ObjType)
        {
            XmlSerializer ser;
            ser = new XmlSerializer(ObjType, PACTSerializer.TargetNamespace);
            MemoryStream memStream;
            memStream = new MemoryStream();
            XmlTextWriter xmlWriter;
            xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
            if (PACTSerializer.PrettyPrint)
            {
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.Indentation = 1;
                xmlWriter.IndentChar = Convert.ToChar(9);
            }
            xmlWriter.Namespaces = true;
            ser.Serialize(xmlWriter, Obj, PACTSerializer.GetNamespaces());
            xmlWriter.Close();
            memStream.Close();
            string xml;
            xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
        }

        /// <summary>
        /// Serializes the <i>Obj</i> to an XML string.
        /// </summary>
        /// <param name="Obj">
        /// The object to serialize.</param>
        /// <param name="ObjType">The object type.</param>
        /// <returns>
        /// The serialized object XML string.
        /// </returns>
        /// <remarks>
        /// The <see cref="PrettyPrint" /> property provides
        /// an easy-to-read formatted XML string.
        /// </remarks>
        public static string ToXml(object Obj, System.Type ObjType,bool AllowNameSpace)
        {
            XmlSerializer ser;
            if(AllowNameSpace)
                ser = new XmlSerializer(ObjType, PACTSerializer.TargetNamespace);
            else
            ser = new XmlSerializer(ObjType, "");
            MemoryStream memStream;
            memStream = new MemoryStream();

            XmlWriterSettings xmlws = new XmlWriterSettings();
            xmlws.OmitXmlDeclaration = true;
            xmlws.Encoding = Encoding.UTF8;
            XmlWriter xmlWriter = XmlWriter.Create(memStream, xmlws);
            
            //xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
            //xmlWriter.Settings.OmitXmlDeclaration = true;
            
            //if (PACTSerializer.PrettyPrint)
            //{
            //    xmlWriter.Formatting = Formatting.Indented;
            //    xmlWriter.Indentation = 1;
            //    xmlWriter.IndentChar = Convert.ToChar(9);
                
            //}
         //   xmlWriter.Namespaces = true;


            if (AllowNameSpace)
            {
                ser.Serialize(xmlWriter, Obj, PACTSerializer.GetNamespaces());
            }
            else
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                ser.Serialize(xmlWriter, Obj, ns);
            }

            xmlWriter.Close();
            memStream.Close();
            string xml;
            xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
        }

        /// <summary>
        /// Creates an object from an XML string.
        /// </summary>
        /// <param name="Xml">
        /// XML string to serialize.</param>
        /// <param name="ObjType">The object type to create.</param>
        /// <returns>
        /// An object of type <i>ObjType</i>.
        /// </returns>
        public static object FromXml(string Xml, System.Type ObjType)
        {
            XmlSerializer ser;
            ser = new XmlSerializer(ObjType);
            StringReader stringReader;
            stringReader = new StringReader(Xml);
            XmlTextReader xmlReader;
            xmlReader = new XmlTextReader(stringReader);
            object obj;
            obj = ser.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();
            return obj;
        }
        /// <summary>
        /// The member for the <see cref="PrettyPrint" />
        /// property.
        /// </summary>
        private static bool @__PrettyPrint;
        /// <summary>
        /// Get or sets the pretty print property.
        /// </summary>
        /// <remarks>
        /// If <b>true</b>, the XML output by the
        /// <see cref="ToXml" /> method is indented in
        /// a hierarchichal manner using one tab character
        /// per level, each level being on a new line. 
        /// If <b>false</b>, no indentation or newline
        /// characters are set in the output.
        /// </remarks>
        public static bool PrettyPrint
        {
            get
            {
                return PACTSerializer.@__PrettyPrint;
            }
            set
            {
                PACTSerializer.@__PrettyPrint = value;
            }
        }
    }
}
