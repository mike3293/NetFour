using System;
using System.Threading;
using System.Threading.Tasks;
using Async.Clients;

namespace Async
{
    class Program
    {
        static async Task Main()
        {
            //var client = new WebClient();

            //Thread thread = new Thread(() =>
            //{
            //    Thread.Sleep(1000);
            //    client.CancelDownload();
            //});

            //thread.Start();

            //client.StartDownload(
            //    "https://www.google.com/logos/doodles/2021/doodle-champion-island-games-july-26-6753651837109017-s.png",
            //    result =>
            //    {
            //        Console.WriteLine($"IsCancelled: {result.IsCancelled}");
            //        Console.WriteLine($"Error: {result.Error?.Message}");
            //        Console.WriteLine($"Content: {result.Content}");
            //    });

            var client = new WebClientAsync();

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            tokenSource.CancelAfter(1000);

            var content = await client.DonwloadAsync(
                "https://www.google.com/logos/doodles/2021/doodle-champion-island-games-july-26-6753651837109017-s.png",
                token);

            Console.WriteLine($"Content: {content}");
        }
    }
}
