using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode6
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllText("..\\..\\..\\input.txt");
            var groupData = input.Split(new[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries);
            var groups = new List<string[]>();
            var sumOfUniqueAnswersInGroups = 0;
            var sumOfCommonYesAnswersInGroups = 0;

            foreach (var group in groupData)
            {
                groups.Add(group.Split("\n").Where(g => g != "").ToArray());
            }

            foreach (var group in groups)
            {
                var uniqueYesAnswers = new List<char>();
                var commonYesAnswers = new List<char>();
                foreach (var passenger in group)
                {
                    foreach (var yesAnswer in passenger)
                    {
                        if (!uniqueYesAnswers.Contains(yesAnswer))
                        {
                            uniqueYesAnswers.Add(yesAnswer);
                        }

                        if (group.All(s => s.Contains(yesAnswer)))
                        {
                            if(!commonYesAnswers.Contains(yesAnswer))
                            {
                                commonYesAnswers.Add(yesAnswer);
                            }
                        }
                    }
                }
                sumOfUniqueAnswersInGroups += uniqueYesAnswers.Count;
                sumOfCommonYesAnswersInGroups += commonYesAnswers.Count;
            }

            Console.WriteLine($"Sum of unique yes answers in groups is {sumOfUniqueAnswersInGroups}");
            Console.WriteLine($"Sum of common yes answers in groups is {sumOfCommonYesAnswersInGroups}");
        }
    }
}
