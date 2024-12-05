using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
            int safeReportsCountWithDampener = 0;

            int test1ErrorCount = 0; // Increases whenever change from increasing to decreasing & visa versa
            int test2ErrorCount = 0; // Increases whenever the adj. levels differ by <1 or >3

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

                // get the number of +ve differences & number of -ve differences: smallest value is the number of times this test fails.
                test1ErrorCount = Math.Min(differences.Count(x => x > 0), differences.Count(x => x < 0));
                test2ErrorCount = differences.Where(x => (Math.Abs(x) > 3 || Math.Abs(x) == 0)).Count();
                Console.WriteLine($"\nt1: {test1ErrorCount.ToString()}");
                Console.WriteLine($"t2: {test2ErrorCount.ToString()}");

                if (test1ErrorCount == 0 && test2ErrorCount == 0)
                {
                    safeReportsCount++;
                }
                else if (test1ErrorCount + test2ErrorCount < 2)
                {
                    safeReportsCountWithDampener++;
                }
                Console.WriteLine($"safe: {safeReportsCount.ToString()}");
                Console.WriteLine($"safe w/ dampener: {(safeReportsCountWithDampener + safeReportsCount).ToString()}");
            }
            
            Pause();

            // ----- Part 2 -----

            // do the same checks as above, but increment a problem counter when a fail condition is met. If this counter <= 1, then the level is safe. 
        }

        static void Day3()
        {
            // ----- Part 1 -----
            string input = System.IO.File.ReadAllText("..\\..\\Problem Input Files\\Input 3-1.txt", System.Text.Encoding.UTF8);
            string pattern = @"(mul\([0-9]+\,[0-9]+\))";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(input);

            int sumOfMultiples = 0;
            foreach (Match match in matches)
            {
                string[] reducedValues = match.Value
                    .Replace("mul(", "")
                    .Replace(")", "")
                    .Split(',');

                sumOfMultiples += Int32.Parse(reducedValues[0]) * Int32.Parse(reducedValues[1]);
            }

            Console.WriteLine(sumOfMultiples.ToString());
            Pause();
        }

        static void Main(string[] args)
        {
            // Each day is it's own function; name takes the format Day<dayNumber>().
            // Part 1 & 2 of each day are contained within the same function.

            // Each input file follows the naming convention 'Input-<dayNumber>-<part>.txt'

            // Day1();
            Day3();
        }
    }
}
