using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day2
{
    public class Game
    {
        public int Id;

        public int? BlueHighestValue;
        public int? RedHighestValue;
        public int? GreenHighestValue;

        public int? BlueHighestPlayedValue;
        public int? RedHighestPlayedValue;
        public int? GreenHighestPlayedValue;

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

        public int? GetHighestPlayedValueForColour(string colour, string line)
        {
            if (line.Contains(colour))
            {
                var matches = Regex.Matches(line, $"\\d+ {colour}").ToList();
                List<int> temp = new List<int>();
                foreach (var match in matches)
                {
                    temp.Add(int.Parse(Regex.Match(match.Value, "\\d+").Value));

                }
                return temp.Max();

            }
            return null;
        }
        public Game(string s1)
        {
            Id = GetIdFromString(s1);

            BlueHighestValue = GetHighestValueForColour("blue", s1, 14);
            GreenHighestValue = GetHighestValueForColour("green", s1, 13);
            RedHighestValue = GetHighestValueForColour("red", s1, 12);

            BlueHighestPlayedValue = GetHighestPlayedValueForColour("blue", s1);
            GreenHighestPlayedValue = GetHighestPlayedValueForColour("green", s1);
            RedHighestPlayedValue = GetHighestPlayedValueForColour("red", s1);
        }

    }
    public class CubeConundrum
    {
        public static void Main()
        {
            string[] data = GetData();

            Console.WriteLine($"Sum of valid game id's:{Sum(data)}");
            Console.WriteLine($"Sum of powers of each game's minumum cards played:{Powers(data)}");
        }

        public static string[] GetData()
        {
            try
            {
                using (var sr = new StreamReader("C:\\Users\\sdecarteret\\Desktop\\code\\C#\\AdventOfCode2023\\Day2\\2data.txt"))
                {
                    string[] data = sr.ReadToEnd().Split("\r");

                    return data;



                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static int Sum(string[] data)
        {
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

            return Sum;
        }

        public static int Powers(string[] data)
        {
            List<Game> games = new List<Game>();

            int Sum = 0;

            foreach (var line in data)
            {
                var game = new Game(line);
                if (game.GreenHighestPlayedValue != null && game.RedHighestPlayedValue != null && game.BlueHighestPlayedValue != null)
                {
                    games.Add(game);
                }
            }

            foreach (var game in games)
            {
                int power = (int)(game.BlueHighestPlayedValue * game.RedHighestPlayedValue * game.GreenHighestPlayedValue);
                Sum += power;
            }

            return Sum;
        }
    }
}