using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // ASCII art
        Console.WriteLine(@"
        00000         000       0000000 000000000000  0000        0000000000
      000000000     000000     000000   00 000000 00  0000        0000000000
    0000   0000    00    00   000       0   0000   0  0000        0000
    0000           00    00   0000000       0000      0000        0000000000
    0000           00000000        000      0000      0000     0  0000
    0000   0000   000    000       000      0000      0000    00  0000     0
      000000000   000    000  0000000       0000      0000000000  0000000000
        00000     000    000  000000        0000      0000000000  0000000000


          00           ###       ###
        00000         ###       ###
      000000000     ##################
    0000   0000    ##################
    0000             ###      ###
    0000           ################
    0000   0000   ################
      000000000     ###     ###
        00000      ###     ###");

        Console.WriteLine("Welcome to the Photo Encryption/Decryption App!");
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Encrypt a photo");
        Console.WriteLine("2. Decrypt a photo");

        int option = int.Parse(Console.ReadLine());

        switch (option)
        {
            case 1:
                EncryptPhoto();
                break;
            case 2:
                DecryptPhoto();
                break;
            default:
                Console.WriteLine("Invalid option");
                break;
        }
    }

    static void EncryptPhoto()
    {
        Console.WriteLine("Enter the path of the photo to encrypt:");
        string inputFilePath = Console.ReadLine();

        Console.WriteLine("Enter the encryption key (0-255):");
        int encryptionKey;
        if (!int.TryParse(Console.ReadLine(), out encryptionKey) || encryptionKey < 0 || encryptionKey > 255)
        {
            Console.WriteLine("Invalid encryption key. Please enter a valid integer between 0 and 255.");
            return;
        }

        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string encryptedFilePath = Path.Combine(desktopPath, Path.GetFileNameWithoutExtension(inputFilePath) + "_encrypted.jpg");

        EncryptFile(inputFilePath, encryptedFilePath, encryptionKey);
        Console.WriteLine("Photo encrypted successfully. Encrypted photo saved on the desktop as: " + encryptedFilePath);
    }

    static void DecryptPhoto()
    {
        Console.WriteLine("Enter the path of the photo to decrypt:");
        string inputFilePath = Console.ReadLine();

        Console.WriteLine("Enter the decryption key (0-255):");
        int decryptionKey;
        if (!int.TryParse(Console.ReadLine(), out decryptionKey) || decryptionKey < 0 || decryptionKey > 255)
        {
            Console.WriteLine("Invalid decryption key. Please enter a valid integer between 0 and 255.");
            return;
        }

        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string decryptedFilePath = Path.Combine(desktopPath, Path.GetFileNameWithoutExtension(inputFilePath) + "_decrypted.jpg");

        DecryptFile(inputFilePath, decryptedFilePath, decryptionKey);
        Console.WriteLine("Photo decrypted successfully. Decrypted photo saved on the desktop as: " + decryptedFilePath);
    }

    static void EncryptFile(string inputFile, string outputFile, int key)
    {
        using (FileStream inputStream = File.OpenRead(inputFile))
        using (FileStream outputStream = File.Create(outputFile))
        {
            int byteRead;
            while ((byteRead = inputStream.ReadByte()) != -1)
            {
                byteRead += key;
                outputStream.WriteByte((byte)byteRead);
            }
        }
    }

    static void DecryptFile(string inputFile, string outputFile, int key)
    {
        using (FileStream inputStream = File.OpenRead(inputFile))
        using (FileStream outputStream = File.Create(outputFile))
        {
            int byteRead;
            while ((byteRead = inputStream.ReadByte()) != -1)
            {
                byteRead -= key;
                outputStream.WriteByte((byte)byteRead);
            }
        }
    }
}
