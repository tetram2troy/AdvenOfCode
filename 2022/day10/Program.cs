
using System.Text.RegularExpressions;

var runner = new PuzzleRunner();
runner.Run();


class PuzzleRunner
{
    private int[] milestone = new int[] {20, 60, 100, 140, 180 , 220};
    private int indexMilestone = 0;
    private int x = 1;
    private int cycle = 0;
    private string resultPart2 = "";

    public void Run()
    {
        Console.WriteLine("read csv ");

        using (var reader = new StreamReader("input.csv"))
        {
            var result = new List<int>();
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    result.Add(FirstPuzzle(line));
                }
            }

            Console.WriteLine($"result : {result.Sum()}");
            Console.WriteLine($"result Part 2:");
            int stringLength = resultPart2.Length;
            List<string> splitString = new List<string>();
            for (int i = 0; i < stringLength; i += 40)
            {
                splitString.Add(resultPart2.Substring(i, Math.Min(40, stringLength - i)));
            }

            // Afficher chaque section de la chaîne de caractères
            foreach (string section in splitString)
            {
                Console.WriteLine(section);
            }
            // Console.WriteLine(resultPart2);
            
        }

    }

    private int FirstPuzzle(string line)
    {
        var score = 0;
        if (line == "noop")
        {
            cycle++;
            calcCRT();
        }
        else
        {
            cycle ++;
            calcCRT();
            cycle ++;
            calcCRT();
            if (indexMilestone < milestone.Count() && cycle >= milestone[indexMilestone])
            {
                score = x * milestone[indexMilestone];
                indexMilestone++;
                
            }
            var spitLine = line.Split(" ");
            x += int.Parse(spitLine[1]);
        }
        return score;
    }

    private void calcCRT()
    {
        if (cycle % 40 >= x && cycle % 40 <= x + 2 ) resultPart2 += "#";
        else resultPart2 += ".";
    }

}
