using System.Security.Cryptography;
using System.Text;
using System.Web;


public class Stringcl
{
    public static string Decrypt(string sPasswordToken)
    {
        byte[] bytes = ASCIIEncoding.ASCII.GetBytes("AossCool");
        try
        {
            if (!String.IsNullOrEmpty(sPasswordToken))
            {
                sPasswordToken = HttpUtility.HtmlDecode(sPasswordToken.Replace(" ", "+").Replace("\"", ""));  // + sign is converted to space in Query String. Replace space by + sign to get original encrypted string.
                DESCryptoServiceProvider oDESCryptoServiceProvider = new DESCryptoServiceProvider();
                MemoryStream oMemoryStream = new MemoryStream(Convert.FromBase64String(sPasswordToken));
                CryptoStream oCryptoStream = new CryptoStream(oMemoryStream, oDESCryptoServiceProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
                StreamReader oStreamReader = new StreamReader(oCryptoStream);
                sPasswordToken = oStreamReader.ReadToEnd();
            }
        }
        catch (Exception ex)
        {
            //Errorcl.HandleException(ex, false);
        }
        return sPasswordToken;
    }
    /*! \brief     Function to encrypt password
     *  \details   Function to encrypt password
     *  \param[in] sPasswordToken Password Token
     */
    public static string Encrypt(string sPasswordToken)
    {
        byte[] bytes = ASCIIEncoding.ASCII.GetBytes("AossCool");
        try
        {
            if (!String.IsNullOrEmpty(sPasswordToken))
            {
                DESCryptoServiceProvider oDESCryptoServiceProvider = new DESCryptoServiceProvider();
                MemoryStream oMemoryStream = new MemoryStream();
                CryptoStream oCryptoStream = new CryptoStream(oMemoryStream, oDESCryptoServiceProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
                StreamWriter oStreamWriter = new StreamWriter(oCryptoStream);
                oStreamWriter.Write(sPasswordToken);
                oStreamWriter.Flush();
                oCryptoStream.FlushFinalBlock();
                oStreamWriter.Flush();
                sPasswordToken = Convert.ToBase64String(oMemoryStream.GetBuffer(), 0, (int)oMemoryStream.Length);
            }
        }
        catch (Exception ex)
        {
            //Errorcl.HandleException(ex, true);
        }
        return sPasswordToken;
    }

    public static string CreateRandomCode(int codeCount)
    {
        string allChar = "0,1,2,3,4,5,6,7,8,9";
        string[] allCharArray = allChar.Split(',');
        string randomCode = "";
        int temp = -1;

        Random rand = new Random();
        for (int i = 0; i < codeCount; i++)
        {
            if (temp != -1)
            {
                rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
            }
            int t = rand.Next(10);
            if (temp != -1 && temp == t)
            {
                return CreateRandomCode(codeCount);
            }
            temp = t;
            randomCode += allCharArray[t];

        }


        return randomCode;

    }
    public static string EncryptPwd(string clearText)
    {
        string EncryptionKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
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
    public static string DecryptPwd(string clearText)
    {
        string EncryptionKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
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
}

