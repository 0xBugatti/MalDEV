using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Bin2ShellCode
{
    internal class Program
    {
        static void Main(string[] args)
        {



            Byte[] ShellCode = File.ReadAllBytes(args[0]);
            for(int i=0; i <=ShellCode.Length-1; i++)
            {
                Console.Write($"0x{ShellCode[i].ToString("X")},");
                if (i % 30 == 0) { Console.Write("\n"); }
            }
        }
    }
}
