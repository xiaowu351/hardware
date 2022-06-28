using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace License.Models
{
    public class RegistryHelper
    {
        //用于存储你软件信息的注册表菜单名
        public static string YourSoftName = "YourSoftName";
        /// <summary>
        /// 获取你软件下对应注册表键的值
        /// </summary>
        /// <param name="keyname">键名</param>
        /// <returns></returns>
        public static string GetRegistData(string keyname)
        {
            if (!IsYourSoftkeyExit()) return string.Empty;

            string registData;
            RegistryKey aimdir = Registry.LocalMachine.OpenSubKey("SOFTWARE\\" + YourSoftName, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
            registData = aimdir.GetValue(keyname).ToString();
            return registData;
        }

        /// <summary>
        /// 向你的软件注册表菜单下添加键值
        /// </summary>
        /// <param name="keyname">键名</param>
        /// <param name="keyvalue">值</param>
        public static void WriteRegedit(string keyname, string keyvalue)
        {
            RegistryKey software = Registry.LocalMachine.OpenSubKey("SOFTWARE", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);

            RegistryKey aimdir;
            if (!IsYourSoftkeyExit()) //不存在则创建
            {
                aimdir = software.CreateSubKey(YourSoftName);
            }
            else //存在则open
            {
                aimdir = software.OpenSubKey(YourSoftName, true);
            }
            aimdir.SetValue(keyname, keyvalue, RegistryValueKind.String);
            aimdir.Close();
        }

        /// <summary>
        /// 删除你软件注册表菜单下的键值
        /// </summary>
        /// <param name="keyname">键名</param>
        public static void DeleteRegist(string keyname)
        {
            if (!IsYourSoftkeyExit()) return;
            string[] aimnames;
            RegistryKey aimdir = Registry.LocalMachine.OpenSubKey("SOFTWARE\\" + YourSoftName, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);

            aimnames = aimdir.GetValueNames();
            foreach (string aimKey in aimnames)
            {
                if (aimKey == keyname)
                    aimdir.DeleteValue(keyname);
            }
            aimdir.Close();
        }


        /// <summary>
        /// 判断你软件注册表菜单下键是否存在
        /// </summary>
        /// <param name="keyname">键名</param>
        /// <returns></returns>
        public static bool IsRegeditExit(string keyname)
        {
            if (!IsYourSoftkeyExit()) return false;
            string[] subkeyNames;
            RegistryKey aimdir = Registry.LocalMachine.OpenSubKey("SOFTWARE\\" + YourSoftName, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);

            subkeyNames = aimdir.GetValueNames();// GetSubKeyNames();
            foreach (string kn in subkeyNames)
            {
                if (kn == keyname)
                {
                    Registry.LocalMachine.Close();
                    return true;
                }
            }
            return false;

        }
        /// <summary>
        /// 删除你软件的注册表项
        /// </summary>
        public static void DeleteYourSoftKey()
        {
            Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\" + YourSoftName);
            Registry.LocalMachine.Close();
        }
        /// <summary>
        /// 判断你软件的键是否存在
        /// </summary>
        /// <returns></returns>
        private static bool IsYourSoftkeyExit()
        {
            using (RegistryKey yourSoftkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\" + YourSoftName, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
            {
                return yourSoftkey != null;
            }
        }
    }
}
