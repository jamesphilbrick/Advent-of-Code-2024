using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal class Program
    {
        static void Pause()
        {
            Console.ReadLine();
        }

        static void Day1()
        {
            // ----- Part 1 -----
            // Split the columns into two groups (lists)
            string input = System.IO.File.ReadAllText("..\\..\\Problem Input Files\\Input 1-1.txt", System.Text.Encoding.UTF8);
            
            List<int> group1 = new List<int>();
            List<int> group2 = new List<int>();
            List<int> groupsSubtracted = new List<int>();

            foreach (string line in input.Split('\n'))
            {
                if (line.Length == 13)
                {
                    group1.Add(Int32.Parse(line.Split(new[] { "   " }, StringSplitOptions.None)[0]));
                    group2.Add(Int32.Parse(line.Split(new[] { "   " }, StringSplitOptions.None)[1]));
                }
            }

            // Sort and subtract lists
            group1.Sort();
            group2.Sort();

            // to do: rewrite the below using LINQ
            for (int i = 0; i < group1.Count; i++)
            {
                groupsSubtracted.Add(Math.Abs(group1[i] - group2[i]));
            }
            int totalDifferences = groupsSubtracted.Sum(x => x);
            Console.WriteLine(totalDifferences.ToString());
            Pause();

            // ----- Part 2 -----
            List<int> counts = new List<int>();
            foreach (int i in group1)
            {
                int _count = group2.Count(x => x == i);
                counts.Add(i * _count);
            }
            Console.WriteLine(counts.Sum());
            Pause();
        }

        static void Day2()
        {
            // ----- Part 1 -----
            List<string> levels = System.IO.File.ReadAllLines("..\\..\\Problem Input Files\\Input 2-1.txt", System.Text.Encoding.UTF8).ToList();
            int safeReportsCount = 0;

            foreach (string level in levels)
            {
                // convert line to list of ints
                List<int> values = level.Split(' ').Select(x => int.Parse(x)).ToList();

                // Create a list of the differences between each adjacent value
                List<int> differences = new List<int>();
                for (int i = 0;i < values.Count-1; i++)
                {
                    int pairDifference = values[i] - values[i+1];
                    differences.Add(pairDifference);
                }

                // check this difference list against the two conditions
                if (differences.Where(x => (Math.Abs(x) > 3 || Math.Abs(x) == 0)).Count() > 0)
                {
                    // fail
                    continue;
                }
                else if (differences.Count(x => x > 0) > 0 && 
                         differences.Count(x => x < 0) > 0)
                {
                    // fail
                    continue;
                }
                else
                {
                    // pass
                    safeReportsCount++;
                }
            }
            Console.WriteLine(safeReportsCount.ToString());
            Pause();

            // ----- Part 2 -----
        }

        static void Main(string[] args)
        {
            // Each day is it's own function; name takes the format Day<dayNumber>().
            // Part 1 & 2 of each day are contained within the same function.

            // Each input file follows the naming convention 'Input-<dayNumber>-<part>.txt'

            // Day1();
            Day2();
        }
    }
}
