using System;
using System.IO;

public class Program
{
    public static string string1 = "VFF vinder guld igen"; // Bliver til: CMM cpukly nbsk pnlu
    public static string string2 = "Programmering er sjovt"; // Bliver til: Fhewhqccuhydw uh izelj

    // English letter frequencies (normalized to be between 0 and 1)
    public static readonly float[] Frequencies = [
        0.08167f, 0.01492f, 0.02782f, 0.04253f, 0.12702f, // a-e
        0.02228f, 0.02015f, 0.06094f, 0.06966f, 0.00153f, // f-j
        0.00772f, 0.04025f, 0.02406f, 0.06749f, 0.07507f, // k-o
        0.01929f, 0.00095f, 0.05987f, 0.06327f, 0.09056f, // p-t
        0.02758f, 0.00978f, 0.02360f, 0.00150f, 0.01974f, // u-y
        0.00074f  // z
    ];

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

    // Frequency analysis to compare letter frequencies
    public static float CompareFrequencies(string text)
    {
        int[] frequency = new int[26];
        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                frequency[char.ToLower(c) - 'a']++;
            }
        }

        float score = 0f;
        int totalLetters = 0;
        foreach (int count in frequency)
        {
            totalLetters += count;
        }

        // Calculate the frequency of each letter in the given text
        for (int i = 0; i < 26; i++)
        {
            if (totalLetters > 0)
            {
                float observedFrequency = (float)frequency[i] / totalLetters;
                score += Math.Abs(observedFrequency - Frequencies[i]);
            }
        }

        return score;
    }

    public static void BruteForceDecrypt(string input)
    {
        float bestScore = float.MaxValue;
        int bestKey = -1;
        string bestDecryption = "";

        for (int key = 0; key < 26; key++)
        {
            string decryptedText = Decrypt(input, key);
            float score = CompareFrequencies(decryptedText);

            // Only keep track of the best result (the one with the lowest frequency score)
            if (score < bestScore)
            {
                bestScore = score;
                bestKey = key;
                bestDecryption = decryptedText;
            }
        }

        Console.WriteLine($"\nBest Key: {bestKey} with Score: {bestScore}");
        Console.WriteLine($"Decrypted Text: {bestDecryption}\n");
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
            Console.WriteLine("Choose an option: \n1. Encrypt text\n2. Brute-force decrypt text with frequency analysis\n3. .Txt Brute-force decrypt\n4. Exit");
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
                Console.WriteLine("Brute-force decryption with frequency analysis results:");
                BruteForceDecrypt(input);
                Console.WriteLine();
            }
            else if (choice == "3")
            {
                Console.WriteLine("Enter file name:\n");
                string path = Console.ReadLine() ?? string.Empty;
                string filePath = $"../../../Lyrics/{path}";

                if (File.Exists(filePath))
                {
                    string text = File.ReadAllText(filePath);
                    Console.WriteLine("Encrypted text:\n" + text + "\n");

                    Console.WriteLine("Brute-force decryption with frequency analysis results:");
                    BruteForceDecrypt(text);
                }
                else
                {
                    Console.WriteLine("Error: File not found!\n");
                }
            }
            else if (choice == "4")
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
