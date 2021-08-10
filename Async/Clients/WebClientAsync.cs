using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Clients
{
    class WebClientAsync : IWebClientAsync
    {
        public Task<string> DonwloadAsync(string url, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<string>();

            var syncClient = new WebClient();
            var tokenRegistration = cancellationToken.Register(syncClient.CancelDownload);

            try
            {
                syncClient.StartDownload(url, (result) =>
                {
                    if (result.Error is not null)
                    {
                        throw result.Error;
                    }

                    if (result.IsCanceled)
                    {
                        tcs.SetCanceled(cancellationToken);
                    }
                    else
                    {
                        tcs.SetResult(result.Content);
                    }

                    tokenRegistration.Unregister();
                });

            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task;
        }
    }
}
