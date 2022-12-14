
using System.Text.RegularExpressions;

var runner = new PuzzleRunner();
runner.Run();


class PuzzleRunner
{
    public void Run()
    {
        Console.WriteLine("read csv ");

        using (var reader = new StreamReader("input.csv"))
        {
            var result = new List<int>();
            var lineList = new List<string>();
            var resultPart2 = new List<int>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    result.Add(FirstPuzzle(line));
                    lineList.Add(line);
                    if (lineList.Count() == 3) {
                        resultPart2.Add(SecondPuzzle(lineList));
                        lineList = new List<string>();
                    }
                }
            }

            Console.WriteLine($"result : {result.Sum()}");
            Console.WriteLine($"result Part 2: {resultPart2.Sum()}");
        }

    }

    private int FirstPuzzle(string line)
    {
        var score = 0;

        var firstCompartment = line.Substring(0, (line.Length / 2) );
        var secondCompartment = line.Substring((line.Length / 2), (line.Length / 2));
        char sharedItem = 'a';

        foreach (var letter in firstCompartment)
        {
            if (secondCompartment.Contains(letter)) sharedItem = letter;
        }

        if ((Regex.Matches(sharedItem.ToString(), @"[a-z]")).Count() > 0 ) score = ((int) sharedItem) - 96;
        else score = ((int) sharedItem) - 64 + 26;
        Console.WriteLine($"letter found: {sharedItem} score : {score} | firstCompartment {firstCompartment} | secondCompartment {secondCompartment}");
        
        return score;
    }

    private int SecondPuzzle(List<string> line)
    {
        var score = 0;

        var filteredLine = line[0];
        foreach (var letter in filteredLine )
        {
            if (!line[1].Contains(letter.ToString())) filteredLine = filteredLine.Replace(letter.ToString(), string.Empty);
            if (!line[2].Contains(letter.ToString())) filteredLine = filteredLine.Replace(letter.ToString(), string.Empty);
        }

        Console.WriteLine($"letter found: {filteredLine}");
        if ((Regex.Matches(filteredLine.ToString(), @"[a-z]")).Count() > 0 ) score = ((int) filteredLine[0]) - 96;
        else score = ((int) filteredLine[0]) - 64 + 26;

        return score;
    }
}
