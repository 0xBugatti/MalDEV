using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace AESShellCodeEnc
{
    internal class Program
    {




            static void Main(string[] args)
        {





byte[] buf =  {0xa5,0x30,0x23,0x75,0x09};

/*$a = Read - Host - Prompt "Enter ShellCode"
$shellcodearr = @()
$shellcodearr = $a.Split(',')
$bytes = [byte[]]$shellcodearr
$bytes[0]*/



            byte[] GetIV(int num)
            {
                var randomBytes = new byte[num]; // 32 Bytes will give us 256 bits. 
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with cryptographically secure random bytes. 
                    rngCsp.GetBytes(randomBytes);
                }
                return randomBytes;
            }

            byte[] GetKey(int size)
            {
                char[] caRandomChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*() ".ToCharArray();
                byte[] baKey = new byte[size];
                using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
                {
                    crypto.GetBytes(baKey);
                }
                return baKey;
            }










            Console.Write("Enter Encryption Mode Mode 0,1,2 :");
            //int inputmode = Convert.ToInt32(Console.ReadLine());
            /*
                        System.Console.WriteLine($"0x{word}");
                        byte[] ShellcodeBytes = new byte [20];
                        int i2 = 0;
                        foreach (var ShellcodeByte in Bytes)
                        {i2++;
                            System.Console.WriteLine($"{ShellcodeByte}");
                            ShellcodeBytes[i2] = Convert.ToByte(ShellcodeByte);}*/






            byte[] GetEncryptedShellCode(byte[] baShellCode,int mode  )
            {
                /*mode = inputmode;*/
                byte[] EncryptedShell;
                byte[] baKey = new byte[16];
                byte[] baIV = new byte[16];

                if (mode == 0)
                {
                    baIV = GetIV(16);
                    baKey = GetKey(16);
                }
                else if (mode == 1)
                {

                    for (int i = 0; i <= 15; i++) { baKey[i] = buf[i]; }
                    for (int i = 0; i <= 15; i++) { baIV[i] = buf[16 + i]; }
                }
                else if (mode == 2)
                {
                    Console.Write("Enter Key:");
                    baKey = Encoding.Unicode.GetBytes(Console.ReadLine());
                    Console.Write("Enter IV:");
                    baIV = Encoding.Unicode.GetBytes(Console.ReadLine());
                }
                using (AesManaged aes = new AesManaged())
                {
                    aes.BlockSize = 128;
                    aes.Padding = PaddingMode.PKCS7;
                    // aes Mode CBC; 
     
                    ICryptoTransform encryptor = aes.CreateEncryptor(baKey, baIV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(baShellCode);
                                cs.FlushFinalBlock();
                                EncryptedShell = ms.ToArray();
                            }
                        }
                    }

                }
                // Return Encrypted Shell     
                return EncryptedShell;
            }


            byte[] EncSh = new byte[255];
            EncSh  = GetEncryptedShellCode(buf,0);
            Console.WriteLine(EncSh.Length);



            /*  for (int C2 = 0; C2 <= EncSh.Length -1; C2++)
              {
                  Console.Write($"0x{EncSh[C2].ToString("X")},");
              }*/







/*             byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
            {
                using (var aes = Aes.Create())
                {
                    aes.KeySize = 128;
                    aes.BlockSize = 128;
                    aes.Padding = PaddingMode.Zeros;

                    aes.Key = key;
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        return PerformCryptography(data, decryptor);
                    }
                }
            }

             byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
            {
                using (var ms = new MemoryStream())
                using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();

                    return ms.ToArray();
                }
            }*/


           

        }
    }

}
    


        