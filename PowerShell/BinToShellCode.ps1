param([Parameter()][String]$File)
$bytes = [System.IO.File]::ReadAllBytes([String]$File)
"arrayofbytes=" 
foreach($b in $bytes){[System.Console]::Write("0x"+$b.ToString("X")+",")}