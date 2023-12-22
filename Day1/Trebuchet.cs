namespace Day1
{
    public class Trebuchet
    {
        public static void Main()
        {
            try
            {
                // Open the text file using a stream reader.
                using (var sr = new StreamReader("C:\\Users\\sdecarteret\\Desktop\\code\\C#\\AdventOfCode2023\\Day1\\1data.txt"))
                {
                    // Read the stream as a string, and write the string to the console.
                    //Console.WriteLine(sr.ReadToEnd());
                    string[] data = sr.ReadToEnd().Split("\r");

                    string[] numbersAsStrings =
                        [
                            "one",
                            "two",
                            "three",
                            "four",
                            "five",
                            "six",
                            "seven",
                            "eight",
                            "nine",
                        ];

                    foreach (string number in numbersAsStrings)
                    {
                        Console.WriteLine($"First: {data[0].IndexOf(number)}");
                        Console.WriteLine($"Last: {data[0].LastIndexOf(number)}");
                    }

                    Console.WriteLine(data[0]);
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
