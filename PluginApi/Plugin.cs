using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace PluginApi {
    public class Plugin {
        public string className { get; }
        public Type objectType { get; }
        private string namespaceName;
        private string pluginName;

        public Plugin(Type type, string className, string namespaceName) {
            this.objectType = type;
            this.className = className;
            this.namespaceName = namespaceName;
        }

        public Plugin(string pathToDir) {
            string descriptionFile;
            using (StreamReader sr = new StreamReader(pathToDir + "plugin.plg")) {
                descriptionFile = sr.ReadToEnd();
            }
            string[] parsedFile = descriptionFile.Split(' ');

            this.className = parsedFile[0];
            this.namespaceName = parsedFile[1];
            this.pluginName = parsedFile[2];

            Assembly asmDll = Assembly.LoadFrom(pathToDir + pluginName);
            this.objectType = asmDll.GetType(namespaceName + "." + className);
        }

        public Object CreateObject() {
            object obj = Activator.CreateInstance(objectType);
            return obj;
        }
    }
}
