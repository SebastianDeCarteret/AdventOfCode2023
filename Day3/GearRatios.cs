using System.Text.RegularExpressions;
// 532136 - too low
// 535235 - correct
namespace Day3
{
    public class GearRatios
    {

        public static void Main()
        {
            string[] specialChars = ["*", "@", "#", "&", "=", "/", "-", "+", "%", "$"];

            string[] regexEscapeChars = ["*", ".", "+", "$", "/"];

            string[] data = GetData();

            int sum = 0;

            int lineIndex = 0;

            List<List<string>> foundNumbers = []; // [each line][single number]

            foreach (var line in data)
            {
                //Console.WriteLine(line.Length);
                List<string> numbersTemp = new List<string>();
                // this regex needs to be redone to allow this to be selected properly => .722.286.
                foreach (var number in Regex.Matches(line, "([^\\d\\s\\r]?\\d+)").ToList())
                {
                    numbersTemp.Add(number.Value);
                }
                foundNumbers.Add(numbersTemp);

            }
            int indexOfCurrentNumber = 0;
            for (int i = 0; i < data.Length; i++)// for each line
            {
                List<string> individualLineNumbers = foundNumbers[i];

                foreach (var currentNumber in individualLineNumbers)
                {
                    string currentNumberStripped = Regex.Match(currentNumber, "\\d+").Value;
                    string currentLine = data[i];
                    string? previousLine = i - 1 >= 0 ? data[i - 1] : null;
                    string? nextLine = i + 1 < data.Length ? data[i + 1] : null;
                    int offset = currentNumberStripped.Length < currentNumber.Length ? 1 : 0;
                    //indexOfCurrentNumber = currentLine.IndexOf(currentNumber) + offset;
                    string regexEscape = regexEscapeChars.Contains(currentNumber[0].ToString()) ? "\\" : "";
                    string regexEscapeEnd = regexEscapeChars.Contains(currentNumber.Last().ToString()) ? "\\" : "";
                    indexOfCurrentNumber = Regex.Match(currentLine, regexEscape + currentNumber + regexEscapeEnd + "($|\\D)").Index + offset;

                    if (currentLine.Contains(currentNumberStripped)) // check if the number is in the current line
                    {
                        //string currentNumberStripped = Regex.Match(currentNumber, "\\d+").Value;
                        //if (specialChars.Contains(currentNumber[0].ToString()))// check adjacent, start, same line
                        //{
                        //    Console.WriteLine($"i = {i} num = {currentNumber} | substring contains special charatcer in current line at start");
                        //    int currentNumberStripped = int.Parse(Regex.Match(currentNumber, "\\d+").Value);
                        //    sum += currentNumberStripped;
                        //}
                        ////if (indexOfCurrentNumber + (currentNumber.Length + 1) < currentLine.Length)// check adjacent, end, same line
                        ////{
                        //if (specialChars.Contains(currentNumber[currentNumber.Length - 1].ToString()))
                        //{
                        //    Console.WriteLine($"i = {i} num = {currentNumber} | substring contains special charatcer in current line at end");
                        //    int currentNumberStripped = int.Parse(Regex.Match(currentNumber, "\\d+").Value);
                        //    sum += currentNumberStripped;
                        //}
                        int boxStart = indexOfCurrentNumber - 1 >= 0 ? indexOfCurrentNumber - 1 : 0;
                        int boxLength = boxStart + currentNumber.Length + 1 > currentLine.Length - 1 ? currentNumber.Length : currentNumber.Length + 1;
                        if (currentLine != null && indexOfCurrentNumber + currentNumberStripped.Length <= currentLine.Length)
                        {
                            // needs fixing when at end of line

                            char[] valuesToTest = currentLine.Substring(boxStart, boxLength).ToCharArray();
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in current line");
                                    //int currentNumberStripped = int.Parse(Regex.Match(currentNumber, "\\d+").Value);
                                    sum += int.Parse(currentNumberStripped);
                                }
                            }
                        }
                        if (previousLine != null && indexOfCurrentNumber + currentNumberStripped.Length <= currentLine?.Length)
                        {
                            char[] valuesToTest = previousLine.Substring(boxStart, boxLength).ToCharArray();
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in previous line");
                                    //int currentNumberStripped = int.Parse(Regex.Match(currentNumber, "\\d+").Value);
                                    sum += int.Parse(currentNumberStripped);
                                }
                            }
                        }
                        if (nextLine != null && indexOfCurrentNumber + currentNumberStripped.Length <= currentLine?.Length)
                        {
                            char[] valuesToTest = nextLine.Substring(boxStart, boxLength).ToCharArray();
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in next line");
                                    //int currentNumberStripped = int.Parse(Regex.Match(currentNumber, "\\d+").Value);
                                    sum += int.Parse(currentNumberStripped);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"i = {i} num = {currentNumber} | not in the current line with index: {i}");
                    }
                }

            }
            Console.WriteLine($"sum: {sum}");

        }

        public static string[] GetData()
        {


            try
            {
                Directory.GetCurrentDirectory();
                using (var sr = new StreamReader("3data.txt"))
                //using (var sr = new StreamReader("test.txt"))
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
    }
}