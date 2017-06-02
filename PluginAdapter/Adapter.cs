using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginApi;
using System.IO;
using ArchivingPlugin;
using IArchivingPlugin;

namespace PluginAdapter {
    public class Adapter : IFuncPlugin {
        public void processStream(string fileNameIn, string fileNameOut) {
            File.WriteAllText(GzipPlugin.FILE_NAME, File.ReadAllText(fileNameIn));

            IArchivingPlugin.IPluginMain zip = new ArchivingPlugin.GzipPlugin();
            IArchivingPlugin.IPluginMain dfl = new ArchivingPlugin.DeflatePlugin();

            zip.Compression();
            dfl.Compression();
        }
    }
}
