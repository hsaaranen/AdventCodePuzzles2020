using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace AdventCode5
{
    class Program
    {
        static void Main()
        {
            var boardingPassCodes = File.ReadAllLines("..\\..\\..\\input.txt");
            var largestSeatId = 0;
            var boardingMapping = new bool[128 * 8];

            foreach (var code in boardingPassCodes)
            {
                var (row, column) = GetSeatPosition(code);
                var seatId = row * 8 + column;
                largestSeatId = seatId > largestSeatId ? seatId : largestSeatId;
                boardingMapping[seatId] = true;
            }
            Console.WriteLine($"Largest seat id is {largestSeatId}");

            var mySeat = boardingMapping.Select((value, index) => new Tuple<bool, int>(value, index)).Where((x) =>
            {
                if(x.Item2 < 1 || x.Item2 == boardingMapping.Length)
                {
                    return false;
                }
                return x.Item1 == false && boardingMapping[x.Item2 - 1] && boardingMapping[x.Item2 + 1];
            }).Single();

            Console.WriteLine($"My seat id is {mySeat.Item2}");

        }

        private static (int, int) GetSeatPosition(string positionCode)
        {
            var rowsCode = positionCode.Take(7).Select(r => r.Equals('B')).ToArray();
            var columnsCode = positionCode.Substring(7).Select(c => c.Equals('R')).ToArray();

            var row = BinaryIteratePosition(0, 127, rowsCode);
            var column = BinaryIteratePosition(0, 7, columnsCode);

            return (row, column);
        }

        private static int BinaryIteratePosition(int min, int max, bool[] positionCode)
        {
            foreach (var code in positionCode.Take(positionCode.Length))
            {
                if (code)
                {
                    // upper half
                    min += Convert.ToInt32(Math.Ceiling((max - min) / 2.00));
                    continue;
                }
                // lower half
                max -= Convert.ToInt32(Math.Ceiling((max - min) / 2.00));
            }
            return max;
        }
    }
}
