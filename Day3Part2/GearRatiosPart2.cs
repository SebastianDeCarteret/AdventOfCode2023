using System.Text.RegularExpressions;
// 535235 - correct
namespace Day3Part2
{
    public class GearRatiosPart2
    {
        public class NumberWithAsteriskIndex(int value, int asteriskIndex, int asteriskLineIndex)
        {
            public int Value { get; set; } = value;
            public int AsteriskIndex { get; set; } = asteriskIndex;
            public int AsteriskLineIndex { get; set; } = asteriskLineIndex;
        }

        public static void Main()
        {
            string[] data = GetData();
            Console.WriteLine(SumOfPartNumbers(data));
        }

        public static string[] GetData()
        {


            try
            {
                Directory.GetCurrentDirectory();
                //using (var sr = new StreamReader("3dataP2.txt"))
                using (var sr = new StreamReader("testP2.txt"))
                {
                    string[] data = sr.ReadToEnd().Split("\r\n");

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

        public static int SumOfPartNumbers(string[] data)
        {
            string[] specialChars = ["*"];
            string[] regexEscapeChars = ["*", ".", "+", "$", "/"]; // these chars are used to determine whether `\\` needs to be added to the regex

            int lineIndex = 0;
            int indexOfCurrentNumber = 0;

            int sum = 0;

            List<List<string>> foundNumbers = []; // [each line][single number]
            List<NumberWithAsteriskIndex> matchedNumbers = [];

            foreach (var line in data)
            {
                List<string> numbersTemp = new List<string>();
                foreach (var number in Regex.Matches(line, "([^\\d\\s\\r]?\\d+)").ToList())
                {
                    numbersTemp.Add(number.Value);
                }
                foundNumbers.Add(numbersTemp);

            }
            for (int i = 0; i < data.Length; i++)// for each line
            {
                List<string> individualLineNumbers = foundNumbers[i];

                foreach (var currentNumber in individualLineNumbers)
                {

                    string currentLine = data[i];
                    string? previousLine = i - 1 >= 0 ? data[i - 1] : null;
                    string? nextLine = i + 1 < data.Length ? data[i + 1] : null;

                    string regexEscape = regexEscapeChars.Contains(currentNumber[0].ToString()) ? "\\" : "";
                    string regexEscapeEnd = regexEscapeChars.Contains(currentNumber.Last().ToString()) ? "\\" : "";

                    string currentNumberStripped = Regex.Match(currentNumber, "\\d+").Value;
                    int offset = currentNumberStripped.Length < currentNumber.Length ? 1 : 0;
                    indexOfCurrentNumber = Regex.Match(currentLine, regexEscape + currentNumber + regexEscapeEnd + "($|\\D)").Index + offset; // apply offset if first char is not a number



                    if (currentLine.Contains(currentNumberStripped)) // check if the number is in the current line
                    {
                        int boxStart = indexOfCurrentNumber - 1 >= 0 ? indexOfCurrentNumber - 1 : 0;
                        int boxLength = boxStart + currentNumber.Length + 1 > currentLine.Length - 1 ? currentNumber.Length : currentNumber.Length + 1;

                        // test if special character is next to current number in current line
                        if (currentLine != null && indexOfCurrentNumber + currentNumberStripped.Length <= currentLine.Length)
                        {
                            char[] valuesToTest = currentLine.Substring(boxStart, boxLength).ToCharArray();
                            int indexOfSubstring = currentLine.IndexOf(currentLine.Substring(boxStart, boxLength));
                            int indexOfAsterisk = currentLine.Substring(boxStart, boxLength).IndexOf("*") + indexOfSubstring;
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in current line");
                                    matchedNumbers.Add(new NumberWithAsteriskIndex(int.Parse(currentNumberStripped), indexOfAsterisk, i));
                                    //sum += int.Parse(currentNumberStripped);
                                }
                            }
                        }


                        // test if special characters are around current number in previous line
                        if (previousLine != null && indexOfCurrentNumber + currentNumberStripped.Length <= currentLine?.Length)
                        {
                            char[] valuesToTest = previousLine.Substring(boxStart, boxLength).ToCharArray();
                            int indexOfSubstring = previousLine.IndexOf(previousLine.Substring(boxStart, boxLength));
                            int indexOfAsterisk = previousLine.Substring(boxStart, boxLength).IndexOf("*") + indexOfSubstring;
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in previous line");
                                    matchedNumbers.Add(new NumberWithAsteriskIndex(int.Parse(currentNumberStripped), indexOfAsterisk, i - 1));
                                    //matchedNumbers.Add(currentNumberStrkipped.ToString());
                                    //sum += int.Parse(currentNumberStripped);
                                }
                            }
                        }

                        // test if special characters are around current number in next line line
                        if (nextLine != null && indexOfCurrentNumber + currentNumberStripped.Length <= currentLine?.Length)
                        {
                            char[] valuesToTest = nextLine.Substring(boxStart, boxLength).ToCharArray();
                            int indexOfSubstring = nextLine.IndexOf(nextLine.Substring(boxStart, boxLength));
                            int indexOfAsterisk = nextLine.Substring(boxStart, boxLength).IndexOf("*") + indexOfSubstring;
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in next line");
                                    matchedNumbers.Add(new NumberWithAsteriskIndex(int.Parse(currentNumberStripped), indexOfAsterisk, i + 1));
                                    //matchedNumbers.Add(currentNumberStripped.ToString());
                                    //sum += int.Parse(currentNumberStripped);
                                }
                            }
                        }

                        //if (matchedNumbers.Count == 2)
                        //{
                        //    // multiply numbers
                        //    sum += matchedNumbers.Aggregate(1, (a, b) => int.Parse(a.ToString()) * int.Parse(b.ToString()));
                        //    matchedNumbers = [];

                        //}
                        //else
                        //{
                        //    Console.WriteLine($"i = {i} | matchedNumbers.Count = {matchedNumbers.Count}");
                        //    //matchedNumbers = [];

                        //}
                        //matchedNumbers.Clear();
                        //matchedNumbers = [];
                    }
                    else
                    {
                        // Error
                        Console.WriteLine($"i = {i} num = {currentNumber} | not in the current line with index: {i}");
                    }
                }

            }

            var matches = matchedNumbers.FindAll(number => number.AsteriskIndex == number.AsteriskIndex);

            return sum;
        }
    }
}