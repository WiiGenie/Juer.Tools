using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace INIFileManager
{
    /// <summary>
    /// <para>Import Win32API</para>
    /// <para>声明所需的Win32API</para>
    /// </summary>
    public static class API
    {
        #region API声明

        /// <summary>
        /// 获取所有节点名称(Section)
        /// </summary>
        /// <param name="lpszReturnBuffer">存放节点名称的内存地址,每个节点之间用\0分隔</param>
        /// <param name="nSize">内存大小(characters)</param>
        /// <param name="lpFileName">Ini文件地址</param>
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        /// <summary>
        /// 获取某个指定节点(Section)中所有KEY和Value
        /// </summary>
        /// <param name="lpAppName">节点名称</param>
        /// <param name="lpReturnedString">返回值的内存地址,每个之间用\0分隔</param>
        /// <param name="nSize">内存大小(characters)</param>
        /// <param name="lpFileName">Ini文件地址</param>
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        /// <summary>
        /// 读取INI文件中指定的Key的值
        /// </summary>
        /// <param name="lpAppName">节点名称。如果为null,则读取INI中所有节点名称,每个节点名称之间用\0分隔</param>
        /// <param name="lpKeyName">Key名称。如果为null,则读取INI中指定节点中的所有KEY,每个KEY之间用\0分隔</param>
        /// <param name="lpDefault">读取失败时的默认值</param>
        /// <param name="lpReturnedString">读取的内容缓冲区，读取之后，多余的地方使用\0填充</param>
        /// <param name="nSize">内容缓冲区的长度</param>
        /// <param name="lpFileName">INI文件地址</param>
        /// <returns>实际读取到的长度</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);
        
        /// <summary>
        /// 读取INI文件中指定的Key的值
        /// 另一种声明方式,使用 StringBuilder 作为缓冲区类型的缺点是不能接受\0字符，会将\0及其后的字符截断,
        /// 所以对于lpAppName或lpKeyName为null的情况就不适用
        /// </summary>
        /// <param name="lpAppName">节点名称。如果为null,则读取INI中所有节点名称,每个节点名称之间用\0分隔</param>
        /// <param name="lpKeyName">Key名称。如果为null,则读取INI中指定节点中的所有KEY,每个KEY之间用\0分隔</param>
        /// <param name="lpDefault">读取失败时的默认值</param>
        /// <param name="lpReturnedString">读取的内容缓冲区，读取之后，多余的地方使用\0填充</param>
        /// <param name="nSize">内容缓冲区的长度</param>
        /// <param name="lpFileName">INI文件地址</param>
        /// <returns>实际读取到的长度</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);
        
        /// <summary>
        /// 读取INI文件中指定的Key的值
        /// 再一种声明，使用string作为缓冲区的类型同char[]
        /// </summary>
        /// <param name="lpAppName">节点名称。如果为null,则读取INI中所有节点名称,每个节点名称之间用\0分隔</param>
        /// <param name="lpKeyName">Key名称。如果为null,则读取INI中指定节点中的所有KEY,每个KEY之间用\0分隔</param>
        /// <param name="lpDefault">读取失败时的默认值</param>
        /// <param name="lpReturnedString">读取的内容缓冲区，读取之后，多余的地方使用\0填充</param>
        /// <param name="nSize">内容缓冲区的长度</param>
        /// <param name="lpFileName">INI文件地址</param>
        /// <returns>实际读取到的长度</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint nSize, string lpFileName);

        /// <summary>
        /// 将指定的键值对写到指定的节点，如果已经存在则替换。
        /// </summary>
        /// <param name="lpAppName">节点，如果不存在此节点，则创建此节点</param>
        /// <param name="lpString">Item键值对，多个用\0分隔,形如key1=value1\0key2=value2
        /// <para>如果为string.Empty，则删除指定节点下的所有内容，保留节点</para>
        /// <para>如果为null，则删除指定节点下的所有内容，并且删除该节点</para>
        /// </param>
        /// <param name="lpFileName">INI文件</param>
        /// <returns>是否成功写入</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]     //可以没有此行
        public static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

        /// <summary>
        /// 将指定的键和值写到指定的节点，如果已经存在则替换
        /// </summary>
        /// <param name="lpAppName">节点名称</param>
        /// <param name="lpKeyName">键名称。如果为null，则删除指定的节点及其所有的项目</param>
        /// <param name="lpString">值内容。如果为null，则删除指定节点中指定的键。</param>
        /// <param name="lpFileName">INI文件</param>
        /// <returns>操作是否成功</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        #endregion

        #region API封装
        /// <summary>
        /// 取文件中的所有配置节点，如果取出失败则返回null。
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string[] GetSections(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("不接受空的参数！请传入一个有效的文件名。", filePath);

            string[] result = null;
            IntPtr memory = Marshal.AllocCoTaskMem(1024);
            uint length = GetPrivateProfileSectionNames(memory, 1024, filePath);
            if (length != 1022 && length != 0)
                result = Marshal.PtrToStringAuto(memory, (int)length).Split(new char[] { '\0',});
            return result;
        }
        /// <summary>
        /// 取文件中指定节点的所有键值对，键值对形式为 key=value。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public static string[] GetItemAll(string filePath, string section)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("不接受空的参数！请传入一个有效的文件名。", filePath);
            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException("不接受空的参数！请传入一个有效的节点名。", section);

            string[] result = null;
            IntPtr memory = Marshal.AllocCoTaskMem(1024);
            uint length = GetPrivateProfileSection(section, memory, 1024, filePath);
            if (length != 1022 && length != 0)
                result = Marshal.PtrToStringAuto(memory, (int)length).Split(new char[] { '\0', });
            return result;
        }
        /// <summary>
        /// 取指定键所对应的值。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string filePath, string section, string key)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("不接受空的参数！请传入一个有效的文件名。", filePath);
            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException("不接受空的参数！请传入一个有效的节点名。", section);
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("不接受空的参数！请传入一个有效的键名。", key);

            char[] result = new char[1024];
            uint length = GetPrivateProfileString(section, key, null, result, 1024, filePath);

            return new string(result);
        }
        /// <summary>
        /// 将若干键值对写入到指定节点中，若已存在则覆盖。多个键值对之间以 \0 分隔。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="section"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetItems(string filePath, string section, string value)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("不接受空的参数！请传入一个有效的文件名。", filePath);
            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException("不接受空的参数！请传入一个有效的节点名。", section);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("不接受空的参数！请传入一个有效的字符串。", value);

            return WritePrivateProfileSection(section, value, filePath);
        }
        /// <summary>
        /// 将若干键值对写入到指定节点中，若已存在则覆盖。键值对形式为 key=value。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="section"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetItems(string filePath, string section, string[] value)
        {
            return SetItems(section, string.Join("\0", value), filePath);
        }
        /// <summary>
        /// 将值写入指定键中。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetValue(string filePath, string section, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("不接受空的参数！请传入一个有效的文件名。", filePath);
            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException("不接受空的参数！请传入一个有效的节点名。", section);
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("不接受空的参数！请传入一个有效的键名。", key);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("不接受空的参数！请传入一个有效的字符串。", value);

            return WritePrivateProfileString(section, key, value, filePath);
        }
        /// <summary>
        /// 删除指定键值对或者清空指定节点，依参数而定。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="section"></param>
        /// <param name="key">若该值为空，则清空指定节点</param>
        /// <returns></returns>
        public static bool Delect(string filePath, string section, string key = null)
        {
            return WritePrivateProfileString(section, key, null, filePath);
        }
        #endregion
    }
}
