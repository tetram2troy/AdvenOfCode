

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
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    result.Add(SecondPuzzle(line));
                }
            }

            Console.WriteLine($"result : {result.Sum()}");
        }

    }

    private int FirstPuzzle(string line)
    {
        var scorePoint = new Dictionary<string, int>(){
                {"X", 1},
                {"Y", 2},
                {"Z", 3},
            };
        var mapMove = new Dictionary<string, string>(){
                {"X", "A"},
                {"Y", "B"},
                {"Z", "C"},
            };
        var winAgainstMove = new Dictionary<string, string>(){
                {"A", "C"},
                {"B", "A"},
                {"C", "B"},
            };


        var moveList = line.Split(' ');
        var oponenMove = moveList[0];
        var myMove = moveList[1];
        var score = 0;
        if (winAgainstMove[mapMove[myMove]] == oponenMove) score += 6;
        else if (mapMove[myMove] == oponenMove) score += 3;

        return scorePoint[myMove] + score;
    }

    private int SecondPuzzle(string line)
    {
        var scorePoint = new Dictionary<string, int>(){
                {"A", 1},
                {"B", 2},
                {"C", 3},
            };
        var mapMove = new Dictionary<string, string>(){
                {"X", "A"}, // lose
                {"Y", "B"}, // draw
                {"Z", "C"}, // win
            };
        var moveToWinAgainst = new Dictionary<string, string>(){
                {"A", "B"},
                {"B", "C"},
                {"C", "A"},
            };
        var winAgainstMove = new Dictionary<string, string>(){
                {"A", "C"},
                {"B", "A"},
                {"C", "B"},
            };

        var moveList = line.Split(' ');
        var oponenMove = moveList[0];
        var resultExpected = moveList[1];
        var score = 0;
        if (resultExpected == "Z") score += 6 + scorePoint[moveToWinAgainst[oponenMove]];
        else if (resultExpected == "Y") score += 3 + scorePoint[oponenMove];
        else score += scorePoint[winAgainstMove[oponenMove]];


        return score;

    }
}
