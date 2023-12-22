using System.Text.RegularExpressions;

namespace Day2
{
    public class Game
    {
        public int Id;
        public int? BlueHighestValue;
        public int? RedHighestValue;
        public int? GreenHighestValue;

        public int GetIdFromString(string s)
        {
            int match = int.Parse(Regex.Match(Regex.Match(s, "Game \\d+").Value, "\\d+").Value);
            return match;
        }
        public int? GetHighestValueForColour(string colour, string line, int maxValue)
        {
            if (line.Contains(colour))
            {
                var matches = Regex.Matches(line, $"\\d+ {colour}").ToList();
                List<int> temp = new List<int>();
                foreach (var match in matches)
                {
                    temp.Add(int.Parse(Regex.Match(match.Value, "\\d+").Value));

                }

                if (temp.Max() > maxValue)
                {
                    return null;
                }
                else
                {
                    return temp.Max();
                }
            }
            return null;
        }
        public Game(string s1)
        {
            Id = GetIdFromString(s1);
            BlueHighestValue = GetHighestValueForColour("blue", s1, 14);
            GreenHighestValue = GetHighestValueForColour("green", s1, 13);
            RedHighestValue = GetHighestValueForColour("red", s1, 12);
        }

    }
    public class CubeConundrum
    {
        public static void Main()
        {
            try
            {
                using (var sr = new StreamReader("C:\\Users\\sdecarteret\\Desktop\\code\\C#\\AdventOfCode2023\\Day2\\2data.txt"))
                {
                    string[] data = sr.ReadToEnd().Split("\r");

                    List<Game> games = new List<Game>();

                    int Sum = 0;

                    foreach (var line in data)
                    {
                        var game = new Game(line);
                        if (game.GreenHighestValue != null && game.RedHighestValue != null && game.BlueHighestValue != null)
                        {
                            games.Add(game);
                        }
                    }

                    foreach (var game in games)
                    {
                        Sum += (int)game.Id;
                    }

                    Console.WriteLine(Sum);
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