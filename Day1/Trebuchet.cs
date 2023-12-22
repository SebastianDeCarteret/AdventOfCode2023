using System.Text.RegularExpressions;

namespace Day1
{
    public class Trebuchet
    {
        public static void Main()
        {
            var conversionTable = new Dictionary<string, int>()
            {
                {  "one", 1},
                {  "two", 2},
                {  "three", 3},
                {  "four", 4},
                {  "five", 5},
                {  "six", 6},
                {  "seven", 7},
                {  "eight", 8},
                {  "nine", 9},
                {  "1", 1},
                {  "2", 2},
                {  "3", 3},
                {  "4", 4},
                {  "5", 5},
                {  "6", 6},
                {  "7", 7},
                {  "8", 8},
                {  "9", 9}
            };
            try
            {
                using (var sr = new StreamReader("C:\\Users\\sdecarteret\\Desktop\\code\\C#\\AdventOfCode2023\\Day1\\1data.txt"))
                {
                    string[] data = sr.ReadToEnd().Split("\r");
                    List<int> matchesList = new List<int>();
                    foreach (string line in data)
                    {
                        var matches = Regex.Matches(line, "one|two|three|four|five|six|seven|eight|nine|\\d", RegexOptions.IgnoreCase);
                        if (matches.Count == 1)
                        {
                            matchesList.Add(conversionTable[matches.First().Value] * 10);
                            matchesList.Add(conversionTable[matches.First().Value]);

                        }
                        else if (matches.Count > 1)
                        {
                            matchesList.Add(conversionTable[matches.First().Value] * 10);
                            matchesList.Add(conversionTable[matches.Last().Value]);

                        }

                    }
                    int total = matchesList.Sum();
                    Console.WriteLine($"Total:\n{total}");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
