using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode9
{
    class Program
    {
        const int preAmpleSize = 25;

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("..\\..\\..\\input.txt");
            var numbers = input.Select(long.Parse).ToArray();
            for (var i = 0; i < numbers.Length; i++)
            {
                if (i >= preAmpleSize)
                {
                    long[] numbersToCampare = new long[preAmpleSize];
                    Array.Copy(numbers, i - preAmpleSize, numbersToCampare, 0, preAmpleSize);
                    bool result = IsNumberSumOfPreviousNumbers(numbers[i], numbersToCampare);
                    if (!result)
                    {
                        Console.WriteLine($"{numbers[i]} is the first number that doesn't meet the criteria");
                        var weakness = FindWeaknessFromXmas(numbers, numbers[i]);
                        Console.WriteLine($"Weakness is {weakness}");
                        return;
                    }
                }
            }
        }

        private static long FindWeaknessFromXmas(long[] numbers, long sumToFind)
        {
            for (var i = 0; i < numbers.Length; i++)
            {
                var sum = numbers[i];
                var sumIndex = 0;
                while (sum < sumToFind)
                {
                    sumIndex++;
                    sum += numbers[i + sumIndex];
                    if (sum == sumToFind)
                    {
                        var biggestNumber = numbers[i];
                        var smallestNumber = numbers[i];
                        for (int j = i; j < i + sumIndex; j++)
                        {
                            biggestNumber = numbers[j] > biggestNumber ? numbers[j] : biggestNumber;
                            smallestNumber = numbers[j] < smallestNumber ? numbers[j] : smallestNumber;
                        }
                        return smallestNumber + biggestNumber;
                    }
                }
            }
            return -1;
        }

        private static bool IsNumberSumOfPreviousNumbers(long n, long[] numbers)
        {
            foreach (var rootNumber in numbers)
            {
                foreach (var number in numbers)
                {
                    if (number == rootNumber)
                    {
                        continue;
                    }

                    if (rootNumber + number == n)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
