using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

//To Run Pointers 1- UnsafeBlock
//2- Project/Netproperties/Allow unsafe code
namespace ConsoleApp1
{

    internal class Program
    {
        static void Main(string[] args)




        {
            MessageBox a = new MessageBox("Hello, World!");

            int[] cars = new int[400];
            Console.WindowHeight = 20;
            Console.WindowWidth = 50;
            Console.WriteLine("Hi");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Mohamed");
            System.Threading.Thread.Sleep(1000);

            UInt64 x = 0b10101;
            uint ex = 0x1E1;
            Console.WriteLine("DEC Of Number 0x1E1 : " + (Int64)ex);
            Console.WriteLine("DEC Of Number 0b10101 : " + (Int64)x);

            unsafe
            {

                int number = 3;
                int* ptr = &number;
                Console.WriteLine("Address Of Number : " + (IntPtr)ptr);
                Console.WriteLine("Address Of Number : " + (Int64)ptr);
            }






        }





    }
}
