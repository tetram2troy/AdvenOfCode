
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
            var resultPart2 = new List<int>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    result.Add(FirstPuzzle(line));
                    resultPart2.Add(SecondPuzzle(line));
                }
            }

            Console.WriteLine($"result : {result.Sum()}");
            Console.WriteLine($"result Part 2: {resultPart2.Sum()}");
        }

    }

    private int FirstPuzzle(string line)
    {
        var score = 0;
        var elfAssignement = line.Split(',');
        var elf1 = elfAssignement[0].Split('-');
        var elf2 = elfAssignement[1].Split('-');

        var minElf1 = Int32.Parse(elf1[0]);
        var maxElf1 = Int32.Parse(elf1[1]);
        var minElf2 = Int32.Parse(elf2[0]);
        var maxElf2 = Int32.Parse(elf2[1]);
        
        if ((minElf1 <= minElf2 && maxElf1 >= maxElf2) || (minElf1 >= minElf2 && maxElf1 <= maxElf2)) score = 1;
        
        return score;
    }

    private int SecondPuzzle(string line)
    {
        var score = 0;
        var elfAssignement = line.Split(',');
        var elf1 = elfAssignement[0].Split('-');
        var elf2 = elfAssignement[1].Split('-');

        var minElf1 = Int32.Parse(elf1[0]);
        var maxElf1 = Int32.Parse(elf1[1]);
        var minElf2 = Int32.Parse(elf2[0]);
        var maxElf2 = Int32.Parse(elf2[1]);
        
        if ((minElf1 <= minElf2 && maxElf1 >= maxElf2) 
        || (minElf1 >= minElf2 && maxElf1 <= maxElf2)
        || (minElf1 <= minElf2 && maxElf1 >= minElf2)
        || (minElf1 >= minElf2 && minElf1 <= maxElf2)
        ) score = 1;

        return score;
    }
}
