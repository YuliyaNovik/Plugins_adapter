using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PluginApi {
    public class Api {
        private static Api instance;
        
        public List<Plugin> getPluginList() {
            List<Plugin> plugins = LoadPlugins("class");
            
            plugins.Add(new Plugin(typeof(BaseType), "Debian", "PluginApi"));
            return plugins;
        }

        public void processFunctionalPlugins(string fileNameIn, string fileNameOut) {
            List<Plugin> functionalPlugins = LoadPlugins("func");

            foreach (var item in functionalPlugins) {
                IFuncPlugin plugin = (IFuncPlugin) item.CreateObject();
                plugin.processStream(fileNameIn, fileNameOut);
            }
        }

        public List<Plugin> LoadPlugins(string type) {
            if (!File.Exists("plugin/info.plg")) {
                Directory.CreateDirectory("plugin/");
                FileStream sw = File.Create("plugin/info.plg");
                sw.Close();
            }

            List<Plugin> pluginList = new List<Plugin>();
            string[] plugins_path = File.ReadAllText("plugin/info.plg").Split(new char[] { '\n', '\r' });

            foreach (string plugin_path in plugins_path) {
                string[] plugin_path_arr = plugin_path.Split(' ');

                if (plugin_path_arr[0] != "" && plugin_path_arr[1] == type) {
                    pluginList.Add(new Plugin(plugin_path_arr[0]));
                }
            }
            return pluginList;
        }

        public bool InstallPlugin(string pathToPlgFile) {
            string descriptionFile;
            using (StreamReader sr = new StreamReader(pathToPlgFile)) {
                descriptionFile = sr.ReadToEnd();
            }
            string[] parsedFile = descriptionFile.Split(' ');

            string className = parsedFile[0];
            string pluginName = parsedFile[2];
            string type = parsedFile[3];

            Directory.CreateDirectory("plugin/" + className);
            File.Copy(pathToPlgFile, "plugin/" + className + "/plugin.plg");
            File.Copy(Path.GetDirectoryName(pathToPlgFile) + "/" + pluginName, "plugin/" + className + "/" + pluginName);

            for (int i = 4; i < parsedFile.Length; i++) {
                File.Copy(Path.GetDirectoryName(pathToPlgFile) + "/" + parsedFile[i], "plugin/" + className + "/" + parsedFile[i]);
            }

            using (StreamWriter sw = new StreamWriter("plugin/info.plg", true)) {
                sw.WriteLine("plugin/" + className + "/" + " " + type);
            }
            return true;
        }

        public static Api GetInstance() {
            if (instance == null) { instance = new Api(); }
            return instance;
        }
    }
}
