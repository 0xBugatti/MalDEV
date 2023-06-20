using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Reflection;
using System.IO.Compression;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics;

namespace PowerShellRunner

{
    internal class Program
    {
        static void Main(string[] args)
        {

            var proc = new Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //EXE
            startInfo.FileName = "cmd.exe";
            //Arguments
            startInfo.Arguments = "/c" + "curl http://127.0.0.1:2100/ ";
            proc.StartInfo = startInfo;
            proc.Start();
            

        }
    }
}
