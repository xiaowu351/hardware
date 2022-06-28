using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace License.Models
{
    /// <summary>
    /// 硬件码生成器  
    /// </summary>
    public class HardwareInfo
    {
		private static string myMachineCode = "";

		public static string GetMachineCode()
		{
			if (string.IsNullOrEmpty(myMachineCode))
			{
				string omsg = " CPU >> " + CpuId() + " BIOS >> " + BiosId() + " BASE >> " + BaseId();
				myMachineCode = MD5(omsg);
			}
			return myMachineCode;
		}

		private static string MD5(string scr)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] palindata = Encoding.Default.GetBytes(scr);
			byte[] encryptdata = md5.ComputeHash(palindata);
			return GetHexString(encryptdata);
		}

		private static string GetHexString(byte[] bt)
		{
			string s = string.Empty;
			for (int i = 0; i < bt.Length; i++)
			{
				byte b = bt[i];
				int j = b;
				int n1 = j & 0xF;
				int n2 = (j >> 4) & 0xF;
				s = ((n2 <= 9) ? (s + n2) : (s + (char)(n2 - 10 + 65)));
				s = ((n1 <= 9) ? (s + n1) : (s + (char)(n1 - 10 + 65)));
				if (i + 1 != bt.Length && (i + 1) % 2 == 0)
				{
					s += "-";
				}
			}
			return s;
		}

		public static string CpuId()
		{
			string retVal = identifier("Win32_Processor", "UniqueId");
			if (retVal == "")
			{
				retVal = identifier("Win32_Processor", "ProcessorId");
				if (retVal == "")
				{
					retVal = identifier("Win32_Processor", "Name");
					if (retVal == "")
					{
						retVal = identifier("Win32_Processor", "Manufacturer");
					}
					retVal += identifier("Win32_Processor", "MaxClockSpeed");
				}
			}
			return retVal;
		}

		public static string BiosId()
		{
			return identifier("Win32_BIOS", "Manufacturer") + identifier("Win32_BIOS", "SMBIOSBIOSVersion") + identifier("Win32_BIOS", "IdentificationCode") + identifier("Win32_BIOS", "SerialNumber") + identifier("Win32_BIOS", "ReleaseDate") + identifier("Win32_BIOS", "Version");
		}

		public static string DiskId()
		{
			return identifier("Win32_DiskDrive", "Model") + identifier("Win32_DiskDrive", "Manufacturer") + identifier("Win32_DiskDrive", "Signature") + identifier("Win32_DiskDrive", "TotalHeads");
		}

		public static string BaseId()
		{
			return identifier("Win32_BaseBoard", "Model") + identifier("Win32_BaseBoard", "Manufacturer") + identifier("Win32_BaseBoard", "Name") + identifier("Win32_BaseBoard", "SerialNumber");
		}

		public static string VideoId()
		{
			return identifier("Win32_VideoController", "DriverVersion") + identifier("Win32_VideoController", "Name");
		}

		public static string MacId()
		{
			return identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
		}

		private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
		{
			string result = "";
			ManagementClass mc = new ManagementClass(wmiClass);
			ManagementObjectCollection moc = mc.GetInstances();
			foreach (ManagementObject mo in moc)
			{
				if (mo[wmiMustBeTrue].ToString() == "True" && result == "")
				{
					try
					{
						result = mo[wmiProperty].ToString();
						return result;
					}
					catch
					{
					}
				}
			}
			return result;
		}

		private static string identifier(string wmiClass, string wmiProperty)
		{
			string result = "";
			ManagementClass mc = new ManagementClass(wmiClass);
			ManagementObjectCollection moc = mc.GetInstances();
			foreach (ManagementObject mo in moc)
			{
				if (result == "")
				{
					try
					{
						result = mo[wmiProperty]?.ToString();
						return result;
					}
					catch
					{
					}
				}
			}
			return result;
		}
	}
}
