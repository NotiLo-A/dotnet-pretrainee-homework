namespace HW2;

class Program
{
    static async Task Main()
    {
        var files = new[] { "file 1", "file 2", "file 3" };
        var tasks = files.Select(ProcessDataAsync);
        
        var results = await Task.WhenAll(tasks);

        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
    
    static async Task<string> ProcessDataAsync(string dataName)
    {
        await Task.Delay(3000);
        return $"Processing '{dataName}' completed in 3 seconds";
    }
}