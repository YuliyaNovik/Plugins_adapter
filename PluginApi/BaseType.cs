  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PluginApi {
    public class BaseType : IPlugin {
        public string title { get; set; }
        public string systemVersion { get; set; }
        public string coreVersion { get; set; } 
        public string systemName { get; set; } 

        public BaseType(){}

        public BaseType(string systemName, string systemVersion, string coreVersion) {
            this.systemName = systemName;
            this.systemVersion = systemVersion;
            this.coreVersion = coreVersion;
        }

        virtual public List<string> getFields() {
            return new List<string>();
        }

        virtual public List<string> getProperties() {
            List<string> props = new List<string>();
            props.Add("title");
            props.Add("systemVersion");
            props.Add("coreVersion");
            props.Add("systemName");
            return props;
        }
    }
    
}
