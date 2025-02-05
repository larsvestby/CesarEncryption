using System;

public class Program
{
    public static string string1 = "VFF vinder guld igen"; // Bliver til: CMM cpukly nbsk pnlu
    public static string string2 = "Programmering er sjovt"; // Bliver til: Fhewhqccuhydw uh izelj

    public static string Encrypt(string input, int key)
    {
        string output = "";
        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                char baseChar = char.IsUpper(c) ? 'A' : 'a';
                output += (char)((c - baseChar + key) % 26 + baseChar);
            }
            else
            {
                output += c;
            }
        }
        return output;
    }

    public static string Decrypt(string input, int key)
    {
        return Encrypt(input, 26 - key);
    }

    public static void BruteForceDecrypt(string input)
    {
        for (int key = 0; key < 26; key++)
        {
            Console.WriteLine($"Key {key}: {Decrypt(input, key)}");
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine($"Original: {string1}");
        string encrypted1 = Encrypt(string1, 7);
        string decrypted1 = Decrypt(encrypted1, 7);
        Console.WriteLine($"Encrypted 1: {encrypted1}");
        Console.WriteLine($"Decrypted 1: {decrypted1}\n");

        Console.WriteLine($"Original: {string2}");
        string encrypted2 = Encrypt(string2, 16);
        string decrypted2 = Decrypt(encrypted2, 16);
        Console.WriteLine($"Encrypted 2: {encrypted2}");
        Console.WriteLine($"Decrypted 2: {decrypted2}\n");

        while (true)
        {
            Console.WriteLine("Choose an option: \n1. Encrypt text\n2. Brute-force decrypt text\n3. Exit");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                Console.Write("Enter a string to encrypt: ");
                string input = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter a key (0-25): ");
                if (int.TryParse(Console.ReadLine(), out int key) && key >= 0 && key < 26)
                {
                    string encrypted = Encrypt(input, key);
                    Console.WriteLine($"Encrypted: {encrypted}\n");
                    Console.WriteLine($"Decrypted: {Decrypt(encrypted, key)}\n");
                }
                else
                {
                    Console.WriteLine("Invalid key! Please enter a number between 0 and 25.\n");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Enter an encrypted string: ");
                string input = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Brute-force decryption results:");
                BruteForceDecrypt(input);
                Console.WriteLine();
            }
            else if (choice == "3")
            {
                Console.WriteLine("Exiting program.");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option! Please enter 1, 2, or 3.\n");
            }
        }
    }
}