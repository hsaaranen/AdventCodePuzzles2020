using System;
using System.Collections.Generic;
using System.IO;

var entries = GetEntries(); 
var twoSumEntries = GetTwoSumEntries(entries);

if (twoSumEntries != null)
{
    Console.WriteLine($"Correct 2 sum entries were {twoSumEntries[0]} and {twoSumEntries[1]} and multiplication is {twoSumEntries[0] * twoSumEntries[1]}");
}

var treeSumEntries = GetTreeSumEntries(entries);

if (treeSumEntries != null)
{
    Console.WriteLine($"Correct 3 sum entries were {treeSumEntries[0]}, {treeSumEntries[1]} and {treeSumEntries[2]}  and multiplication is {treeSumEntries[0] * treeSumEntries[1] * treeSumEntries[2]}");
}


static int[] GetEntries()
{
    var reader = File.OpenText("..\\..\\..\\input.txt");
    var entries = new List<int>();
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        entries.Add(int.Parse(line));
    }

    return entries.ToArray();
}

static int[] GetTwoSumEntries(int[] entries)
{
    foreach (var entry1 in entries)
    {
        foreach (var entry2 in entries)
        {
            if (entry1 + entry2 == 2020)
            {
                return new[] {entry1, entry2};
            }
        }
    }

    return null;
}

static int[] GetTreeSumEntries(int[] entries)
{
    foreach (var entry1 in entries)
    {
        foreach (var entry2 in entries)
        {
            foreach (var entry3 in entries)
            {
                if (entry1 + entry2 + entry3 == 2020)
                {
                    return new[] { entry1, entry2, entry3 };
                }
            }
        }
    }
    return null;
}
    

