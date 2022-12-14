
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
            var ropeTail = new Rope();
            var ropeTail2 = new Rope();
            var rope1 = new List<Rope> {
                new Rope(),
                ropeTail,
            };
            var rope2 = new List<Rope> {
                new Rope(),
                new Rope(),
                new Rope(),
                new Rope(),
                new Rope(),
                new Rope(),
                new Rope(),
                new Rope(),
                new Rope(),
                ropeTail2,
            };
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    FirstPuzzle(line, rope1);
                    FirstPuzzle(line, rope2);
                }
            }

            
            Console.WriteLine($"result : {ropeTail.history.Distinct().Count()}");
            Console.WriteLine($"result Part 2: {ropeTail2.history.Distinct().Count()}");
        }

    }

    private void FirstPuzzle(string line, List<Rope> rope)
    {
        var move = line.Split(" ");
        var nbMove = Int32.Parse(move[1]);

        moveRope(move[0], nbMove, rope);
    }

    private void moveRope(string direction, int nbMove, List<Rope> rope)
    {
        for(var i = 0; i < nbMove; i++){
            switch (direction)
            {
                case "U": rope[0].move(rope[0].x, ++rope[0].y); break;
                case "D": rope[0].move(rope[0].x, --rope[0].y);break;
                case "R": rope[0].move(++rope[0].x, rope[0].y);break;
                case "L": rope[0].move(--rope[0].x, rope[0].y);break;
            }
            for (var j = 1; j < rope.Count(); j++){
                moveTail( rope[j-1],  rope[j]);
            }
        }
    }

    private void moveTail(Rope ropeHead, Rope ropeTail)
    {
        if (!AreAdjacent(ropeHead, ropeTail))
        {
            var newX = ropeTail.x;
            var newY = ropeTail.y;
            if (ropeTail.x < ropeHead.x) newX++;
            if (ropeTail.x > ropeHead.x) newX--;
            if (ropeTail.y < ropeHead.y) newY++;
            if (ropeTail.y > ropeHead.y) newY--;

            ropeTail.move(newX, newY);
        }
        
    }

    public bool AreAdjacent(Rope point1, Rope point2)
    {
        if (point1.x == point2.x && point1.y == point2.y) return true;
        double distance = Math.Round(Math.Sqrt(Math.Pow(point1.x - point2.x, 2) +
                                    Math.Pow(point1.y - point2.y, 2)));
        return distance == 1;
    }

    private int SecondPuzzle(string line)
    {
        var score = 0;

        return score;
    }
}


class Rope
{
    public int x { get; set; } = 0;
    public int y { get; set; } = 0;

    public LinkedList<(int, int)> history { get; set; } = new LinkedList<(int, int)>();

    public Rope()
    {
        history.AddLast((0,0));
    }

    public void move(int x, int y)
    {
        this.x = x;
        this.y = y;
        history.AddLast((this.x,this.y));
    }
}