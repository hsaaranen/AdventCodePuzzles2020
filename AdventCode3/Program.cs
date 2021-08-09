using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode3
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = CreateMap();
            var trees = GetTreesForSlope(map, 3, 1);
            Console.WriteLine($"Tree count for 1 to 3 slope is {trees}");

            long slope1 = GetTreesForSlope(map, 1, 1);
            long slope2 = GetTreesForSlope(map, 3, 1);
            long slope3 = GetTreesForSlope(map, 5, 1);
            long slope4 = GetTreesForSlope(map, 7, 1);
            long slope5 = GetTreesForSlope(map, 1, 2);

            long result = slope1 * slope2 * slope3 * slope4 * slope5;

            Console.WriteLine($"Result for puzzle par to is {result}");
        }

        private static int GetTreesForSlope(char[][] map, int slopeRight, int slopeDown)
        {
            var treeCount = 0;
            var dx = 0;
            var dy = 0;
            while (dy < map.Length - 1)
            {
                dx += slopeRight;
                dy += slopeDown;
                if (IsPointTree(map, dx, dy))
                {
                    treeCount++;
                }
            }
            return treeCount;
        }

        private static bool IsPointTree(char[][] map, int x, int y)
        {
            x = x % map.First().Length;
            return map[y][x].Equals('#');
        }

        static char[][] CreateMap()
        {
            var stream = File.OpenText("..\\..\\..\\input.txt");
            string line;
            var map = new List<char[]>();
            while ((line = stream.ReadLine()) != null)
            {
                map.Add(line.ToCharArray());
            }
            return map.ToArray();
        }
    }
}

