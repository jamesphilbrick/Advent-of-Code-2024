using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
