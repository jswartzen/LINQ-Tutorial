static IEnumerable<string> CreateEnumeration(string valueFormat, int count, bool quiet)
{
    for (int i = 0; i < count; i++)
    {
        var s = string.Format("Creating: " + valueFormat, i);
        if (!quiet)
        {
            Console.WriteLine(s);
        }

        yield return s;
    }
}

// Demonstrate deferred execution
int loopCount = 0;
foreach (var s in CreateEnumeration("Deferred execution string {0}", 5, false))
{
    Console.WriteLine($"Processing value in loop {loopCount++}");
}
Console.WriteLine();

// Demonstrate .ToList() stops deferring execution
loopCount = 0;
foreach (var s in CreateEnumeration("Non-deferred execution string {0}", 5, false).ToList())
{
    Console.WriteLine($"Processing value in loop {loopCount++}");
}
Console.WriteLine();

// Demonstrate that .Count() stops deferring execution
Console.WriteLine($"This IEnumerable<string> has {CreateEnumeration("Count() also stops deferring execution string {0}", 6, false).Count()} items in it");
Console.WriteLine();

// Show that memory isn't allocated until execution deferral ends
Console.WriteLine($"Initial memory: {GC.GetGCMemoryInfo().TotalCommittedBytes}");

var data = CreateEnumeration("1111111111222222222233333333334444444444555555555566666666667777777777888888888899999999990000000000", 1000000, true);
Console.WriteLine($"Memory after data defined: {GC.GetGCMemoryInfo().TotalCommittedBytes}");

Console.WriteLine($"Data has {data.Count()} items");
Console.WriteLine($"Memory after Count() called: {GC.GetGCMemoryInfo().TotalCommittedBytes}");
Console.WriteLine($"Data has {data.Count()} items");
Console.WriteLine($"Memory after Count() called: {GC.GetGCMemoryInfo().TotalCommittedBytes}");

