// See https://aka.ms/new-console-template for more information
using CryptographyApp;

string original = "Here is some data to encrypt!";
Console.WriteLine("Original:   {0}", original);

string encrypted = AesEncryption.Encrypt(original);
Console.WriteLine("Encrypted: {0}", encrypted);

string decrypted = AesEncryption.Decrypt(encrypted);
Console.WriteLine("Decrypted: {0}", decrypted);