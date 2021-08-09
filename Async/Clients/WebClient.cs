using System;
using System.Net;
using System.Text;

namespace Async.Clients
{
    public class WebClient : IWebClient
    {
        private bool _isCanceled = false;

        public void StartDownload(string url, Action<DownloadResult> onDownloaded)
        {
            void ReturnCancelledResult()
            {
                var result = new DownloadResult()
                {
                    IsCanceled = true
                };

                onDownloaded(result);
            }

            try
            {
                if (_isCanceled)
                {
                    ReturnCancelledResult();
                }

                using var client = new System.Net.WebClient();
                client.DownloadStringAsync(new Uri(url));

                client.DownloadStringCompleted += (_, e) => 
                {
                    if (_isCanceled)
                    {
                        ReturnCancelledResult();
                    }
                    else
                    {
                        var result = new DownloadResult()
                        {
                            IsCanceled = false,
                            Content = e.Result
                        };

                        onDownloaded(result);
                    }
                };
            }
            catch (Exception ex)
            {
                var result = new DownloadResult()
                {
                    IsCanceled = _isCanceled,
                    Error = ex
                };

                onDownloaded(result);
            }
        }

        public void CancelDownload()
        {
            _isCanceled = true;
        }
    }
}
