using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PluginApi;

namespace SolusOSPlugin {
    public class LinuxMintDebianEdition : BaseType, IPlugin {
        public string fileManager { get; set; }
        public LinuxMintDebianEdition() { }

        public LinuxMintDebianEdition(string systemName, string systemVersion, string coreVersion, string fileManager) :
               base(systemName, systemVersion, coreVersion) {
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
