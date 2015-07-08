using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;

namespace LoginControl.Utilities
{
    public class CommonUtils
    {


        #region Global Variables

        /// <summary>
        /// Global Variables
        /// </summary>
        public const string DIRECTORY_NOT_EXISTS = "Directory not exists";


        public const string EmailVerificationSubject = "Registration Not Completed- Please verify your email address";
        public const string EmailVerificationBody = "<html><body><p>	Hi [NAME],	<br/>	<br/>Congratulations, a new account has been created for you. Once you activate our account, you will be a member of the Manager called PrimeSoft Solutions Inc.<br/><br/>Your Name within PrimeSoft Solutions Inc is <b>[UserName]</b>.<br/><br/> 	<a href='[URL]'> Please activate for your new account</a> ( [URL] ).<br/>	<br/>Once you have activated your account, your Administrator will allocate you features so you get started right away.	<br/><br/>Thanks & Regards,<br />Primesoft team.</p> </body></html>";

        public const string ForgotPasswordSubject = "Account passowrd changed";
        public const string ForgotPasswordBody = "<html><body><p>Hi [NAME],Your password has been reset, <b>[UserName]</b><br/><br/>According to our records, you have requested that your password be change.<br/><br/><a href='[URL]'> Please click for change your password.</a> ( [URL] ).<br/><br/>If you have any questions or trouble logging on please contact a site administrator.<br/><br/>Thanks & Regards,<br />Primesoft team.</p></body></html>";

        public const string EmailVerificationFrom = "sthirupathuru@primesoftsolutionsinc.com";
        public const string EmailVerificationBCC = "";
        public const string SMTPUserName = "sthirupathuru@primesoftsolutionsinc.com";
        public const string SMTPPassword = "pwd";
        public const string SMTPServerName = "192.168.0.4";

        public const string EmailVerificationSucessPage = "EmailVerification.aspx";
        public const string ChangePasswordPage = "ChangePassword.aspx";


        public const string EmailLandingPage = "EmailLandingPage.aspx";


        #endregion

        #region Directory Path

        /// <summary>m
        ///  Get Directory path 
        /// </summary>
        /// <returns></returns>
        static public string GetExceptionPath()
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ExceptionPath"].ToString());
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
            catch (Exception ex)
            {
                return "C:/Exception/";
            }
        }

        #endregion


        #region Password Encrpt

        /// <summary>
        /// This method is using for Encrypt text
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// This method is using for Decrypt text
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        #endregion

        /// <summary>
        ///  This method is using for Get IP Addresses
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddresses()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).FirstOrDefault();
        }
    }
}