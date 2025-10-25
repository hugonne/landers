namespace TareasApi.ConsoleTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting both requests...");

            // Kick off both async calls — don't await them yet
            var task1 = FetchDataAsync("https://jsonplaceholder.typicode.com/posts/1", 3000);
            var task2 = FetchDataAsync("https://jsonplaceholder.typicode.com/posts/2", 500);

            Console.WriteLine("Both requests sent. Waiting for results...");

            // Await both in parallel
            // await Task.WhenAll(task1, task2);
            //
            // // Retrieve results
            // var result1 = await task1;
            // var result2 = await task2;

            Console.WriteLine("\n--- Result 1 ---");
            //Console.WriteLine(result1);

            Console.WriteLine("\n--- Result 2 ---");
            //Console.WriteLine(result2);

            Console.WriteLine("\nAll done!");
        }

        static async Task<string> FetchDataAsync(string url, int sleep)
        {
            using var httpClient = new HttpClient();
            Console.WriteLine($"Fetching {url} - {DateTime.Now}");
            await Task.Delay(sleep);
            if (sleep == 3000)
            {
                throw new Exception("Error");
            }
            
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}