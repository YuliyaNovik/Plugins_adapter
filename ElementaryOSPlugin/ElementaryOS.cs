using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PluginApi;

namespace ElementaryOSPlugin {
    public class ElementaryOS : Ubuntu, IPlugin {
        public string installer { get; set; }
        public ElementaryOS() {}

        public ElementaryOS(string systemName, string systemVersion, string coreVersion, string imageName, string namingRules, string installer) :
               base(systemName, systemVersion, coreVersion, imageName, namingRules) {
            this.installer = installer;
        }
        
        override public List<string> getFields() {
            List<string> fields = base.getFields();
            return fields;
        }

        override public List<string> getProperties() {
            List<string> props = base.getProperties();
            props.Add("installer");
            return props;
        }
    }
}
