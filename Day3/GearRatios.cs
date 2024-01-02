using System.Text.RegularExpressions;
// 532136 - too low
namespace Day3
{
    public class GearRatios
    {

        public static void Main()
        {
            string[] specialChars = ["*", "@", "#", "&", "=", "/", "-", "+", "%", "$"];

            string[] data = GetData();

            int sum = 0;

            int lineIndex = 0;

            List<List<string>> foundNumbers = []; // [each line][single number]

            foreach (var line in data)
            {
                //Console.WriteLine(line.Length);
                List<string> numbersTemp = new List<string>();
                foreach (var number in Regex.Matches(line, "\\d+").ToList())
                {
                    numbersTemp.Add(number.Value);
                }
                foundNumbers.Add(numbersTemp);

            }
            int indexOfCurrentNumber = 0;
            for (int i = 11; i < data.Length; i++)// for each line
            {
                List<string> individualLineNumbers = foundNumbers[i];

                foreach (var currentNumber in individualLineNumbers)
                {
                    string currentLine = data[i];
                    string? previousLine = i - 1 >= 0 ? data[i - 1] : null;
                    string? nextLine = i + 1 < data.Length ? data[i + 1] : null;
                    indexOfCurrentNumber = currentLine.IndexOf(currentNumber);

                    if (currentLine.Contains(currentNumber)) // check if the number is in the current line
                    {
                        if (specialChars.Contains(currentLine[indexOfCurrentNumber - 1 >= 0 ? indexOfCurrentNumber - 1 : 0].ToString()))// check adjacent, start, same line
                        {
                            sum += int.Parse(currentNumber);
                        }
                        if (indexOfCurrentNumber + (currentNumber.Length + 1) < currentLine.Length)// check adjacent, end, same line
                        {
                            if (specialChars.Contains(currentLine[(indexOfCurrentNumber - 1) + (currentNumber.Length + 1)].ToString()))
                            {
                                sum += int.Parse(currentNumber);
                            }
                        }
                        if (previousLine != null && indexOfCurrentNumber + (currentNumber.Length + 1) < currentLine.Length)
                        {
                            char[] valuesToTest = previousLine.Substring(indexOfCurrentNumber - 1 >= 0 ? indexOfCurrentNumber - 1 : 0, currentNumber.Length + 2).ToCharArray();
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in previous line");
                                    sum += int.Parse(currentNumber);
                                }
                            }
                        }
                        if (nextLine != null && indexOfCurrentNumber + (currentNumber.Length + 1) < currentLine.Length)
                        {
                            char[] valuesToTest = nextLine.Substring((indexOfCurrentNumber - 1 >= 0 ? indexOfCurrentNumber - 1 : 0), currentNumber.Length + 2).ToCharArray();
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} num = {currentNumber} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in next line");
                                    sum += int.Parse(currentNumber);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"not in the current line with index: {i}");
                    }
                }

            }
            Console.WriteLine($"sum: {sum}");

        }

        public static string[] GetData()
        {


            try
            {
                using (var sr = new StreamReader("C:\\Users\\sdecarteret\\Desktop\\code\\C#\\AdventOfCode2023\\Day3\\3data.txt"))
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