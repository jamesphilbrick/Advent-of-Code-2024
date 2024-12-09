using System;
using System.Collections.Generic;

namespace AdventOfCode2024
{
    internal static class HelperFunctions
    {
        public static string CharGridToString(List<List<char>> charGrid)
        {
            string result = string.Empty;
            foreach (List<char> row in charGrid)
            {
                string rowString = new string(row.ToArray());
                result += rowString + ",";
            }
            return result;
        }

        public static void DisplayGrid(char[,] grid)
        {
            //System.Threading.Thread.Sleep(300);
            Console.Clear();
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    Console.Write(grid[r, c]);
                }
                Console.Write('\n');
            }
            Console.Write('\n');
        }
    }
}
