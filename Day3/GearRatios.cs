using System.Text.RegularExpressions;

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
            for (int i = 1; i < data.Length; i++)// for each line
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
                        //int indexOfCurrentNumber = 0;
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
                        if (previousLine != null)
                        {
                            char[] valuesToTest = previousLine.Substring(indexOfCurrentNumber - 1 >= 0 ? indexOfCurrentNumber - 1 : 0, currentNumber.Length + 2).ToCharArray();
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in previous line");
                                    sum += int.Parse(currentNumber);
                                }
                            }
                        }
                        //else
                        //{
                        //    Console.WriteLine($"{currentNumber} is not in the currnt line with index: {i}");
                        //}
                        if (nextLine != null)
                        {
                            char[] valuesToTest = nextLine.Substring((indexOfCurrentNumber - 1 >= 0 ? indexOfCurrentNumber - 1 : 0), currentNumber.Length + 2).ToCharArray();
                            foreach (var value in valuesToTest)
                            {
                                if (specialChars.Contains(value.ToString()))
                                {
                                    Console.WriteLine($"i = {i} | substring of [{string.Join(", ", valuesToTest)}] contains special charatcer in next line");
                                    sum += int.Parse(currentNumber);
                                }
                            }
                        }
                        //else
                        //{
                        //    Console.WriteLine($"{currentNumber} is not in the currnt line with index: {i}");
                        //}
                    }
                    else
                    {
                        Console.WriteLine($"not in the current line with index: {i}");
                    }
                }

            }


            //foreach (var lineNumbers in foundNumbers)
            //{
            //    foreach (var number in lineNumbers)
            //    {
            //        int indexOfNumber = data[lineIndex].IndexOf(number);

            //        string currentLine = data[lineIndex];
            //        //string previousLine = data[lineIndex];
            //        //string nextLine = data[lineIndex];

            //        if (indexOfNumber != -1)
            //        {
            //            if (specialChars.Contains(currentLine[indexOfNumber].ToString()))
            //            {
            //                sum = sum + int.Parse(number);
            //                //sum += int.Parse(number);
            //            }
            //            if (specialChars.Contains(currentLine[indexOfNumber + (number.Length + 1)].ToString()))
            //            {
            //                sum += int.Parse(number);
            //            }
            //            lineIndex++;
            //            //if (specialChars.Contains(nextLine[indexOfNumber - 1].ToString()))
            //            //{
            //            //    sum += int.Parse(number);
            //            //}
            //            //if (specialChars.Contains(nextLine[indexOfNumber + (number.Length + 1)].ToString()))
            //            //{
            //            //    sum += int.Parse(number);
            //            //}
            //        }

            //        Console.WriteLine(number);
            //    }
            //}

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