using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Data;
using System.Reflection.Emit;
using System.Net.Http;
using System.IO;
using System.Security.Cryptography;


namespace RefLectiveTest
{    class Program
    {
       static void Main(string[] args)
        {    Type UNSAFEWin32Type = typeof(Int16);
            //Search For This File amd Assembly sysdll = Assembly.LoadFrom(@" C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System\v4.0_4.0.0.0__b77a5c561934e089\System.dll");
            Assembly sysdll = Assembly.LoadWithPartialName("System");
            Console.WriteLine("Search For Target Classes");
            Console.Beep();
            
            Type[] ts = sysdll.GetTypes();
            foreach (var type in ts)
            {
                if (type.FullName.Equals("Microsoft.Win32.UnsafeNativeMethods"))
                {
                    UNSAFEWin32Type = type;
                    Console.WriteLine("Found Traget Class: " +type.FullName);

                }

            }
            MethodInfo[] methods = UNSAFEWin32Type.GetMethods();
            int i = 0;
            Console.WriteLine("Search For Target Functions");
            Console.Beep();
            MethodInfo GetModuleHAND = typeof(int).GetMethod("trash");
            MethodInfo GetProcADDR =typeof(int).GetMethod("trash");
            foreach (var m in methods)
            {   if (m.Name.Equals("GetModuleHandle"))
                {GetModuleHAND = m;
                 Console.WriteLine($"Found Target Function : {i} {GetModuleHAND.Name} ");
                    
                }

                if (m.Name.Equals("GetProcAddress"))
                { GetProcADDR = m;
                    Console.WriteLine($"Found Target Function : {i} {GetProcADDR.Name} ");
                    break;
                }
                i++;
            }


            ///var MyAssembly = AppDomain.CurrentDomain.GetType().Assembly.GetName();
            ///var MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(MyAssembly, System.Reflection.Emit.AssemblyBuilderAccess.Run);

            object ExecFunctionWithReflection6(string DLLName, string FucntionName, object p1, object p2, object p3, object p4, object p5, object p6, object returnDT)
            {
                IntPtr DynamicLookUPFunc()
                {
                    object retptr = GetProcADDR.Invoke(null, new object[] { GetModuleHAND.Invoke(null, new object[] { DLLName }), FucntionName });
                    IntPtr ADDR = (IntPtr)retptr;
                    Console.WriteLine($"Hooking {FucntionName} From {DLLName}........");
                    Console.Beep();
                    Console.WriteLine($"Address of {FucntionName} is: 0x" + ADDR.ToString("X"));
                    return ADDR;
                }

                Assembly myAsm;
                AssemblyName assembly;
                assembly = new AssemblyName();
                assembly.Name = "ReflectionEmitDelegateTest";
                var MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assembly, System.Reflection.Emit.AssemblyBuilderAccess.Run);
                var MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InMemoryModule", false);
                var MyTypeBuilder = MyModuleBuilder.DefineType("BugattiDelegate", TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.AnsiClass | TypeAttributes.AutoClass, typeof(System.MulticastDelegate));
                ConstructorBuilder constructorBuilder = MyTypeBuilder.DefineConstructor(MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public, CallingConventions.Standard, new Type[] { p1.GetType(), p2.GetType(), p3.GetType(), p4.GetType(), p5.GetType(), p6.GetType() });
                constructorBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
                var MyMethodBuilder = MyTypeBuilder.DefineMethod("Invoke", MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Public, returnDT.GetType(), new Type[] { p1.GetType(), p2.GetType(), p3.GetType(), p4.GetType(), p5.GetType(), p6.GetType() });
                MyMethodBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
                Type tex = MyTypeBuilder.CreateType();
                var func = Marshal.GetDelegateForFunctionPointer(DynamicLookUPFunc(), tex);
                var FuncRet = func.DynamicInvoke(new object[] { p1, p2, p3, p4, p5, p6 });
                Console.WriteLine($"Invoking  {FucntionName} With Address {FuncRet}...........");

                return FuncRet;
            }



            object ExecFunctionWithReflection2(string DLLName, string FucntionName, object p1, object p2, object returnDT)
            {
                IntPtr DynamicLookUPFunc()
                {
                    object retptr = GetProcADDR.Invoke(null, new object[] { GetModuleHAND.Invoke(null, new object[] { DLLName }), FucntionName });
                    IntPtr ADDR = (IntPtr)retptr;
                    Console.WriteLine($"Hooking {FucntionName} From {DLLName}........");
                    Console.Beep();
                    Console.WriteLine($"Address of {FucntionName} is: 0x" + ADDR.ToString("X"));
                    return ADDR;
                }

                Assembly myAsm;
                AssemblyName assembly;
                assembly = new AssemblyName();
                assembly.Name = "ReflectionEmitDelegateTest";
                var MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assembly, System.Reflection.Emit.AssemblyBuilderAccess.Run);
                var MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InMemoryModule", false);
                var MyTypeBuilder = MyModuleBuilder.DefineType("BugattiDelegate", TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.AnsiClass | TypeAttributes.AutoClass, typeof(System.MulticastDelegate));
                ConstructorBuilder constructorBuilder = MyTypeBuilder.DefineConstructor(MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public, CallingConventions.Standard, new Type[] { p1.GetType(), p2.GetType() });
                constructorBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
                var MyMethodBuilder = MyTypeBuilder.DefineMethod("Invoke", MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Public, returnDT.GetType(), new Type[] { p1.GetType(), p2.GetType() });
                MyMethodBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
                Type tex = MyTypeBuilder.CreateType();
                var func = Marshal.GetDelegateForFunctionPointer(DynamicLookUPFunc(), tex);
                var FuncRet = func.DynamicInvoke(new object[] { p1, p2 });
                Console.WriteLine($"Invoking  {FucntionName} With Return {FuncRet}...........");

                return FuncRet;
            }






