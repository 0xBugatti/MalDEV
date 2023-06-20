using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
          static Int64 maina()
        {



            return 12;
        }
        static void Main(string[] args)
        {




            //Reflection
          

            int e = 47;
            Type t0 = e.GetType();
            Console.WriteLine(t0);
            Type t2 = Type.GetType("System.Int64");
/*            var dll = Assembly.LoadFrom(@"C:\Users\Blu-Ray\source\testing\DotNetDLL\DotNetDLL\bin\Debug\DotNetDLL.dll");
            Type t1 = dll.GetType();*/
            Type t3 = typeof(UInt16);
            Console.WriteLine(t2);

            Console.WriteLine($"DllName is :{t2.Assembly}");
            Console.WriteLine($"Dll is GAC :{t2.Assembly.GlobalAssemblyCache}");
            Console.WriteLine($"Name Space is :{t2.Namespace}");
            Console.WriteLine($"ClassName is :{t2.Name}");
            Console.WriteLine($"FullName is :{t2.FullName}");

            MethodInfo[] methods = t2.GetMethods();
            Console.WriteLine($"Number Of Methods is {methods.Length}");
            foreach (var method in methods) { Console.WriteLine($"Found method: {method}"); }

            
            PropertyInfo[] Properties = t2.GetProperties();
            Console.WriteLine($"Number Of Properties is {Properties.Length}");
            foreach (var property in Properties) { Console.WriteLine($"Found method: {property}"); }

            





            object classInstance = Activator.CreateInstance(typeof(int));/*
            int aas = int.Parse("12");




            //Type[] parameterType = {typeof(string) };


            object magicValue = methods[11].Invoke(classInstance, new object[] { "100" });
            */
            
            
            















            

        }
    }
}
