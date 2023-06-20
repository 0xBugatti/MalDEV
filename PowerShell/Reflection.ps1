#Assembly Can Be EXE orDll
#Load From Remote Assembly
$AssemblyBytes_Remote = (New-Object System.Net.WebClient).DownloadData('http://127.0.0.1:2100/APP.exe');
#=====or=======
#Load From Local Assembly
$AssemblyBytes = [IO.File]::ReadAllBytes("C:\Users\Blu-Ray\source\testing\APP\APP\bin\Debug\APP.exe")

#First Reflection Step - Load The Assembly to Memory 

$HelloWorldEXE = [System.Reflection.Assembly]::Load($AssemblyBytes_Remote)
#=====or=======
$HelloWorldEXE = [System.Reflection.Assembly]::Load($AssemblyBytes)




#Second Reflection Step - Reflect The Assembly to get Classes
##====RAW:[System.Diagnostics.ProcessStartInfo]====##
$class = $HelloWorldEXE.GetType('APP.Program')
#=====or=======
$classes = $HelloWorldEXE.GetTypes()




#Third Reflection Step - Get Methods From Reflected Class
##====RAW: Get-Process or Object| Get-Member -MemberType Methods====##
$MainMethod =$class.GetMethod("Main")
#=====or=======
$Methods = $class.GetMethods()
#invoking  Got static Method
$MainMethod.Invoke($null, [Object[]] @(@(,([String[]] @()))));
#Fourth Reflection Step - Craeting Objects (For Invoke Non Static Method)
##====RAW:$Object = New-Object -TypeName ====##$OBJECT = [Activator]::CreateInstance($class)
$OBJECT.MessageMe("Param")