            object ExecFunctionWithReflection4(string DLLName, string FucntionName, object p1, object p2, object p3, object p4, object returnDT)
            {
                IntPtr DynamicLookUPFunc()
                {
                    object retptr = GetProcADDR.Invoke(null, new object[] { GetModuleHAND.Invoke(null, new object[] { DLLName }), FucntionName });
                    IntPtr ADDR = (IntPtr)retptr;
                    Console.WriteLine($"Hooking {FucntionName} From {DLLName}........");
                    Console.Beep();
                    Console.WriteLine($"Address of {FucntionName} is: 0x" + ADDR.ToString("X"));
                    return ADDR;
                }

                Assembly myAsm;
                AssemblyName assembly;
                assembly = new AssemblyName();
                assembly.Name = "ReflectionEmitDelegateTest";
                var MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assembly, System.Reflection.Emit.AssemblyBuilderAccess.Run);
                var MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InMemoryModule", false);
                var MyTypeBuilder = MyModuleBuilder.DefineType("BugattiDelegate", TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.AnsiClass | TypeAttributes.AutoClass, typeof(System.MulticastDelegate));
                ConstructorBuilder constructorBuilder = MyTypeBuilder.DefineConstructor(MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public, CallingConventions.Standard, new Type[] { p1.GetType(), p2.GetType(), p3.GetType(), p4.GetType() });
                constructorBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
                var MyMethodBuilder = MyTypeBuilder.DefineMethod("Invoke", MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Public, returnDT.GetType(), new Type[] { p1.GetType(), p2.GetType(), p3.GetType(), p4.GetType() });
                MyMethodBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
                Type tex = MyTypeBuilder.CreateType();
                var func = Marshal.GetDelegateForFunctionPointer(DynamicLookUPFunc(), tex);
                var FuncRet = func.DynamicInvoke(new object[] { p1, p2, p3, p4,});
                Console.WriteLine($"Invoking  {FucntionName} With Returned Value {FuncRet}...........");

                return FuncRet;
            }          
                var result = ExecFunctionWithReflection4("user32.dll", "MessageBoxA", IntPtr.Zero, "Fuuuuuuuuuck Powershell", "This is My MessageBox From C#", 0, (Int32)10);
    



            /*void reflectionfunction()
            {


                Assembly myAsm;
                AssemblyName assembly;
                assembly = new AssemblyName();
                assembly.Name = "ReflectionEmitDelegateTest";
                var MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assembly, System.Reflection.Emit.AssemblyBuilderAccess.Run);
                var MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InMemoryModule", false);
                var MyTypeBuilder = MyModuleBuilder.DefineType("BugattiDelegate", TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.AnsiClass | TypeAttributes.AutoClass, typeof(System.MulticastDelegate));
                ConstructorBuilder constructorBuilder = MyTypeBuilder.DefineConstructor(MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(IntPtr), typeof(String), typeof(String), typeof(int) });
                constructorBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
                Int32 ex = 90;
                var MyMethodBuilder = MyTypeBuilder.DefineMethod("Invoke", MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Public, ex.GetType(), new Type[] { typeof(IntPtr), typeof(String), typeof(String), typeof(int) });
                MyMethodBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
                Type tex = MyTypeBuilder.CreateType();


                IntPtr DynamicLookUPFunc(string FunctionName, string moduleName)
                {
                    object ret = methods[4].Invoke(null, new object[] { UNSAFEWin32Type.GetMethod("GetModuleHandle").Invoke(null, new object[] { moduleName }), FunctionName });
                    return (IntPtr)ret;
                }
                string dllN;
                string FunctionN;
                dllN = "user32.dll";
                FunctionN = "MessageBoxA";
                Console.WriteLine($"Address of {FunctionN} is: 0x" + DynamicLookUPFunc(FunctionN, dllN).ToString("X"));

                var func = Marshal.GetDelegateForFunctionPointer(DynamicLookUPFunc(FunctionN, dllN), tex);
                func.DynamicInvoke(new object[] { IntPtr.Zero, "Fuuuuuuuuuck Powershell", "This is My MessageBox From C#", 0 });
                
                //Var for Parameters DataType and Values DLL Name Function Name



            
            }*/


        }
    }
}
