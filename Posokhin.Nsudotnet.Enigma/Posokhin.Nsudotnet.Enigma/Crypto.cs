using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Posokhin.Nsudotnet.Enigma
{
    class Crypto
    {
        private Dictionary<string, Func<SymmetricAlgorithm>> _algorithms = 
            new Dictionary<string, Func<SymmetricAlgorithm>>();
        static void Main(string[] args)
        {
            new Crypto().Run(args);
        }

        public Crypto()
        {
            _algorithms.Add("aes",      () => new AesCryptoServiceProvider());
            _algorithms.Add("des",      () => new DESCryptoServiceProvider());
            _algorithms.Add("rc2",      () => new RC2CryptoServiceProvider());
            _algorithms.Add("rijndael", () => new RijndaelManaged());
        }
        public void Run(string[] args)
        {
            if(args.Length  == 0)
            {
                PrintUsageAndExit();
            }


            switch(args[0].ToLower())
            {
                case "encrypt":
                    {
                        if (args.Length != 4)
                        {
                            PrintUsageAndExit();
                        }
                        string inputFilePath = args[1];
                        string algorithm = args[2].ToLower();
                        string outputFilePath = args[3];

                        Encrypt(inputFilePath, algorithm, outputFilePath);

                        break;
                    }
                case "decrypt":
                    {
                        if (args.Length != 5)
                        {
                            PrintUsageAndExit();
                        }
                        string inputFilePath = args[1];
                        string algorithm = args[2].ToLower();
                        string keyAndIVFilePath = args[3];
                        string outputFilePath = args[4];

                        Decrypt(inputFilePath, algorithm, keyAndIVFilePath, outputFilePath);
                        break;
                    }
                default:
                    PrintUsageAndExit();
                    break;
            }
        }

        public void Encrypt(string inputFilePath, string algorithm, string outputFilePath)
        {
            CheckForInvalidAlgorithm(algorithm);

            using (SymmetricAlgorithm cipher = _algorithms[algorithm]())
            {
                cipher.GenerateIV();
                cipher.GenerateKey();
                
                using (ICryptoTransform encryptor = cipher.CreateEncryptor())
                using (FileStream inputFile = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream outputFile = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(outputFile, encryptor, CryptoStreamMode.Write))
                {
                    inputFile.CopyTo(cryptoStream);
                }

                using (StreamWriter writer = new StreamWriter
                    (new FileStream(String.Concat(inputFilePath,".key"), FileMode.Create, FileAccess.Write)))
                {
                    writer.Write(String.Format("{0}\n{1}",
                        Convert.ToBase64String(cipher.Key),
                        Convert.ToBase64String(cipher.IV)));
                }
            }

            Console.WriteLine("Successfully encrypted into {0}", outputFilePath);
        }

        public void Decrypt(string inputFilePath, string algorithm, string keyAndIVFilePath, string outputFilePath)
        {
            CheckForInvalidAlgorithm(algorithm);

            using (SymmetricAlgorithm cipher = _algorithms[algorithm]())
            {
                using (StreamReader reader = new StreamReader
                    (new FileStream(keyAndIVFilePath, FileMode.Open, FileAccess.Read)))
                {
                    string s = reader.ReadLine();
                    byte[] key = Convert.FromBase64String(s);
                    cipher.Key = key;

                    s = reader.ReadLine();
                    byte[] IV = Convert.FromBase64String(s);
                    cipher.IV = IV;                   
                 }

                using(ICryptoTransform decryptor = cipher.CreateDecryptor())
                using (FileStream inputFile = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream outputFile = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(outputFile, decryptor, CryptoStreamMode.Write))
                {
                    inputFile.CopyTo(cryptoStream);
                }
            }

            Console.WriteLine("Successfully decrypted into {0}", outputFilePath);
        }

        private void PrintUsageAndExit()
        {
            Console.WriteLine(
@"Enigma cipher! Algorithms supported: AES, DES, RC2, Rijndael.
Usage:
crypto.exe encrypt <input file path> <algorithm> <binary output file path>
crypto.exe decrypt <binary input file path> <algorithm> <IV and key file path>  <output file path>");
            System.Environment.Exit(0);
        }

        private void CheckForInvalidAlgorithm(string algorithm)
        {
            if(!_algorithms.ContainsKey(algorithm))
            {
                Console.WriteLine
                    ("Could not find \"{0}\" algorithm. \nPossible ones: \"aes\", \"des\", \"rc2\", \"rijndael\" (case insensitive)", algorithm);
                System.Environment.Exit(0);
            }
        }
    }
}
