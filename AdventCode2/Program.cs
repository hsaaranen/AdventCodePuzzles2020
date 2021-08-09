using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = GetInputLines();
            int numberOfValidPasswords = 0;
            int numberOfValidPasswordsWithUpdatedPolicy = 0;
            foreach (var line in lines)
            {
                var range = GetRanges(line);
                var letter = GetPolicyLetter(line);
                var password = line.Substring(line.LastIndexOf(' ') + 1, line.Length - (line.LastIndexOf(' ') + 1));
                if (VerifyPassword(password, range, letter))
                {
                    numberOfValidPasswords++;
                }

                if (VerifyPasswordWitUpdatedPolicy(password, range, letter))
                {
                    numberOfValidPasswordsWithUpdatedPolicy++;
                }
            }
            Console.WriteLine($"Number of valid passwords is {numberOfValidPasswords}");
            Console.WriteLine($"Number of valid passwords with updated policy is {numberOfValidPasswordsWithUpdatedPolicy}");
        }

        private static List<string> GetInputLines()
        {
            using var stream = File.OpenText("..\\..\\..\\input.txt");
            var lines = new List<string>();
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;
        }

        private static Tuple<int, int> GetRanges(string line)
        {
            var minMaxSeparatorIndex = line.IndexOf('-');
            var min = int.Parse(line.Substring(0, minMaxSeparatorIndex));
            var max = int.Parse(line.Substring(minMaxSeparatorIndex + 1, line.IndexOf(' ') - (minMaxSeparatorIndex + 1)));

            return new Tuple<int, int>(min, max);
        }

        private static char GetPolicyLetter(string line)
        {
            var letter = line.Substring(line.IndexOf(':') - 1, 1).ToCharArray();
            return letter.First();
        }

        private static bool VerifyPassword(string password, Tuple<int, int> range, char policyLetter)
        {
            var hits = password.Count(s => s.Equals(policyLetter));
            return hits >= range.Item1 && hits <= range.Item2;
        }
        private static bool VerifyPasswordWitUpdatedPolicy(string password, Tuple<int, int> range, char policyLetter)
        {
            var firstIndexContainsLetter = password[range.Item1 - 1].Equals(policyLetter);
            var secondIndexContainsLetter = password[range.Item2 - 1].Equals(policyLetter);
            return firstIndexContainsLetter ^ secondIndexContainsLetter;
        }
    }
}
