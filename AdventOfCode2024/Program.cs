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
        }

        static void Main(string[] args)
        {
            // Each day is it's own function; name takes the format Day<dayNumber>().
            // Part 1 & 2 of each day are contained within the same function.

            // Each input file follows the naming convention 'Input-<dayNumber>-<part>.txt'
            
            Day1();
        }
    }
}
