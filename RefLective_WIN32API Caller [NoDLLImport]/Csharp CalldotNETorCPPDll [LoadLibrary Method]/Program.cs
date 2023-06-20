using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_CalldotNETorCPPDll__LoadLibrary_Method_
{

    internal class Program
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);
        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);
        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        static extern bool FreeLibrary(int hModule);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate double MethodName(string a);

        static void Main(string[] args)
        {

            string DllName = "user32.dll";                 //would be like: string  DllName = ChooseWhatDllToCall();
            string FuncName = "MessageBoxA";    //would be like: string FuncName = ChooseWhatFuncToUse();
            int hModule = LoadLibrary(DllName);
            IntPtr FuncAddr;
            FuncAddr = GetProcAddress(hModule, FuncName);

            Assembly myAsm;
            AssemblyName assembly;
            assembly = new AssemblyName();
            assembly.Name = "ReflectionEmitDelegateTest";
            var MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assembly, System.Reflection.Emit.AssemblyBuilderAccess.Run);
            var MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InMemoryModule", false);
            var MyTypeBuilder = MyModuleBuilder.DefineType("BugattiDelegate", TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.AnsiClass | TypeAttributes.AutoClass, typeof(System.MulticastDelegate));
                                                                                                                                                                                                                    //DataType Of Parameters        
            ConstructorBuilder constructorBuilder = MyTypeBuilder.DefineConstructor(MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(IntPtr), typeof(String), typeof(String), typeof(int) });
            constructorBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
                                                                                                                                                                                //DataType Of Return            //DataType Of Parameters
            var MyMethodBuilder = MyTypeBuilder.DefineMethod("Invoke", MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Public, typeof(UInt32), new Type[] { typeof(IntPtr), typeof(String), typeof(String), typeof(int) });
            MyMethodBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
            Type tex = MyTypeBuilder.CreateType();
            Console.WriteLine($"Address of {FuncName} is: 0x" + FuncAddr.ToString("X"));
            var func = Marshal.GetDelegateForFunctionPointer(FuncAddr, tex);
                                             //Passing Parameters As Array of var or RealName of it (Object) [Casted to any Type and Any Type Inherit From it as TS]
            func.DynamicInvoke(new object[] { IntPtr.Zero, "Fuuuuuuuuuck Powershell", "This is My MessageBox From C#", 0 });
            FreeLibrary(hModule);
            return;
        }
    }
}





