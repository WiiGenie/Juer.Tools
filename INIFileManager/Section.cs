using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIFileManager
{
    public class Section
    {
        private string
            filePath,
            section;
        /// <summary>
        /// 键所对应的值
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public string this[string Index]
        {
            get
            {
                return API.GetValue(filePath, section, Index);
            }
            set
            {
                API.SetValue(filePath, section, Index, value);
            }
        }
        /// <summary>
        /// 从文件中构造一个Section
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="section"></param>
        public Section(string filePath, string section)
        {
            this.filePath = filePath;
            this.section = section;
        }
        /// <summary>
        /// 以指定的类型取出该值。
        /// </summary>
        /// <typeparam name="T">
        /// <para>欲取到的值类型。</para>
        /// <para>支持基本的数据类型。</para>
        /// </typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValue<T>(string key)
        {
            string result = this[key];
            return (T)Convert.ChangeType(result, typeof(T));
        }
        /// <summary>
        /// 删除指定键值，若键参数为空，则清空该节点。
        /// </summary>
        /// <param name="key"></param>
        public void Delect(string key = null)
        {
            API.Delect(filePath, section, key);
        }
    }
}
