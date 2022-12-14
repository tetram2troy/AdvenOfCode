
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
            var forest = new List<List<Tree>>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    forest.Add(FirstPuzzle(line, forest));
                }
            }
            var total = forest.Sum(line => line.Sum(tree => 1));

            Console.WriteLine($"total : {total}");

            var maxCol = new List<int?>();
            forest.First().ForEach(tree => maxCol.Add(-1));
            
            for(var i = forest.Count() -1; i >= 0; i--)
            {
                var forestLine = forest[i];
                var maxLine = 0;
                for(var j = forestLine.Count() -1; j >= 0; j--)
                {
                    var tree = forestLine[j];

                    if (i == 0 || i == forest.Count() -1) tree.isVisible = true;
                    if (j == 0 || j == forestLine.Count() -1) tree.isVisible = true;

                    if (tree.size > maxCol[j])
                    {
                        maxCol[j] = tree.size;
                        tree.isVisible = true;
                    }

                    if (tree.size > maxLine)
                    {
                        maxLine = tree.size;
                        tree.isVisible = true;
                    }
                    tree.score = calculateScore(i, j, forest);
                }
            }
            

            var result = forest.Sum(line => line.Sum(tree => tree.isVisible? 1: 0));

            var resultPart2 = forest.Max(line => line.Max(tree => tree.score));
            Console.WriteLine($"result : {result}");
            Console.WriteLine($"result Part 2: {resultPart2}");
        }

    }

    private double calculateScore (int i, int j,  List<List<Tree>> forest)
    {
        int left = 0;
        int right = 0;
        int up = 0;
        int down = 0;

        if (i == 0 || j==0 || i == forest.Count()-1 || j == forest[0].Count()-1) return 0;

        for (var iUp = i-1; iUp >= 0; iUp--)
        {
            up++;
            if (forest[i][j].size <= forest[iUp][j].size)
            break;
        }

        for (var iDown = i+1; iDown < forest[i].Count(); iDown++)
        {
            down++;
            if (forest[i][j].size <= forest[iDown][j].size)
            break;
        }

        for (var jLeft = j-1; jLeft >= 0; jLeft--)
        {
            left++;
            if (forest[i][j].size <= forest[i][jLeft].size)
            break;
        }

        for (var jRight = j+1; jRight < forest.Count(); jRight++)
        {
            right++;
            if (forest[i][j].size <= forest[i][jRight].size)
            break;
        }

        return left * right * up * down;
    }

    private List<Tree> FirstPuzzle(string line, List<List<Tree>> forest)
    {
        var lineTree = new List<Tree>();
        var maxLine = -1;
        for (var i = 0; i < line.Length; i++ )
        {
            var tree = new Tree() {
                size = Int32.Parse(line[i].ToString())
            };
            var maxCol = -1;
            foreach(var forestLine in forest)
            {
                if (forestLine[i].size > maxCol) {
                    maxCol = forestLine[i].size;
                }
            }

            tree.isVisible = tree.size > maxLine || tree.size > maxCol;
            lineTree.Add(tree);
            if (maxLine < tree.size) maxLine = tree.size;
        }
        return lineTree;
    }

    private int SecondPuzzle(string line)
    {
        var score = 0;

        return score;
    }
}

class Tree
{
    public int size { get; set; } = 0;
    public double score { get; set; } = 0;

    public bool isVisible { get; set; } = true;
}
