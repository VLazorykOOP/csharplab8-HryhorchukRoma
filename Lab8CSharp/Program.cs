using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "input.txt";
            string outputFilePath = "output.txt";
            string replacementIP = "0.0.0.0"; // IP-адреса для заміни

            // Читання даних з файлу
            string text = File.ReadAllText(inputFilePath);
            Console.WriteLine("Original Text:");
            Console.WriteLine(text);

            // Витяг IP-адрес з тексту
            Regex ipRegex = new Regex(@"\b(?:\d{1,3}\.){3}\d{1,3}\b");
            var ipAddresses = ipRegex.Matches(text).Select(match => match.Value).Distinct().ToList();

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
                text = text.Replace(ipToReplace, replacementIP);
                text = RemoveUnwantedWords(text);

                File.WriteAllText(inputFilePath, text);
                Console.WriteLine("\nText after replacement and word removal:");
                Console.WriteLine(text);
            }

            // Знаходження та виведення симетричних слів
            var symmetricWords = FindSymmetricWords(text);
            Console.WriteLine("\nSymmetric Words:");
            foreach (var word in symmetricWords)
            {
                Console.WriteLine(word);
            }
        }

        static string RemoveUnwantedWords(string text)
        {
            // Регулярний вираз для видалення однобуквенних слів та слів, що починаються на 'a', 'b', 'c', 'd', 'e'
            string pattern = @"\b([a-eA-E]\w*|\w)\b";
            return Regex.Replace(text, pattern, "").Trim();
        }

        static List<string> FindSymmetricWords(string text)
        {
            // Регулярний вираз для знаходження слів
            var words = Regex.Matches(text, @"\b\w+\b")
                             .Cast<Match>()
                             .Select(m => m.Value)
                             .Distinct()
                             .ToList();

            // Фільтрація симетричних слів
            var symmetricWords = words.Where(IsSymmetric).ToList();
            return symmetricWords;
        }

        static bool IsSymmetric(string word)
        {
            int len = word.Length;
            for (int i = 0; i < len / 2; i++)
            {
                if (word[i] != word[len - i - 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
