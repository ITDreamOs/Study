using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace lvwei8.Common
{
    /// <summary>
    /// 加密类
    /// </summary>
    public class Security
    {
        /// <summary>
        /// 密钥
        /// </summary>
        private const string ConstSecretKey = "ADFRAA4FDHTR7UR7786554TJ43K4K3J255C";

        /// <summary>
        /// 加密向量
        /// </summary>
        public const string ConstRijndaelIv = "TR'GSGFSDOGK'ROK'5459234=403=2549-8R9G%DFG8#54KSFS9[3!34";

        #region MD5加密
        /// <summary>
        /// 32位MD5加密字符串
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Md532(string s)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5");
        }

        /// <summary>
        /// 16位MD5加密字符串
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Md516(string s)
        { 
            return FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5").Substring(8, 16);
        }

        #endregion MD5加密


        #region MD5 utf-8

        /// <summary>
        /// MD5 加密字符串
        /// gbk 编码
        /// </summary>
        /// <param name="rawPass">源字符串</param>
        /// <returns>加密后字符串</returns>
        public static string MD5Encoding(string rawPass)
        {
            // 创建MD5类的默认实例：MD5CryptoServiceProvider
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.GetEncoding("gbk").GetBytes(rawPass);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                // 以十六进制格式格式化
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// MD5盐值加密
        /// </summary>
        /// <param name="rawPass">源字符串</param>
        /// <param name="salt">盐值</param>
        /// <returns>加密后字符串</returns>
        public static string MD5Encoding(string rawPass, object salt)
        {
            if (salt == null) return rawPass;
            return MD5Encoding(rawPass + "{" + salt+ "}");
        }
        #endregion


        #region SHA1加密
        /// <summary>
        ///Sha1加密字符串
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Sha1(string s)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(s, "SHA1");
        }
        #endregion SHA1加密

        #region Rijndael加密
        /// <summary>
        /// Rijndael加密(使用类默认密钥与向量)
        /// </summary>
        /// <param name="s">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public static string EnRijndael(string s)
        {
            if (s == null) s = ""; //
            return EnRijndael(s, ConstSecretKey, ConstRijndaelIv);
        }

        /// <summary>
        /// Rijndael加密
        /// </summary>
        /// <param name="s">待加密的串</param>
        /// <param name="strKey">密钥</param>
        /// <param name="strIv">向量</param>
        /// <returns>经过加密的串</returns>
        public static string EnRijndael(string s, string strKey, string strIv)
        {
            if (s == null) s = "";//

            var plaintextBuffer = Encoding.UTF8.GetBytes(s);
            var rijndael = System.Security.Cryptography.Rijndael.Create();
            rijndael.Key = GetLegalKey(strKey);
            rijndael.IV = GetLegalIv(strIv);
            var transform = rijndael.CreateEncryptor();
            var cipherTextBuffer = transform.TransformFinalBlock(plaintextBuffer, 0, plaintextBuffer.Length); 
            var result = Convert.ToBase64String(cipherTextBuffer); 
            transform.Dispose();
            return result;
        }

        /// <summary>
        /// 解密方法（使用类默认密钥和向量）
        /// </summary>
        /// <param name="s">待解密的串</param>
        /// <returns>经过解密的串</returns>
        public static string DeRijndael(string s)
        {
            return DeRijndael(s, ConstSecretKey, ConstRijndaelIv);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="s">待解密的串</param> 
        /// <param name="strKey">密钥</param>
        /// <param name="strIv">向量</param>
        /// <returns>经过解密的串</returns>
        public static string DeRijndael(string s, string strKey, string strIv)
        {
            try
            {
                var cipherTextBuffer = Convert.FromBase64String(s);
                var rijndael2 = Rijndael.Create();
                var transform2 = rijndael2.CreateDecryptor(GetLegalKey(strKey), GetLegalIv(strIv));
                var decryption = transform2.TransformFinalBlock(cipherTextBuffer, 0, cipherTextBuffer.Length);
                var result = Encoding.UTF8.GetString(decryption);
                transform2.Dispose();
                return result;
            }catch/*(Exception e)*/
            {
                return "error";
            }
        }

        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private static byte[] GetLegalKey(string strKey)
        {
            SymmetricAlgorithm symmetricAlgorithm = new RijndaelManaged();
            symmetricAlgorithm.GenerateKey();

            string sTemp = strKey;
            byte[] bytTemp = symmetricAlgorithm.Key;
            int keyLength = bytTemp.Length;
            if (sTemp.Length > keyLength)
            {
                sTemp = sTemp.Substring(0, keyLength);
            }
            else if (sTemp.Length < keyLength)
            {
                sTemp = sTemp.PadRight(keyLength, ' ');
            }
            return Encoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private static byte[] GetLegalIv(string strVi)
        {
            SymmetricAlgorithm symmetricAlgorithm = new RijndaelManaged();
            symmetricAlgorithm.GenerateIV();

            string sTemp = strVi;
            byte[] bytTemp = symmetricAlgorithm.IV;
            int ivLength = bytTemp.Length;
            if (sTemp.Length > ivLength)
                sTemp = sTemp.Substring(0, ivLength);
            else if (sTemp.Length < ivLength)
                sTemp = sTemp.PadRight(ivLength, ' ');
            return Encoding.ASCII.GetBytes(sTemp);
        }
        #endregion

        #region RSA加密（公钥与私钥是由同一RSA对象生成的）
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <param name="rsaKeyInfo">RSA产生的公钥</param>
        /// <param name="doOaepPadding">是否使用OAEPPadding</param>
        /// <returns></returns>
        public static string RSAEncrypt(string s, RSAParameters rsaKeyInfo, bool doOaepPadding)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(s);
            byteArray = RSAEncrypt(byteArray, rsaKeyInfo, doOaepPadding);
            StringBuilder rtnValue = new StringBuilder();
            foreach (byte b in byteArray)
            {
                rtnValue.AppendFormat("{0:X2}", b);
            }
            return rtnValue.ToString();
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="dataToEncrypt">要加密的字节数组</param>
        /// <param name="rsaKeyInfo">RSA产生的公钥</param>
        /// <param name="doOaepPadding">是否使用OAEPPadding</param>
        /// <returns></returns>
        public static byte[] RSAEncrypt(byte[] dataToEncrypt, RSAParameters rsaKeyInfo, bool doOaepPadding)
        {
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.ImportParameters(rsaKeyInfo);
                return RSA.Encrypt(dataToEncrypt, doOaepPadding);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="s">要解密的字符串</param>
        /// <param name="RSAKeyInfo">RSA产生的私钥</param>
        /// <param name="DoOAEPPadding">是否使用OAEPPadding</param>
        /// <returns></returns>
        public static string RSADecrypt(string s, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            int len = s.Length / 2;
            byte[] byteArray = new byte[len];
            for (int i = 0; i < len; i++)
            {
                byteArray[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
            }
            byteArray = RSADecrypt(byteArray, RSAKeyInfo, DoOAEPPadding);
            return Encoding.Unicode.GetString(byteArray);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="DataToDecrypt">要解密的字节数组</param>
        /// <param name="RSAKeyInfo">RSA产生的私钥</param>
        /// <param name="DoOAEPPadding">是否使用OAEPPadding</param>
        /// <returns></returns>
        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.ImportParameters(RSAKeyInfo);
                return RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
            }
            catch
            {
                return null;
            }

        }
        #endregion

        #region 3DES加密算法
        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="s">明文</param>
        /// <returns></returns>
        public static string DESEncrypt(string s)
        {
            return DESEncrypt(s, ConstSecretKey);
        }
        /// <summary> 
        /// 3DES加密
        /// </summary> 
        /// <param name="s">明文</param> 
        /// <param name="sKey">密钥</param> 
        /// <returns></returns> 
        public static string DESEncrypt(string s, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(s);
            des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(8, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="s">密文</param>
        /// <returns></returns>
        public static string DESDecrypt(string s)
        {
            return DESDecrypt(s, ConstSecretKey);
        }

        /// <summary> 
        /// 3DES解密 
        /// </summary> 
        /// <param name="s">密文</param> 
        /// <param name="sKey">密钥</param> 
        /// <returns></returns> 
        public static string DESDecrypt(string s, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = s.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(s.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(8, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion


    }
}