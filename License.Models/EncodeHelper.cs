using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace License.Models
{
	public class EncodeHelper
	{
		private const string DesIV_64 = "xiao1><@";

		private const string AesIV_128 = "小y设计.tuy";

		public static string MD5(string scr)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] palindata = Encoding.Default.GetBytes(scr);
			byte[] encryptdata = md5.ComputeHash(palindata);
			return Convert.ToBase64String(encryptdata);
		}

		public static string SHA1(string scr)
		{
			SHA1 sha1 = new SHA1CryptoServiceProvider();
			byte[] palindata = Encoding.Default.GetBytes(scr);
			byte[] encryptdata = sha1.ComputeHash(palindata);
			return Convert.ToBase64String(encryptdata);
		}

		public static string RSA(string scr)
		{
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = "tuyile006.cnblogs";
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
			{
				byte[] plaindata = Encoding.Default.GetBytes(scr);
				byte[] encryptdata = rsa.Encrypt(plaindata, fOAEP: false);
				return Convert.ToBase64String(encryptdata);
			}
		}

		public static string RSADecrypt(string scr)
		{
			try
			{
				CspParameters csp = new CspParameters();
				csp.KeyContainerName = "tuyile006.cnblogs";
				using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
				{
					byte[] bytes = Convert.FromBase64String(scr);
					byte[] DecryptBytes = rsa.Decrypt(bytes, fOAEP: false);
					return Encoding.Default.GetString(DecryptBytes);
				}
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		public static string GetRSAPublicKey()
		{
			CspParameters csp = new CspParameters();
			csp.KeyContainerName = "tuyile006.cnblogs";
			using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
			{
				return rsa.ToXmlString(includePrivateParameters: false);
			}
		}

		public static string DES(string strContent, string strKey)
		{
			if (string.IsNullOrEmpty(strContent))
			{
				return string.Empty;
			}
			if (strKey.Length > 8)
			{
				strKey = strKey.Substring(0, 8);
			}
			DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
			byte[] byKey = Encoding.ASCII.GetBytes(strKey);
			byte[] byIV = Encoding.ASCII.GetBytes("xiao1><@");
			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write))
				{
					using (StreamWriter sw = new StreamWriter(cst))
					{
						sw.Write(strContent);
						sw.Flush();
						cst.FlushFinalBlock();
						sw.Flush();
						return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
					}
				}
			}
		}

		public static string DESDecrypt(string strContent, string strKey)
		{
			if (string.IsNullOrEmpty(strContent))
			{
				return string.Empty;
			}
			if (strKey.Length > 8)
			{
				strKey = strKey.Substring(0, 8);
			}
			byte[] byKey = Encoding.ASCII.GetBytes(strKey);
			byte[] byIV = Encoding.ASCII.GetBytes("xiao1><@");
			try
			{
				byte[] byEnc = Convert.FromBase64String(strContent);
				using (DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider())
				{
					using (MemoryStream ms = new MemoryStream(byEnc))
					{
						using (CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read))
						{
							StreamReader sr = new StreamReader(cst);
							return sr.ReadToEnd();
						}
					}
				}
			}
			catch
			{
				return string.Empty;
			}
		}

		public static string AES(string strContent, string strKey)
		{
			if (string.IsNullOrEmpty(strContent))
			{
				return string.Empty;
			}
			if (strKey.Length > 8)
			{
				strKey = strKey.Substring(0, 8);
			}
			using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
			{
				aesAlg.Key = Encoding.Unicode.GetBytes(strKey);
				aesAlg.IV = Encoding.Unicode.GetBytes("小y设计.tuy");
				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(strContent);
						}
						return Convert.ToBase64String(msEncrypt.ToArray());
					}
				}
			}
		}

		public static string AESDecrypt(string strContent, string strKey)
		{
			if (string.IsNullOrEmpty(strContent))
			{
				return string.Empty;
			}
			if (strKey.Length > 8)
			{
				strKey = strKey.Substring(0, 8);
			}
			try
			{
				byte[] byEnc = Convert.FromBase64String(strContent);
				using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
				{
					aesAlg.Key = Encoding.Unicode.GetBytes(strKey);
					aesAlg.IV = Encoding.Unicode.GetBytes("小y设计.tuy");
					ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
					using (MemoryStream msDecrypt = new MemoryStream(byEnc))
					{
						using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
						{
							using (StreamReader srDecrypt = new StreamReader(csDecrypt))
							{
								return srDecrypt.ReadToEnd();
							}
						}
					}
				}
			}
			catch
			{
				return string.Empty;
			}
		}

		public static string ECC_EncodeKey(string AKeyName, string BKey)
		{
			byte[] BKeybyte = Convert.FromBase64String(BKey);
			using (ECDiffieHellmanCng AClient = new ECDiffieHellmanCng(CngKey.Open(AKeyName)))
			{
				AClient.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
				AClient.HashAlgorithm = CngAlgorithm.Sha256;
				byte[] MsgKey = AClient.DeriveKeyMaterial(CngKey.Import(BKeybyte, CngKeyBlobFormat.EccPublicBlob));
				return Convert.ToBase64String(MsgKey);
			}
		}

		public static string ECC_GetMyPublicKey(string keyName)
		{
			if (!CngKey.Exists(keyName))
			{
				using (ECDiffieHellmanCng eCDiffieHellmanCng = new ECDiffieHellmanCng(CngKey.Create(CngAlgorithm.ECDiffieHellmanP256, keyName)))
				{
					eCDiffieHellmanCng.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
					eCDiffieHellmanCng.HashAlgorithm = CngAlgorithm.Sha256;
					byte[] Keybyte2 = eCDiffieHellmanCng.PublicKey.ToByteArray();
					return Convert.ToBase64String(Keybyte2);
				}
			}
			using (ECDiffieHellmanCng MyECC = new ECDiffieHellmanCng(CngKey.Open(keyName)))
			{
				byte[] Keybyte = MyECC.PublicKey.ToByteArray();
				return Convert.ToBase64String(Keybyte);
			}
		}
	}
}
