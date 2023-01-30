using System;
using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Highlighter
{
    public class Function1
    {
        [FunctionName("Highlight")]
        public void Run([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            WebClient wc = new WebClient();
            var result = wc.DownloadString("https://footballwebshop.azurewebsites.net/Home/HighlightShoe");
        }
    }
}
