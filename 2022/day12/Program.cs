
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
            var map = new List<List<Square>>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    map.Add(createMap(line));
                }
            }
            var (xStart, yStart) = findFirst(map);
            if (findPath(map, xStart, yStart))
                Console.WriteLine($"trouvé");
            else
                Console.WriteLine($"Noooooooooo");


            var result = map.Sum(line => line.Sum(s => s.visited ? 1 : 0));
            Console.WriteLine($"result : {result}");
            // Console.WriteLine($"result Part 2: {resultPart2.Sum()}");
        }

    }

    private List<Square> createMap(string line)
    {
        var lineMap = new List<Square>();

        foreach (var height in line)
        {
            lineMap.Add(new Square
            {
                height = height.ToString()
            });
        }

        return lineMap;
    }

    private (int, int) findFirst(List<List<Square>> map)
    {
        for (var i = 0; i < map.Count(); i++)
        {
            for (var j = 0; j < map[i].Count(); j++)
            {
                if (map[i][j].height == "S") return (i, j);
            }
        }
        return (0, 0);
    }

    private bool findPath(List<List<Square>> map, int xStart, int yStart)
    {

        var found = false;
        var i = 0;
        var x = xStart;
        var y = yStart;

        var path = new Queue<(int, int)>();
        path.Enqueue((x, y));
        while (!found && i < 100000)
        {
            if (map[x][y].height == "E") found = true;
            map[x][y].visited = true;
            (x, y) = nextPosition(map, x, y, path);
            if (x != -1 && y != -1)
                path.Enqueue((x, y));
            else 
                (x, y) = path.Dequeue();

            i++;
        }

        return found;
    }

    private (int, int) nextPosition(List<List<Square>> map, int x, int y, Queue<(int, int)> path)
    {
        if (x + 1 < map.Count() && canMove(map[x][y].height, map[x + 1][y].height) == 1 && !path.Contains((x+1, y)))
        {
            // map[x][y].visited = true;
            // map[x][y].move = ">";
            return (x+1, y);
        }
        else if (x - 1 >= 0 && canMove(map[x][y].height, map[x - 1][y].height) == 1 && !path.Contains((x-1, y)))
        {
            // map[x][y].visited = true;
            // map[x][y].move = "<";
            return (x-1, y);
        }
        else if (y + 1 < map[x].Count() && canMove(map[x][y].height, map[x][y + 1].height) == 1 && !path.Contains((x, y+1)))
        {
            // map[x][y].visited = true;
            // map[x][y].move = "^";
            return (x, y+1);
        }
        else if (y - 1 >= 0 && canMove(map[x][y].height, map[x][y - 1].height) == 1 && !path.Contains((x, y-1)))
        {
            // map[x][y].visited = true;
            // map[x][y].move = "v";
            return (x, y-1);
        }
        else if (x + 1 < map.Count() && canMove(map[x][y].height, map[x + 1][y].height) == 0 && !path.Contains((x+1, y)))
        {
            // map[x][y].visited = true;
            // map[x][y].move = ">";
            return (x+1, y);
        }
        else if (x - 1 >= 0 && canMove(map[x][y].height, map[x - 1][y].height) == 0 && !path.Contains((x-1, y)))
        {
            // map[x][y].visited = true;
            // map[x][y].move = "<";
            return (x-1, y);
        }
        else if (y + 1 < map[x].Count() && canMove(map[x][y].height, map[x][y + 1].height) == 0 && !path.Contains((x, y+1)))
        {
            // map[x][y].visited = true;
            // map[x][y].move = "^";
            return (x, y+1);
        }
        else if (y - 1 >= 0 && canMove(map[x][y].height, map[x][y - 1].height) == 0 && !path.Contains((x, y-1)))
        {
            // map[x][y].visited = true;
            // map[x][y].move = "v";
            return (x, y-1);
        }
        return (-1, -1);
    }


    private int canMove(string heightStart, string heightWanted)
    {
        if (heightStart == "S") heightStart = "a";
        if (heightWanted == "E") heightWanted = "z";
        return (int)heightWanted[0] - (int)heightStart[0];
    }

    private int SecondPuzzle(string line)
    {
        var score = 0;

        return score;
    }
}

class Square
{
    public string height { get; set; } = "a";

    public bool visited { get; set; } = false;
    public string? move { get; set; }
}