// TODO - refactor and tidy up
namespace Day4Part1
{
    public class Scratchcards
    {
        public class ScratchCard(List<string> winningNumbers, List<string> myNumbers)
        {
            public List<string> WinningNumbers = winningNumbers;
            public List<string> MyNumbers = myNumbers;
        }
        public static void Main()
        {
            string[] data = GetData("4data.txt");

            List<ScratchCard> scratchCards = [];

            int sum = 0;

            foreach (var line in data)
            {
                string winningNumbers = line.Substring(line.IndexOf(":") + 1, line.IndexOf("|") - 9);
                string myNumbers = line.Substring(line.IndexOf("|") + 2);

                ScratchCard scratchCard = ExtractDataToList(winningNumbers, myNumbers);

                scratchCards.Add(scratchCard);

            }
            foreach (var scratchCard in scratchCards)
            {
                List<string> wonNumbers = [];
                int miniSum = 0;

                foreach (var scratchCardMyNumbers in scratchCard.MyNumbers)
                {
                    if (scratchCard.WinningNumbers.Contains(scratchCardMyNumbers))
                    {
                        wonNumbers.Add(scratchCardMyNumbers);
                    }
                }

                if (wonNumbers.Count > 0)
                {
                    for (int i = 1; i <= wonNumbers.Count; i++)
                    {
                        if (i == 1)
                        {
                            miniSum++;
                        }
                        else
                        {
                            miniSum *= 2;
                        }
                    }
                }
                Console.WriteLine($"miniSum = {miniSum} | sum = {sum} | winningNumbers = [{string.Join(", ", scratchCard.WinningNumbers)}] | myNumbers = [{string.Join(", ", scratchCard.MyNumbers)}]");
                sum += miniSum;
            }

            Console.WriteLine($"sum:{sum}");
        }

        public static List<string> RemoveEmptyStrings(List<string> data)
        {
            data.RemoveAll(data => data == "");
            return data;
        }

        public static ScratchCard ExtractDataToList(string winningNumbers, string myNumbers)
        {
            List<string> winningNumbersSplit = winningNumbers.Split(" ").ToList();
            List<string> myNumbersSplit = myNumbers.Split(" ").ToList();

            return new ScratchCard(RemoveEmptyStrings(winningNumbersSplit), RemoveEmptyStrings(myNumbersSplit));
        }

        public static string[] GetData(string fileNameWithExt)
        {
            try
            {
                Directory.GetCurrentDirectory();
                using (var sr = new StreamReader(fileNameWithExt))
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
