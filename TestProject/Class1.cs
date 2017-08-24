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
            //对于INIManager的示例
            INIManager ini = new INIManager("./config.cfg");
            ini["Hello"]["World"] = "233";
            Debug.WriteLine(ini["Hello"].GetValue<int>("World") - 1);
        }
    }
}
