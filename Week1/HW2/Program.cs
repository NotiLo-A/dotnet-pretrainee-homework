using System.Diagnostics;

namespace HW2
{
    class Program
    {
        static async Task Main()
        {
            var files = new[] { "file 1", "file 2", "file 3" };

            Console.WriteLine("> Synchronous processing <");
            var swSync = Stopwatch.StartNew();

            foreach (var file in files)
            {
                var result = ProcessDataSync(file);
                Console.WriteLine(result);
            }

            swSync.Stop();
            Console.WriteLine($"Synchronous processing finished in {swSync.ElapsedMilliseconds} ms\n");

            Console.WriteLine("> Asynchronous processing <");
            var swAsync = Stopwatch.StartNew();

            var tasks = files.Select(ProcessDataAsync);
            var results = await Task.WhenAll(tasks);

            swAsync.Stop();

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            Console.WriteLine($"Asynchronous processing finished in {swAsync.ElapsedMilliseconds} ms");
        }

        static string ProcessDataSync(string dataName)
        {
            Task.Delay(3000).Wait();
            return $"Processing '{dataName}' completed in 3 seconds";
        }

        static async Task<string> ProcessDataAsync(string dataName)
        {
            await Task.Delay(3000);
            return $"Processing '{dataName}' completed in 3 seconds";
        }
    }
}