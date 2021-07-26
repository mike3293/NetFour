using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Clients
{
    public interface IWebClient
    {
        void StartDownload(string url, Action<DownloadResult> onDownloaded);

        void CancelDownload();
    }
}
