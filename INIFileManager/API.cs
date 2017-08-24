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
        /// 获取所有节点名
        /// </summary>
        /// <param name="lpszReturnBuffer">存放节点名的缓冲内存,每个节点之间用\0分隔</param>
        /// <param name="nSize">内存大小(characters)</param>
        /// <param name="lpFileName">配置文件路径</param>
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        /// <summary>
        /// 获取指定节点中的所有键值对
        /// </summary>
        /// <param name="lpAppName">节点名</param>
        /// <param name="lpReturnedString">返回值的缓冲内存,每个键值对之间用\0分隔</param>
        /// <param name="nSize">缓冲区的长度(characters)</param>
        /// <param name="lpFileName">配置文件路径</param>
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        /// <summary>
        /// 读取指定的Key的值
        /// </summary>
        /// <param name="lpAppName">节点名</param>
        /// <param name="lpKeyName">键名</param>
        /// <param name="lpDefault">默认值</param>
        /// <param name="lpReturnedString">读取的内容</param>
        /// <param name="nSize">缓冲区的长度</param>
        /// <param name="lpFileName">配置文件路径</param>
        /// <returns>实际读取到的长度</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);
        
        /// <summary>
        /// <para>读取INI文件中指定的Key的值</para>
        /// <para>另一个方法,使用 StringBuilder 作为缓冲区</para>
        /// </summary>
        /// <param name="lpAppName">节点名</param>
        /// <param name="lpKeyName">键名</param>
        /// <param name="lpDefault">默认值</param>
        /// <param name="lpReturnedString">读取的内容</param>
        /// <param name="nSize">缓冲区的长度</param>
        /// <param name="lpFileName">配置文件路径</param>
        /// <returns>实际读取到的长度</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        /// <summary>
        /// <para>读取INI文件中指定的Key的值</para>
        /// <para>另一个方法,使用 string 作为缓冲区，实际上与使用 char[] 是差不多的，string本身就是 char[] 的封装</para>
        /// </summary>
        /// <param name="lpAppName">节点名</param>
        /// <param name="lpKeyName">键名</param>
        /// <param name="lpDefault">默认值</param>
        /// <param name="lpReturnedString">读取的内容</param>
        /// <param name="nSize">缓冲区的长度</param>
        /// <param name="lpFileName">配置文件路径</param>
        /// <returns>实际读取到的长度</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint nSize, string lpFileName);

        /// <summary>
        /// 将键值对写到指定的节点，如果有键值对已经存在，则将之替换。
        /// </summary>
        /// <param name="lpAppName">节点名，如果不存在，则创建此节点</param>
        /// <param name="lpString">键值对，多个用\0分隔，多个键值对的形式为 key1=value1\0key2=value2
        /// <para>如果为string.Empty，则清空节点中的所有键值对</para>
        /// <para>如果为null，则删除该节点（连同节点内的所有键值对）</para>
        /// </param>
        /// <param name="lpFileName">配置文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

        /// <summary>
        /// 将指定的键和值写到指定的节点，如果已经存在则替换
        /// </summary>
        /// <param name="lpAppName">节点名</param>
        /// <param name="lpKeyName">键名，如果为null，则删除指定的节点（同 WritePrivateProfileSection() 方法）</param>
        /// <param name="lpString">值，如果为null，则删除指定的键</param>
        /// <param name="lpFileName">配置文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
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
