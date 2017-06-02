using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using PluginApi;

namespace XmlToJsonPlugin {
    class XmlToJSON : IFuncPlugin {
        public void processStream(string fileNameIn, string fileNameOut) {
            XmlToJson(fileNameIn, fileNameOut);
        }
        
        private void XmlToJson(string fileName, string fileNameOut) {
            string xmlStr = File.ReadAllText(fileName);
            File.WriteAllText(fileNameOut, new JavaScriptSerializer().Serialize(GetXmlValues(XElement.Parse(xmlStr))));
        }

        private Dictionary<string, object> GetXmlValues(XElement xml) {
            var attr = xml.Attributes().ToDictionary(
                d => d.Name.LocalName,
                d => (object)d.Value
            );

            if (xml.HasElements) {
                attr.Add("_value", xml.Elements().Select(
                    e => GetXmlValues(e)
                ));
            } else if (!xml.IsEmpty) {
                attr.Add("_value", xml.Value);
            }

            return new Dictionary<string, object> { { xml.Name.LocalName, attr } };
        }
    }
}