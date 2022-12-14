
using System.Text.RegularExpressions;

var runner = new PuzzleRunner();
runner.Run();


class PuzzleRunner
{
    public void Run()
    {
        Console.WriteLine("read csv ");

        using (var reader = new StreamReader("input.yml"))
        {
            var input = reader.ReadToEnd();
            var monkeyList = ParseMonkeys(input);

            for (var i = 0; i < 20; i++)
            {
                makeRound(monkeyList, w => w/3);
            }
            var result = monkeyList
            .OrderByDescending(monkey => monkey.inspectedItems)
            .Take(2).Aggregate(1, (res, monkey) => res * monkey.inspectedItems);

            Console.WriteLine($"result : {result}");
            // Console.WriteLine($"result Part 2: {resultPart2.Sum()}");
        }

    }

    private void makeRound(Monkey[] monkeyList, Func<long, long> updateWorryLevel)
    {
        foreach (var monkey in monkeyList)
        {
            while (monkey.items.Any())
            {
                monkey.inspectedItems++;
                var item = monkey.items.Dequeue();
                item = monkey.operation(item);
                item = updateWorryLevel(item);
                var index = (item % monkey.mod == 0) ? monkey.monkeyIndexTrue : monkey.monkeyIndexFalse;
                monkeyList[index].items.Enqueue(item);
            }
        }
        
    }


    Monkey[] ParseMonkeys(string input) =>
        input.Split("\r\n\r\n").Select(ParseMonkey).ToArray();

    Monkey ParseMonkey(string input) {
        var monkey = new Monkey();
        var monkeyDescription = input.Split("\r\n");
        // // monkey id
        // var match = Regex.Match(monkeyDescription[0], @"Monkey (?<monkeyId>\d+)");
        // if (m.Success) {
        //     var monkeyId = match.Groups["monkeyId"];
        // }
        // Starting items
        var startingItems = parseLine(monkeyDescription[1], @"Starting items: ([\d+, ]*)");
        if (!string.IsNullOrEmpty(startingItems))
        {
            monkey.items = new Queue<long>(startingItems.Split(", ").Select(long.Parse));
        }
        // operation
        var operation = parseLine(monkeyDescription[2], @"Operation: new = (.*)");
        if (!string.IsNullOrEmpty(operation))
        {
            var splitAdd = operation.Split("+");
            if (splitAdd.Count() == 2)
            {
                monkey.operation = old => old + int.Parse(splitAdd[1]);
            }
            else
            {
                var splitMultiply = operation.Split("*");
                if (splitMultiply[1] == " old")
                {
                    monkey.operation = old => old * old;
                }
                else
                {
                    monkey.operation = old => old * int.Parse(splitMultiply[1]);
                }

            }
        }
        // Test
        var test = parseLine(monkeyDescription[3], @"Test: divisible by (\d+)");
        if (!string.IsNullOrEmpty(test))
        {
            monkey.mod = int.Parse(test);
        }
        // monkeyIndexTrue
        var monkeyIndexTrue = parseLine(monkeyDescription[4], @"If true: throw to monkey (\d+)");
        if (!string.IsNullOrEmpty(monkeyIndexTrue))
        {
            monkey.monkeyIndexTrue = int.Parse(monkeyIndexTrue);
        }
        // monkeyIndexFalse
        var monkeyIndexFalse = parseLine(monkeyDescription[5], @"If false: throw to monkey (\d+)");
        if (!string.IsNullOrEmpty(monkeyIndexFalse))
        {
            monkey.monkeyIndexFalse = int.Parse(monkeyIndexFalse);
        }

        return monkey;
    }

    private string parseLine(string line, string pattern)
    {
        var match = Regex.Match(line, pattern);
        if (match.Success) {
            return match.Groups[match.Groups.Count - 1].Value; // all the value wanted are at the end of the line
        }
        return "";
    }
}


class Monkey
{
    public Queue<long> items;
    public Func<long, long> operation;
    public int inspectedItems;
    public int mod;
    public int monkeyIndexTrue;
    public int monkeyIndexFalse;
}