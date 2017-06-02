using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PluginApi;

namespace CommodoreOSPlugin {
    public class LinuxMint : Ubuntu, IPlugin {
        public string fileManager { get; set; }
        public LinuxMint() {
        }

        public LinuxMint(string systemName, string systemVersion, string coreVersion, string imageName, string namingRules, string fileManager) :
               base(systemName, systemVersion, coreVersion, imageName, namingRules) {
            this.fileManager = fileManager;
        }

        override public List<string> getFields() {
            List<string> fields = base.getFields();
            return fields;
        }

        override public List<string> getProperties() {
            List<string> props = base.getProperties();
            props.Add("fileManager");
            return props;
        }
    }
}
