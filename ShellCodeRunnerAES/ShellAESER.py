import sys
from Cryptodome.Cipher import AES
from os import urandom
import hashlib

KEY = urandom(16)


def pad(s):
	return s + (AES.block_size - len(s) % AES.block_size) * chr(AES.block_size - len(s) % AES.block_size)

# def CSenc(plaintext, key):
#
# 	k = hashlib.sha256(key).digest()
# 	plaintext = pad(plaintext)
# 	iv = 16 * '\x00'
# 	cipher = AES.new(k, AES.MODE_CBC,iv)
# 	return cipher.encrypt(bytes(plaintext)),cipher.iv


def CPPenc(plaintext, key):

	k = hashlib.sha256(key).digest()
	iv = 16 * '\x00'
	plaintext = pad(plaintext)
	cipher = AES.new(k, AES.MODE_CBC,iv)
	return cipher.encrypt(bytes(plaintext))#,cipher.iv



plaintext = open("message.bin", "r").read()

# cs_ciphertext ,cs_IV= CSenc(plaintext, KEY)
#cpp_ciphertext ,cpp_IV= CPPenc(plaintext, KEY)
cpp_ciphertext = CPPenc(plaintext, KEY)


# print("C++ Format: \n unsigned char IV[] =" )
# print(' { 0x' + ', 0x'.join(hex(ord(x))[2:] for x in cpp_IV) + ' };')
print("C++ Format: \n unsigned char AESkey[] =" )
print(' { 0x' + ', 0x'.join(hex(ord(x))[2:] for x in KEY) + ' };')
print(" unsigned char payload[] =" )
print(' { 0x' + ', 0x'.join(hex(ord(x))[2:] for x in cpp_ciphertext) + ' };')


# print("C# Format: \n Byte[] IV =" )
# print(' { 0x' + ', 0x'.join(hex(ord(x))[2:] for x in cs_IV) + ' };')
# print(" Byte[] AESkey =" )
# print(' { 0x' + ', 0x'.join(hex(ord(x))[2:] for x in KEY) + ' };')
# print(" Byte[] paylaod =" )
# print(' { 0x' + ', 0x'.join(hex(ord(x))[2:] for x in cs_ciphertext) + ' };')

