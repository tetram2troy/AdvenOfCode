// using System.IO;


// Console.WriteLine("read csv ");

// using (var reader = new StreamReader("input.csv"))
// {
//     var max = 0;
//     var sum = 0;
//     while (! reader.EndOfStream)
//     {
//         var line = reader.ReadLine();
//         if (string.IsNullOrEmpty(line)) 
//         {
//             if (sum > max) max = sum;
//             sum = 0;
//         }
//         else
//         {
//             int lineParsed = Int32.Parse(line);
//             sum += lineParsed;
//         }

//     }

//     Console.WriteLine($"max : {max}");
// }
