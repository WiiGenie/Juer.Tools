using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIFileManager
{
    public class INIManager
    {
        private string filePath;
        private Hashtable table;
        public Section this[string Index]
        {
            get
            {
                if (!table.ContainsKey(Index))
                    table.Add(Index, new Section(filePath, Index));
                return (Section)table[Index];
            }
            set
            {
                if (!table.ContainsKey(Index))
                    table.Add(Index, null);
                table[Index] = value;
            }
        }

        public INIManager(string filePath)
        {
            if (!File.Exists(filePath))
                File.Create(filePath);
            this.filePath = filePath;
            table = new Hashtable();
            string[] sections = API.GetSections(filePath);
            if (sections != null)
                for (int i = 0; i < sections.Length; i++)
                    table.Add(sections[i], new Section(filePath, sections[i]));
        }
        /// <summary>
        /// 删除指定节点。
        /// </summary>
        /// <param name="section"></param>
        public void Delect(string section)
        {
            if (!table.ContainsKey(section))
                throw new ArgumentException("错误的节点名称！", "section");
            API.Delect(filePath, section);
            table.Remove(section);
        }
    }
}
