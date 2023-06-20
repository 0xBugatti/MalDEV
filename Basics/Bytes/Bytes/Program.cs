using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Bytes
{
    class Program
    {
        static void Main(string[] args)
        {
            Byte B1 = 0b101;

        //Byte is unsigned  Integer Value Has 1 Byte size 
        //Integer Value in C# has 3 Types 0b1110 and 0x2F and 72 (Decimal)
        //You Can Convert all Integer Datatypes specially Byte to any Datatype You Want (Char - string float - Double - Decimal) and You Can Represent it as any thing with ToString()

        /*

        Type Conversion Methods
        1 - Convert Function
        2 - Parse Function
        3 - (Cast) Way 
                      Advanced Ex:  Console.WriteLine((int) (Byte) 0x74 );


        ToString() Function

        Var Datatype


         */




       // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-between-hexadecimal-strings-and-numeric-types


            // Process.Start("notepad.exe");
            /*            string winpath = Environment.GetEnvironmentVariable("windir");
                        string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);

                        Process.Start(winpath + @"\Microsoft.NET\Framework\v1.0.3705\Installutil.exe",
                        path + "\\MyService.exe");*/


            Byte[] bytesarr = { 0xF, 0xAD, 0xE1 };
            /// Console.WriteLine(bytesarr[1]);
            ///  Console.WriteLine(ASCIIEncoding.ASCII.GetBytes("aAFergo")[0].ToString("X"));
            Byte byte1 = 0xFF;
            
            Byte Key = 0xFF;

            int a = 500;
            int key = 1000;

            int EncryptedByte = a & key;
            int DecryptedByte =key & EncryptedByte ;

            //Convert String To bytes Method 
            //method 1:
            Convert.FromBase64String("SSDD");
            //method 2:
            Encoding.ASCII.GetBytes("SSDD");
                        //Arrays 
            int[] m = { 10, 2, 7 };
       
            //Console.ReadKey();
            System.Int32 a2 = 145;
            //a is instance from struct
            //System is Used
            Int32 a1 = 45;

            //Int instead of Int32

            unsafe
            {
                

                Console.WriteLine(a);
                Console.WriteLine(EncryptedByte);
                Console.WriteLine(DecryptedByte);

                /*                Console.WriteLine(4 * byte1);
                                Console.WriteLine( 4*byte1 & Key);*/
                Console.WriteLine();
                //int* ptr = &b;
                UIntPtr x ;
                Console.WriteLine(@"{0}\n");
            }
                string I1 = B1.ToString("X");
            char C1 = ((char)B1);
            //Know Cuurent Application Name 
            //Know Current DIrectory

            //Console.WriteLine("ASCII Representation Of Byte Has Hexalue  0x{1} which Has Decimal Value {0} is {2}", B1,I1, C1);   }
            Console.ReadKey();
        }

    }
}
