using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

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

            // ----- Part 2 -----

            // Adjust regex pattern to also match do() and don't()
            pattern = @"(mul\([0-9]+\,[0-9]+\))|(do\(\))|(don\'t\(\))"; 
            regex = new Regex(pattern);
            matches = regex.Matches(input);
            sumOfMultiples = 0;

            // 1 for true, 0 for false. Can be used as a multiplyer
            int isDo = 1; 

            foreach (Match match in matches)
            {
                // if not a mul() instruction, set isDo accordingly.
                if (!match.Value.Contains("mul"))
                {
                    isDo = match.Value == "do()"    ? 1 : 0;
                    isDo = match.Value == "don't()" ? 0 : 1;
                    continue;
                }

                string[] reducedValues = match.Value
                    .Replace("mul(", "")
                    .Replace(")", "")
                    .Split(',');

                sumOfMultiples += Int32.Parse(reducedValues[0]) * Int32.Parse(reducedValues[1]) * isDo;
            }

            Console.WriteLine(sumOfMultiples.ToString());
            Pause();
        }

        static void Day4()
        {
            // 2549

            // Import file into a grid.
            List<string> inputLines = System.IO.File.ReadAllLines("..\\..\\Problem Input Files\\Input 4-1.txt", System.Text.Encoding.UTF8).ToList();

            // Get dimensions of import data; this assumes all rows are the same length (grid is of NxM size)
            int rowCount = inputLines.Count;
            int colCount = inputLines[0].Length;

            // Import as list of lists (rows)
            List<List<char>> horizontal = new List<List<char>>();
            List<List<char>> vertical   = new List<List<char>>();
            List<List<char>> diagNE     = new List<List<char>>();
            List<List<char>> diagNW     = new List<List<char>>();

            for (int r = 0; r < (2 * rowCount - 1); r++)
            {
                if (r < rowCount)
                {
                    horizontal.Add(inputLines[r].ToCharArray().ToList());
                    vertical.Add(inputLines[r].ToCharArray().ToList());
                    diagNE.Add(inputLines[r].ToCharArray().ToList());
                    diagNW.Add(inputLines[r].ToCharArray().ToList());
                }
                else
                {
                    List<char> zeroRowNE = Enumerable.Repeat('0', colCount).ToList();
                    List<char> zeroRowNW = Enumerable.Repeat('0', colCount).ToList();
                    diagNE.Add(zeroRowNE);
                    diagNW.Add(zeroRowNW);
                }
            }

            // diagNW
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = colCount - 1; c > r; c--)
                {
                    char temp = new char();
                    temp = diagNW[r][c];
                    diagNW[r][c] = diagNW[r + rowCount][c];
                    diagNW[r + rowCount][c] = temp;
                }
            }

            // diagNE
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount-r-1; c++)
                {
                    char temp = new char();
                    temp = diagNE[r][c];
                    diagNE[r][c] = diagNE[r + rowCount][c];
                    diagNE[r + rowCount][c] = temp;
                }
            }

            // vertical - rotated by 90 deg via transpose then reversal of each row
            for (int r=0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    // Transpose
                    vertical[r][c] = horizontal[c][r];
                }
            }
            for (int r = 0; r < rowCount; r++)
            {
                // Reversal
                vertical[r].Reverse();
            }

            string pattern = @"((XMAS)|(SAMX))";
            Regex regex = new Regex(pattern);
            MatchCollection horizontalMatches = regex.Matches(HelperFunctions.CharGridToString(horizontal));
            MatchCollection verticalMatches   = regex.Matches(HelperFunctions.CharGridToString(vertical));
            MatchCollection diagNEMatches     = regex.Matches(HelperFunctions.CharGridToString(diagNE));
            MatchCollection diagNWMatches     = regex.Matches(HelperFunctions.CharGridToString(diagNW));
            //Console.WriteLine(
            //    horizontalMatches.Count + 
            //    verticalMatches.Count + 
            //    diagNEMatches.Count + 
            //    diagNWMatches.Count);

            // The above solution is over complicated, and doesn't give the correct answer anyway...
            // Likely better to do some kernal thing. 

            // Covert the input rows to a 2d array
            char[,] grid = new char[rowCount, colCount];

            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0;c < colCount; c++)
                {
                    grid[r,c] = inputLines[r].ToCharArray().ElementAt(c);
                }
            }

            // loop over each element and check for surrounding word matches. I'm sure there are MUCH better and less labourious ways of doing this...
            // it's also a lot slower than my above approach. 
            int totalMatches = 0;

            for (int r = 0;  r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    try
                    {
                        char[] chars = new char[4];
                        chars[0] = grid[r, c]; chars[1] = grid[r - 1, c - 1]; chars[2] = grid[r - 2, c - 2]; chars[3] = grid[r - 3, c - 3];
                        if (new string(chars) == "XMAS" || new string(chars) == "SAMX") { totalMatches++; }
                    }
                    catch (Exception e) { }

                    try
                    {
                        char[] chars = new char[4];
                        chars[0] = grid[r, c]; chars[1] = grid[r, c - 1]; chars[2] = grid[r, c - 2]; chars[3] = grid[r, c - 3];
                        if (new string(chars) == "XMAS" || new string(chars) == "SAMX") { totalMatches++; }
                    }
                    catch (Exception e) { }

                    try
                    {
                        char[] chars = new char[4];
                        chars[0] = grid[r, c]; chars[1] = grid[r + 1, c - 1]; chars[2] = grid[r + 2, c - 2]; chars[3] = grid[r + 3, c - 3];
                        if (new string(chars) == "XMAS" || new string(chars) == "SAMX") { totalMatches++; }
                    }
                    catch (Exception e) { }

                    try
                    {
                        char[] chars = new char[4];
                        chars[0] = grid[r, c]; chars[1] = grid[r + 1, c]; chars[2] = grid[r + 2, c]; chars[3] = grid[r + 3, c];
                        if (new string(chars) == "XMAS" || new string(chars) == "SAMX") { totalMatches++; }
                    }
                    catch (Exception e) { }
                }
            }
            Console.WriteLine(totalMatches.ToString()); // this is also wrong... 


            // --- Part 2 ---
            totalMatches = 0;
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    int tempMatches = 0;
                    try
                    {
                        char[] chars1 = new char[3];
                        chars1[0] = grid[r, c]; chars1[1] = grid[r - 1, c - 1]; chars1[2] = grid[r + 1, c + 1];
                        if (new string(chars1) == "MAS" || new string(chars1) == "SAM") { tempMatches++; }
                    }
                    catch (Exception e) { }

                    try
                    {
                        char[] chars2 = new char[3];
                        chars2[0] = grid[r, c]; chars2[1] = grid[r + 1, c - 1]; chars2[2] = grid[r - 1, c + 1];
                        if (new string(chars2) == "MAS" || new string(chars2) == "SAM") { tempMatches++; }
                    }
                    catch (Exception e) { }

                    if (tempMatches == 2)
                    {
                        totalMatches++;
                    }
                }
            }
            Console.WriteLine(totalMatches.ToString());
            Pause();
        }

        static void Day5()
        {
            // ----- Part 1 -----
            List<string> input = System.IO.File.ReadAllLines("..\\..\\Problem Input Files\\Input 5-1.txt", System.Text.Encoding.UTF8).ToList();
            List<string[]> rules = new List<string[]>();
            List<string[]> updates = new List<string[]>();

            int runningTotal = 0;
            int runningTotal2 = 0;

            foreach (string line in input)
            {
                if (line.Contains("|"))
                { 
                    rules.Add(line.Split('|')); 
                }
                else if ( line.Contains(","))
                {
                    updates.Add(line.Split(','));
                }
            }

            foreach (string[] update in updates)
            {
                bool violated = false;
                foreach (string[] rule in rules)
                {
                    if (update.Contains(rule[0]) && update.Contains(rule[1]))
                    {
                        if (Array.IndexOf(update, rule[0]) > Array.IndexOf(update, rule[1]))
                        {
                            violated = true;
                        }
                    }
                }
                if (!violated)
                {
                    runningTotal += Int32.Parse(update[update.Length / 2]);
                }

                else // violated
                {
                    // reorder in accordance to rules
                    continue;

                    // add middle value to running total 2
                }
            }
            Console.WriteLine(runningTotal.ToString());
            Pause();

            // ----- Part 1 -----
            // would be more efficient to 
        }

        static void Main(string[] args)
        {
            // Each day is it's own function; name takes the format Day<dayNumber>().
            // Part 1 & 2 of each day are contained within the same function.

            // Each input file follows the naming convention 'Input-<dayNumber>-<part>.txt'

            // Day1();
            // Day2();
            // Day3();
            // Day4();
            Day5();
        }
    }
}
