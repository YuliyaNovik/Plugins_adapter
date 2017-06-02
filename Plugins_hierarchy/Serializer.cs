using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using PluginApi;

namespace Plugins_hierarchy.Class {
    public class Serializer {
        public static void Serialize(Object obj, string fileName) {
            List<Type> types = new List<Type>();
            List<Object> objectList = new List<Object>();

            if (obj is IEnumerable) {
                foreach (var item in (IEnumerable)obj) {
                    types.Add(item.GetType());
                    objectList.Add((Object)item);
                } 
            }

            XmlSerializer serializer = new XmlSerializer(objectList.GetType(), types.ToArray());
            using (StreamWriter sw = new StreamWriter(fileName)) {
                serializer.Serialize(sw, objectList);
            }

            PluginApi.Api.GetInstance().processFunctionalPlugins(fileName, fileName+".json");
        }

        public static Object Deserialize(List<Type> obj, string fileName) {        
            XmlSerializer serializer = new XmlSerializer(typeof(List<Object>), obj.ToArray());
            using (StreamReader sr = new StreamReader(fileName)) {
                return serializer.Deserialize(sr);
            }
        }

        public static Stream StringToStream(string src) {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }
    }
}
