using INIFileManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            INIManager ini = new INIManager("./config.cfg");
            ini["Hello"]["World"] = "!";
            Debug.WriteLine(ini["Hello"]["World"]);
        }
    }
}
