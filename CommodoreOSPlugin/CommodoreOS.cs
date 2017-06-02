using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PluginApi;


namespace CommodoreOSPlugin {
    public class CommodoreOS : LinuxMint, IPlugin {
        public string specialDesign { get; set; }
        public CommodoreOS() {
        }

        public CommodoreOS(string systemName, string systemVersion, string coreVersion, string imageName, string namingRules, string fileManager, string specialDesign) :
               base(systemName, systemVersion, coreVersion, imageName, namingRules, fileManager) {
            this.specialDesign = specialDesign;
        }

        override public List<string> getFields() {
            List<string> fields = base.getFields();
            return fields;
        }

        override public List<string> getProperties() {
            List<string> props = base.getProperties();
            props.Add("specialDesign");
            return props;
        }
    }
}
