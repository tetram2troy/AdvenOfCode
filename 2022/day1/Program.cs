using System.IO;


Console.WriteLine("read csv ");

using (var reader = new StreamReader("input.csv"))
{
    var sumList = new List<int> ();
    var sum = 0;
    while (! reader.EndOfStream)
    {
        var line = reader.ReadLine();
        if (string.IsNullOrEmpty(line)) 
        {
            sumList.Add(sum);
            sum = 0;
        }
        else
        {
            int lineParsed = Int32.Parse(line);
            sum += lineParsed;
        }
    }
    var result = sumList.OrderDescending().Take(3).Sum();

    Console.WriteLine($"max : {result}");
}
