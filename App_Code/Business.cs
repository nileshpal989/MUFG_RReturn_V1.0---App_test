using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;


public class TF_PageControls
{
    public TF_PageControls()
    {
    }
    public void enablefirstnav(Button Btn_Previous, Button Btn_First)
    {
        Btn_Previous.Enabled = true;
        Btn_Previous.CssClass = "pagingButton";

        Btn_First.Enabled = true;
        Btn_First.CssClass = "pagingButton";
    }
    public void disablefirstnav(Button Btn_Previous, Button Btn_First)
    {
        Btn_Previous.Enabled = false;
        Btn_Previous.CssClass = "pagingButtonDisabled";

        Btn_First.Enabled = false;
        Btn_First.CssClass = "pagingButtonDisabled";
    }
    public void enablelastnav(Button Btn_Next, Button Btn_Last)
    {
        Btn_Next.Enabled = true;
        Btn_Next.CssClass = "pagingButton";

        Btn_Last.Enabled = true;
        Btn_Last.CssClass = "pagingButton";
    }
    public void disablelastnav(Button Btn_Next, Button Btn_Last)
    {
        Btn_Next.Enabled = false;
        Btn_Next.CssClass = "pagingButtonDisabled";

        Btn_Last.Enabled = false;
        Btn_Last.CssClass = "pagingButtonDisabled";
    }
}
public class Encryption//This class is used to encrypt/decrypt the string provided to it. It uses SHA1 algorithm, however MD5 can also be used with the same code.
{
    string passPhrase = "ShrIY@Is";
    string saltValue = "sHriY@2009";
    string hashAlgorithm = "SHA1";
    int passwordIterations = 2;
    string initVector = "@1B2c3D4e5F6g7H8";
    int keySize = 256;
    public string encryptplaintext(string textvalue)//function used to encrypt plain text passed to it.
    {
        string plainText = textvalue;
        string cipherText = Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        return cipherText;
    }
    public string decrypttext(string ciphervalue)//function used to decrypt cipher text passed to it.
    {
        string cipherText = ciphervalue;
        string plainText = Decrypt(cipherText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        return plainText;
    }
    //public static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    //{
    //    byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);

    //    byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

    //    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

    //    PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

    //    byte[] keyBytes = password.GetBytes(keySize / 8);

    //    RijndaelManaged symmetricKey = new RijndaelManaged();

    //    symmetricKey.Mode = CipherMode.CBC;

    //    ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

    //    MemoryStream memoryStream = new MemoryStream();

    //    CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

    //    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

    //    cryptoStream.FlushFinalBlock();

    //    byte[] cipherTextBytes = memoryStream.ToArray();

    //    memoryStream.Close();

    //    cryptoStream.Close();

    //    string cipherText = Convert.ToBase64String(cipherTextBytes);

    //    return cipherText;
    //}
    public static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        // ✅ Replace PasswordDeriveBytes with Rfc2898DeriveBytes
        var password = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        // ✅ Replace RijndaelManaged with AES
        using (Aes aes = Aes.Create())
        {
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // ⚠️ If IV length wrong ho to fallback to safe IV
            if (initVectorBytes.Length != 16)
            {
                aes.GenerateIV();
                initVectorBytes = aes.IV;
            }

            using (ICryptoTransform encryptor = aes.CreateEncryptor(keyBytes, initVectorBytes))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // 👉 Optional: IV prepend karna (recommended)
                memoryStream.Write(initVectorBytes, 0, initVectorBytes.Length);

                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }
                byte[] cipherTextBytes = memoryStream.ToArray();
                return Convert.ToBase64String(cipherTextBytes);
            }
        }
    }
    //public static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    //{
    //    byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);

    //    byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

    //    byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

    //    PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

    //    byte[] keyBytes = password.GetBytes(keySize / 8);

    //    RijndaelManaged symmetricKey = new RijndaelManaged();

    //    symmetricKey.Mode = CipherMode.CBC;

    //    ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

    //    MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

    //    CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

    //    byte[] plainTextBytes = new byte[cipherTextBytes.Length];

    //    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

    //    memoryStream.Close();


    //    cryptoStream.Close();

    //    string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

    //    return plainText;
    //}
    public static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
        try
        {
            return DecryptNew(cipherText, passPhrase, saltValue, passwordIterations, keySize);
        }
        catch (Exception ex)
        {
            return ex.Message;
            //return DecryptOld(cipherText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        }
    }
    private static string DecryptNew(string cipherText, string passPhrase, string saltValue, int passwordIterations, int keySize)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        byte[] saltBytes = Encoding.ASCII.GetBytes(saltValue);

        var key = new Rfc2898DeriveBytes(passPhrase, saltBytes, passwordIterations);
        byte[] keyBytes = key.GetBytes(keySize / 8);

        byte[] iv = new byte[16];
        Array.Copy(cipherBytes, 0, iv, 0, 16);

        byte[] actualCipher = new byte[cipherBytes.Length - 16];
        Array.Copy(cipherBytes, 16, actualCipher, 0, actualCipher.Length);

        using (Aes aes = Aes.Create())
        {
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var decryptor = aes.CreateDecryptor(keyBytes, iv))
            using (var ms = new MemoryStream(actualCipher))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            {
                byte[] plainBytes = new byte[actualCipher.Length];
                int count = cs.Read(plainBytes, 0, plainBytes.Length);

                return Encoding.UTF8.GetString(plainBytes, 0, count);
            }
        }
    }
    //private static string DecryptOld(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    //{
    //    byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
    //    byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
    //    byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

    //    PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
    //    byte[] keyBytes = password.GetBytes(keySize / 8);

    //    using (RijndaelManaged symmetricKey = new RijndaelManaged())
    //    {
    //        symmetricKey.Mode = CipherMode.CBC;

    //        using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
    //        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
    //        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
    //        {
    //            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
    //            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

    //            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    //        }
    //    }
    //}
}
