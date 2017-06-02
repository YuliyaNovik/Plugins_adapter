using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PluginApi;

namespace ElementaryOSPlugin {
    public class Ubuntu : BaseType, IPlugin {
        public string imageName { get; set; }
        public string namingRules { get; set; }

        public Ubuntu() {}

        public Ubuntu(string systemName, string systemVersion, string coreVersion, string imageName, string namingRules) : 
               base(systemName, systemVersion, coreVersion) {
            this.imageName = imageName;
            this.namingRules = namingRules;
        }

        override public List<string> getFields() {
            List<string> fields = base.getFields();
            return fields;
        }

        override public List<string> getProperties() {
            List<string> props = base.getProperties();
            props.Add("imageName");
            props.Add("namingRules");
            return props;
        }
    }
    
}
