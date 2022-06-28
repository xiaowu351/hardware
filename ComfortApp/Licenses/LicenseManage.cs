using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace License.Models
{
    public class LicenseManage
    {
		private const string regeditkey = "lic";

		public static LicenseStorageMode LicenseStorageMode = LicenseStorageMode.Regedit;

		public static string SavePath = "abatch";

		public static LicenseModel ApplicationLicense = null;

		private static string aeskey = "zpw;&tu@";

		public static void Init(LicenseStorageMode licStorageMode, string savePath, string encodekey)
		{
			LicenseStorageMode = licStorageMode;
			SavePath = savePath;
			if (!string.IsNullOrEmpty(encodekey))
			{
				aeskey = encodekey;
			}
			if (licStorageMode == LicenseStorageMode.Regedit)
			{
				RegistryHelper.YourSoftName = savePath;
			}
			if (licStorageMode == LicenseStorageMode.Regedit && savePath[1] != ':')
			{
				SavePath = AppDomain.CurrentDomain.BaseDirectory + savePath;
			}
		}

		public static string GetMachineCode()
		{
			return HardwareInfo.GetMachineCode();
		}

		public static string CreateLicenseString(LicenseModel lic)
		{
			if (lic == null)
			{
				return string.Empty;
			}
			byte[] licByte = SerializeHelper.SerializeToBinary(lic);
			return EncodeHelper.AES(Convert.ToBase64String(licByte), aeskey);
		}

		public static LicenseModel GetLicense()
		{
			if (ApplicationLicense != null)
			{
				return ApplicationLicense;
			}
			try
			{
				if (LicenseStorageMode == LicenseStorageMode.Regedit)
				{
					if (!RegistryHelper.IsRegeditExit("lic"))
					{
						LicenseModel licenseModel = new LicenseModel();
						licenseModel.CustomMachineCode = GetMachineCode();
						licenseModel.CustomRole = RoleType.Trial;
						licenseModel.LastUseTime = DateTime.Now;
						licenseModel.ExpireTime = DateTime.Now.AddDays(60.0);
						RegistryHelper.WriteRegedit("lic", CreateLicenseString(ApplicationLicense = licenseModel));
					}
					else
					{
						string licFromReg = RegistryHelper.GetRegistData("lic");
						try
						{
							string strlic2 = EncodeHelper.AESDecrypt(licFromReg, aeskey);
							byte[] licbyte2 = Convert.FromBase64String(strlic2);
							LicenseModel lm2 = (ApplicationLicense = SerializeHelper.DeserializeWithBinary<LicenseModel>(licbyte2));
						}
						catch (Exception)
						{
							LicenseModel licenseModel2 = new LicenseModel();
							licenseModel2.CustomMachineCode = GetMachineCode();
							licenseModel2.CustomRole = RoleType.Trial;
							licenseModel2.LastUseTime = DateTime.Now;
							licenseModel2.ExpireTime = DateTime.Now;
							LicenseModel licenseErr3 = (ApplicationLicense = licenseModel2);
						}
					}
				}
				if (LicenseStorageMode == LicenseStorageMode.File)
				{
					if (!File.Exists(SavePath))
					{
						LicenseModel licenseModel3 = new LicenseModel();
						licenseModel3.CustomMachineCode = GetMachineCode();
						licenseModel3.CustomRole = RoleType.Trial;
						licenseModel3.LastUseTime = DateTime.Now;
						licenseModel3.ExpireTime = DateTime.Now.AddDays(60.0);
						LicenseModel license = licenseModel3;
						File.WriteAllText(SavePath, CreateLicenseString(license));
						ApplicationLicense = license;
					}
					else
					{
						string licFromFile = File.ReadAllText(SavePath);
						try
						{
							string strlic = EncodeHelper.AESDecrypt(licFromFile, aeskey);
							byte[] licbyte = Convert.FromBase64String(strlic);
							LicenseModel lm = (ApplicationLicense = SerializeHelper.DeserializeWithBinary<LicenseModel>(licbyte));
						}
						catch (Exception)
						{
							LicenseModel licenseModel4 = new LicenseModel();
							licenseModel4.CustomMachineCode = GetMachineCode();
							licenseModel4.CustomRole = RoleType.Trial;
							licenseModel4.LastUseTime = DateTime.Now;
							licenseModel4.ExpireTime = DateTime.Now;
							LicenseModel licenseErr2 = (ApplicationLicense = licenseModel4);
						}
					}
				}
			}
			catch (Exception)
			{
				LicenseModel licenseModel5 = new LicenseModel();
				licenseModel5.CustomMachineCode = GetMachineCode();
				licenseModel5.CustomRole = RoleType.Trial;
				licenseModel5.LastUseTime = DateTime.Now;
				licenseModel5.ExpireTime = DateTime.Now;
				LicenseModel licenseErr = (ApplicationLicense = licenseModel5);
			}
			return ApplicationLicense;
		}

		public static bool VerifyLicense(string lic, bool bSave)
		{
			if (string.IsNullOrEmpty(lic))
			{
				return false;
			}
			try
			{
				string strlic = EncodeHelper.AESDecrypt(lic, aeskey);
				byte[] licbyte = Convert.FromBase64String(strlic);
				LicenseModel lm = SerializeHelper.DeserializeWithBinary<LicenseModel>(licbyte);
				if (VerifyLicense(lm, bSave))
				{
					ApplicationLicense = lm;
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		public static bool VerifyLicense(LicenseModel licmod, bool bSave)
		{
			bool isHaveRight = false;
			if (licmod.CustomMachineCode == GetMachineCode())
			{
				if (licmod.CustomRole == RoleType.Free)
				{
					isHaveRight = true;
				}
				else if (licmod.LastUseTime < DateTime.Now && licmod.ExpireTime > DateTime.Now)
				{
					isHaveRight = true;
				}
			}
			if (isHaveRight && bSave)
			{
				licmod.LastUseTime = DateTime.Now;
				if (LicenseStorageMode == LicenseStorageMode.Regedit)
				{
					RegistryHelper.WriteRegedit("lic", CreateLicenseString(licmod));
				}
				if (LicenseStorageMode == LicenseStorageMode.File)
				{
					File.WriteAllText(SavePath, CreateLicenseString(licmod));
				}
			}
			return isHaveRight;
		}

		public static void DeleteLicense()
		{
			RegistryHelper.DeleteRegist("lic");
			ApplicationLicense = null;
		}

		public static string RoleTypeToString()
		{
			if (ApplicationLicense == null)
			{
				return string.Empty;
			}
			if (ApplicationLicense.CustomRole == RoleType.Expiration)
			{
				return "正式版";
			}
			if (ApplicationLicense.CustomRole == RoleType.Free)
			{
				return "终身版";
			}
			return "试用版";
		}

	}
}
