using System;
using System.Text;

namespace TestLibrary
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
                    IsCancelled = true
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
                var content = client.DownloadString(url);

                if (_isCanceled)
                {
                    ReturnCancelledResult();
                }
                else
                {
                    var result = new DownloadResult()
                    {
                        IsCancelled = false,
                        Content = content
                    };

                    onDownloaded(result);
                }
            }
            catch (Exception ex)
            {
                var result = new DownloadResult()
                {
                    IsCancelled = _isCanceled,
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
