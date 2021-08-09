using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AdventCode4
{
    class Program
    {
        static void Main()
        {
            var passports = new List<Dictionary<string, string>>();
            var stream = File.OpenText("..\\..\\..\\input.txt");
            var data = stream.ReadToEnd();
            var rawPassports = data.Split(new[] {"\n\n"},
                StringSplitOptions.RemoveEmptyEntries).Select(p => p.Replace('\n', ' ')).ToArray();
            foreach (var rawPassport in rawPassports)
            {
                var datas = rawPassport.Split(" ");
                var passport = datas.Where(s => s.Contains(":")).Select(d => d.Split(":")).ToDictionary(p => p[0], p => p[1]);
                passports.Add(passport);
            }

            var numberOfValidPassports = 0;
            foreach (var passport in passports)
            {
                if (!ValidateFields(passport)) continue;
                if (!ValidateData(passport)) continue;
                numberOfValidPassports++;
            }

            Console.WriteLine($"Number of valid passports is {numberOfValidPassports}");
        }

        private static bool ValidateData(Dictionary<string, string> passport)
        {
            return ValidateYear(passport["byr"], 1920, 2002) &&
            ValidateYear(passport["iyr"], 2010, 2020) &&
            ValidateYear(passport["eyr"], 2020, 2030) &&
            ValidateHeight(passport["hgt"]) &&
            ValidateHairColor(passport["hcl"]) &&
            ValidateEyeColor(passport["ecl"]) &&
            ValidatePassportId(passport["pid"]);
        }

        private static bool ValidateHairColor(string hairColor)
        {
            if (hairColor.StartsWith('#') && hairColor.Length == 7)
            {
                return int.TryParse(hairColor.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _);
            }

            return false;
        }

        private static bool ValidatePassportId(string passportId)
        {
            if (passportId.Length == 9)
            {
                return int.TryParse(passportId, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
            }

            return false;
        }

        private static bool ValidateEyeColor(string eyeColor)
        {
            return eyeColor.Equals("amb") ||
                   eyeColor.Equals("blu") ||
                   eyeColor.Equals("brn") ||
                   eyeColor.Equals("gry") ||
                   eyeColor.Equals("grn") ||
                   eyeColor.Equals("hzl") ||
                   eyeColor.Equals("oth");
        }

        private static bool ValidateHeight(string height)
        {
            var heightNum = int.Parse(height.Substring(0, height.Length - 2));

            if (height.EndsWith("cm"))
            {
                if (heightNum >= 150 && heightNum <= 193)
                {
                    return true;
                }
            }

            if (height.EndsWith("in"))
            {
                if (heightNum >= 59 && heightNum <= 76)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ValidateYear(string year,int  min, int max)
        {
            if (year.Length == 4)
            {
                var byrInt = int.Parse(year);
                if (byrInt <= max && byrInt >= min)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool ValidateFields(Dictionary<string, string> passport)
        {
            return passport.ContainsKey("byr")
                   && passport.ContainsKey("iyr")
                   && passport.ContainsKey("eyr")
                   && passport.ContainsKey("hgt")
                   && passport.ContainsKey("hcl")
                   && passport.ContainsKey("ecl")
                   && passport.ContainsKey("pid");
        }
    }
}
