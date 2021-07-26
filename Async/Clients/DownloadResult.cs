using System;

namespace Async.Clients
{
    public class DownloadResult
    {
        public bool IsCancelled { get; set; }

        public Exception Error { get; set; }

        public string Content { get; set; }
    }
}
