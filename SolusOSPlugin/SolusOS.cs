using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PluginApi;

namespace SolusOSPlugin {
    public class SolusOS : LinuxMintDebianEdition, IPlugin {
        public string installer { get; set; }
        public SolusOS() {
        }

        public SolusOS(string systemName, string systemVersion, string coreVersion, string fileManager, string installer) :
               base(systemName, systemVersion, coreVersion, fileManager) {
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
