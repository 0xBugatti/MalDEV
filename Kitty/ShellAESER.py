import sys
from Cryptodome.Cipher import AES
from os import urandom
import hashlib

KEY = urandom(16)


def pad(s):
	return s + (AES.block_size - len(s) % AES.block_size) * chr(AES.block_size - len(s) % AES.block_size)

def aesenc(plaintext, key):

	k = hashlib.sha256(key).digest()
	iv = 16 * '\x00'
	plaintext = pad(plaintext)
	cipher = AES.new(k, AES.MODE_CBC, iv)

	return cipher.encrypt(bytes(plaintext))



plaintext = open("ver64xor.bin", "r").read()

ciphertext = aesenc(plaintext, KEY)
print("C++ Format: \n char AESkey[] =" )
print(' { 0x' + ', 0x'.join(hex(ord(x))[2:] for x in KEY) + ' };')
print(" unsigned char payload[] =" )
print(' { 0x' + ', 0x'.join(hex(ord(x))[2:] for x in ciphertext) + ' };')