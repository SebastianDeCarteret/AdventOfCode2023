using System.Text.RegularExpressions;
// 535235 - correct
namespace Day3
{
    public class GearRatios
    {

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

        public static int SumOfPartNumbers(string[] data)
        {
            string[] specialChars = ["*", "@", "#", "&", "=", "/", "-", "+", "%", "$"];
            string[] regexEscapeChars = ["*", ".", "+", "$", "/"]; // these chars are used to determine whether `\\` needs to be added to the regex

            int lineIndex = 0;
            int indexOfCurrentNumber = 0;

            int sum = 0;

            List<List<string>> foundNumbers = []; // [each line][single number]

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
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in current line");
                                    sum += int.Parse(currentNumberStripped);
                                }
                            }
                        }


                        // test if special characters are around current number in previous line
                        if (previousLine != null && indexOfCurrentNumber + currentNumberStripped.Length <= currentLine?.Length)
                        {
                            char[] valuesToTest = previousLine.Substring(boxStart, boxLength).ToCharArray();
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in previous line");
                                    sum += int.Parse(currentNumberStripped);
                                }
                            }
                        }

                        // test if special characters are around current number in next line line
                        if (nextLine != null && indexOfCurrentNumber + currentNumberStripped.Length <= currentLine?.Length)
                        {
                            char[] valuesToTest = nextLine.Substring(boxStart, boxLength).ToCharArray();
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in next line");
                                    sum += int.Parse(currentNumberStripped);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Error
                        Console.WriteLine($"i = {i} num = {currentNumber} | not in the current line with index: {i}");
                    }
                }

            }
            return sum;
        }
    }
}