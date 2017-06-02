using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PluginApi {
    public interface IFuncPlugin {
        void processStream(string fileNameIn, string fileNameOut);
    }
}
