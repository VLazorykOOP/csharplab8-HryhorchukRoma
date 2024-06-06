using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace IPAddressProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "input.txt";
            string outputFilePath = "output.txt";
            string replacementIP = "0.0.0.0"; // IP-адреса для заміни

            // Читання даних з файлу
            string[] lines = File.ReadAllLines(inputFilePath);
            Console.WriteLine("Original Text:");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            // Витяг IP-адрес з тексту
            Regex ipRegex = new Regex(@"\b(?:\d{1,3}\.){3}\d{1,3}\b");
            var ipAddresses = lines.SelectMany(line => ipRegex.Matches(line).Select(match => match.Value)).Distinct().ToList();

            // Підрахунок кількості знайдених IP-адрес
            Console.WriteLine($"\nFound {ipAddresses.Count} unique IP addresses:");

            // Запис IP-адрес у новий файл
            File.WriteAllLines(outputFilePath, ipAddresses);

            foreach (var ip in ipAddresses)
            {
                Console.WriteLine(ip);
            }

            // Заміна IP-адрес за параметрами користувача
            Console.WriteLine("\nEnter an IP address to replace:");
            string ipToReplace = Console.ReadLine();

            if (!ipAddresses.Contains(ipToReplace))
            {
                Console.WriteLine("IP address not found.");
            }
            else
            {
                string[] modifiedLines = lines.Select(line => line.Replace(ipToReplace, replacementIP)).ToArray();
                File.WriteAllLines(inputFilePath, modifiedLines);
                Console.WriteLine("\nText after replacement:");
                foreach (var line in modifiedLines)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
