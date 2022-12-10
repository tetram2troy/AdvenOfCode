
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
            var root = new NTree<Folder>(new Folder(){name = "/"});
            var currentNode = root;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    if (currentNode != null)
                        currentNode = BuildTree(line, currentNode);
                }
            }
            Console.WriteLine($"debug1");
            long result = 0;
            root.Traverse(fold => {
                if (fold.size <= 100000) result += fold.size; 
                });
            Console.WriteLine($"result : {result}");

            var totalSize   = 70000000;
            var spaceNeeded = 30000000;
            var freeSpace = totalSize - root.data.size;

            var possibleSizeList = new List<long>();
            root.Traverse(fold => {
                if (freeSpace + fold.size >= spaceNeeded) possibleSizeList.Add(fold.size); 
                });

            Console.WriteLine($"result Part 2: {possibleSizeList.Min()}");
        }

    }

    private NTree<Folder>? BuildTree(string line, NTree<Folder> node)
    {
        var currentNode = node;

        if (line == "$ cd /") return node;
        var cmdSplit = line.Split(' ');
        if (cmdSplit[0] == "$") // it's a commande
        {
            switch (cmdSplit[1])
            {
                case "ls": break; //do nothing
                case "cd": 
                    if (cmdSplit[2] == "..") {
                        return node.parent;
                    }
                    else
                    {
                        return node.FindFirstChild(folder => folder.name == cmdSplit[2]);
                    }
                
                default: Console.WriteLine($"cmd not known"); break;
            }
        }
        else
        {
            if (cmdSplit[0] == "dir")
            {
                currentNode?.AddChild(new Folder(){name = cmdSplit[1]});
            }
            else
            {
                long size = long.Parse(cmdSplit[0]);
                currentNode.TraverseAsc(folder => folder.size += size);
            }
        }
        return currentNode;
    }
}

class Folder
{
    public string name { get; set; } = string.Empty;
    public long size { get; set; } = 0;
}

delegate void TreeVisitor<T>(T nodeData);

class NTree<T>
{
    public T data;
    private LinkedList<NTree<T>> children;

    public NTree<T>? parent;

    public NTree(T data) : this(data, null) {}
    public NTree(T data, NTree<T>? parent)
    {
        this.parent = parent;
        this.data = data;
        children = new LinkedList<NTree<T>>();
    }

    public void AddChild(T data)
    {
        children.AddFirst(new NTree<T>(data, this));
    }

    public NTree<T>? FindFirstChild(Func<T, bool> predicate)
    {
        foreach (NTree<T> n in children)
            if (predicate(n.data))
                return n;
        return null;
    }
    public NTree<T>? GetChild(int i)
    {
        foreach (NTree<T> n in children)
            if (--i == 0)
                return n;
        return null;
    }

    public void TraverseAsc(TreeVisitor<T> visitor)
    {
        visitor(data);
        if (parent != null)
            parent.TraverseAsc(visitor);
    }

    public void Traverse( TreeVisitor<T> visitor)
    {
        visitor(data);
        foreach (NTree<T> kid in children)
            kid.Traverse(visitor);
    }
}
