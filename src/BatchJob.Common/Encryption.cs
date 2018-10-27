using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BatchJob.Common
{
    public static class Encryption
    {
        /// <summary>
        /// 用户密码加密方法
        /// </summary>
        /// <param name="password">待加密的明文</param>
        /// <returns>加密后的密文</returns>
        public static string UserPasswordEncryption(string password)
        {
            string encryptionPassword = "";

            //MD5加密
            byte[] md5Password = MD5.Create().ComputeHash(Encoding.Default.GetBytes(password));

            //对MD5加密后的密文加入一些处理
            for (int i = 0; i < md5Password.Length; i++)
                encryptionPassword += Convert.ToString(int.Parse(md5Password[i].ToString().PadLeft(4, '1')), 16);

            return encryptionPassword;
        }

        /// <summary>
        /// 用户密码加密方法
        /// </summary>
        /// <param name="password">待加密的明文</param>
        /// <returns>加密后的密文</returns>
        public static string TokenEncryption(string source)
        {
            string encryptionPassword = "";

            //MD5加密
            byte[] md5Password = MD5.Create().ComputeHash(Encoding.Default.GetBytes(source));

            //对MD5加密后的密文加入一些处理
            for (int i = 0; i < md5Password.Length; i++)
                encryptionPassword += Convert.ToString(int.Parse(md5Password[i].ToString().PadLeft(4, '2')), 16);

            return encryptionPassword;
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        public static string UTF8EncodeBase64(string source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        public static string UTF8DecodeBase64( string result)
        {
            byte[] output = Convert.FromBase64String(result);
            return Encoding.UTF8.GetString(output);
        }
    }
}
