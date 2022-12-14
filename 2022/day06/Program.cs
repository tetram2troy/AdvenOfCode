
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
        var score = 1;
        var found = string.Empty;
        foreach (var car in line)
        {
            if (!found.Contains(car)) found += car.ToString();
            else found = car.ToString();
            if (found.Length == 4) break;
            score ++;

        }
        return score;
    }

    private int SecondPuzzle(string line)
    {
        var score = 1;
        var found = string.Empty;
        foreach (var car in line)
        {
            if (!found.Contains(car)) found += car.ToString();
            else 
            {
                Console.WriteLine($"reinit : {found} - {found.Length}");
                found = car.ToString();
            }
            if (found.Length == 13) break;
            score ++;

        }
        return score;
    }
}
