
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
            var ship = new List<Stack<string>>();
            var ship2 = new List<List<string>>();
			var initFinish = false;
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
					if (initFinish)
					{
						FirstPuzzle(line, ship);
						SecondPuzzle(line, ship2);
					}
					else
					{
                        var i = 0;
                        var matcheList = Regex.Matches(line, @"((\[(?<crate>[A-Z])\])| {3}) ?");
                        foreach (Match match in matcheList) {
                            if (ship.ElementAtOrDefault(i) == null) {
                                ship.Add(new Stack<string>());
                            }
                            var crate = match.Groups["crate"];
                            if (crate != null && !string.IsNullOrEmpty(crate.Value))
                            {
                                ship[i].Push(crate.Value);
                            }
                            i++;
                        }
					}
                }
                else
                {
                    initFinish = true;
                    var newShip = new List<Stack<string>>();
                    // reverse all stack
                    foreach (var stack in ship) {
                        var newStack = new Stack<string>();
                        var newList = new List<string>();
                        while (stack.Count != 0) {
                            var crate = stack.Pop();
                            newStack.Push(crate);
                            newList.Add(crate);
                        }
                        newShip.Add(newStack);
                        ship2.Add(newList);
                    }
                    ship = newShip;
                }
            }

            var result = "";
            var resultPart2 = "";
            foreach(var stack in ship) if (stack.Count() > 0) result += stack.Pop();
            foreach(var stack in ship2) if (stack.Count() > 0) resultPart2 += stack[stack.Count()-1];
            Console.WriteLine($"result : {result}");
            Console.WriteLine($"result Part 2: {resultPart2}");
        }

    }

    private void FirstPuzzle(string line, List<Stack<string>> ship)
    {
        var actionMatchList = Regex.Matches(line, @"move (?<howMany>[0-9]{1,2}) from (?<from>[0-9]{1,2}) to (?<to>[0-9]{1,2})");
        if (actionMatchList.Count() == 1)
        {
            var action = actionMatchList[0];
            var howMany = Int32.Parse(action.Groups["howMany"].Value);
            var from = Int32.Parse(action.Groups["from"].Value);
            var to = Int32.Parse(action.Groups["to"].Value);

            for (var i = 0; i < howMany; i++)
            {
                if (ship[from - 1].Count() > 0)
                    ship[to - 1].Push(ship[from - 1].Pop());
            }
        }
    }

    private void SecondPuzzle(string line, List<List<string>> ship2)
    {
        var actionMatchList = Regex.Matches(line, @"move (?<howMany>[0-9]{1,2}) from (?<from>[0-9]{1,2}) to (?<to>[0-9]{1,2})");
        if (actionMatchList.Count() == 1)
        {
            var action = actionMatchList[0];
            var howMany = Int32.Parse(action.Groups["howMany"].Value);
            var from = Int32.Parse(action.Groups["from"].Value);
            var to = Int32.Parse(action.Groups["to"].Value);
            
            if (ship2[from - 1].Count() <= howMany)
            {
                howMany = ship2[from - 1].Count();
            }
            ship2[to - 1].AddRange(ship2[from - 1].GetRange(ship2[from - 1].Count() - howMany, howMany));
            ship2[from - 1].RemoveRange(ship2[from - 1].Count() - howMany, howMany);
        }
    }
}
