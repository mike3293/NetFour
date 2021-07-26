using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Clients
{
    interface IWebClientAsync
    {
        Task<string> DonwloadAsync(string url, CancellationToken cancellationToken);
    }
}
