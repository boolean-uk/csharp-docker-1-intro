// See https://aka.ms/new-console-template for more information

using System.Diagnostics.Metrics;

DateTime start = DateTime.Now;
DateTime end = DateTime.Now.AddSeconds(10);
int countdown = 10;
while (DateTime.Now.CompareTo(end)==-1)
{
    Console.WriteLine("************************");
    Console.WriteLine($"Started  :{start}");
    Console.WriteLine($"Now      :{DateTime.Now}");
    Console.WriteLine($"Ending   :{end}");
    Console.WriteLine($"Remaining: {countdown}");
    Console.WriteLine("************************");
    countdown--;
    await Task.Delay(TimeSpan.FromMilliseconds(1_000));

}
Console.WriteLine(" **** THE END **** ");
