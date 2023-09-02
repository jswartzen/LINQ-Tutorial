using System.Text;
using System;

static IEnumerable<string> CreateEnumeration(string valueFormat, int count, bool quiet)
{
    for (int i = 0; i < count; i++)
    {
        var s = string.IsNullOrEmpty(valueFormat) ? RandomString(100) : string.Format(valueFormat, i);
        if (!quiet)
        {
            Console.WriteLine(s);
        }

        yield return s;
    }
}

// Generates a random string with a given size.
static string RandomString(int size)
{
    var random = new Random();
    var builder = new StringBuilder(size);

    for (var i = 0; i < size; i++)
    {
        builder.Append((char)random.Next('A', 'A' + 26));
    }

    return builder.ToString();
}

// Demonstrate deferred execution
int loopCount = 0;
foreach (var s in CreateEnumeration("[Deferred execution string {0}]", 5, false))
{
    Console.WriteLine($"Processing value in loop {loopCount++}");
}
Console.WriteLine();

// Demonstrate .ToList() stops deferring execution
loopCount = 0;
foreach (var s in CreateEnumeration("[Non-deferred execution string {0}]", 5, false).ToList())
{
    Console.WriteLine($"Processing value in loop {loopCount++}");
}
Console.WriteLine();

// Demonstrate that .Count() stops deferring execution
Console.WriteLine($"This IEnumerable<string> has {CreateEnumeration("[Count() also stops deferring execution string {0}]", 6, false).Count()} items in it");
Console.WriteLine();

// Show that memory isn't allocated until execution deferral ends
Console.WriteLine($"Initial memory: {GC.GetGCMemoryInfo().TotalCommittedBytes}");

var data = CreateEnumeration(string.Empty, 1000000, true);
Console.WriteLine($"Memory after data defined: {GC.GetGCMemoryInfo().TotalCommittedBytes}");

Console.WriteLine($"Data has {data.Count()} items");
Console.WriteLine($"Memory after Count() called: {GC.GetGCMemoryInfo().TotalCommittedBytes}");
Console.WriteLine($"Data has {data.Count()} items");
Console.WriteLine($"Memory after Count() called: {GC.GetGCMemoryInfo().TotalCommittedBytes}");
Console.WriteLine();
var info = GC.GetGCMemoryInfo();

Console.WriteLine("Always prefer .Any() to .Count() > 0");
var data2 = CreateEnumeration(string.Empty, 1000000, false);
Console.WriteLine($"Memory after data2 defined: {GC.GetGCMemoryInfo().TotalCommittedBytes}");
Console.WriteLine($"data2 has contents based on .Any() call: {data2.Any()}");
Console.WriteLine($"Memory after Any() called: {GC.GetGCMemoryInfo().TotalCommittedBytes}");
Console.WriteLine();
