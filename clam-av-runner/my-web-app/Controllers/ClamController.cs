using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using nClam;
using System.Web;

namespace my_web_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClamController : ControllerBase
    {
        [HttpPost("files/{name}/scan")]
        public async Task<Result> Scan([FromRoute]String name)
        {
            try
            {
                name = HttpUtility.UrlDecode(name);
                var clam = new ClamClient("localhost", 3310);

                var sw = Stopwatch.StartNew();

                var scanResult = await clam.ScanFileOnServerAsync(name);  //any file you would like!

                long ticks = sw.ElapsedTicks;
                var time = sw.ElapsedMilliseconds;

                switch (scanResult.Result)
                {
                    case ClamScanResults.Clean:
                        return new Result() { FileName = name, IsCompleted = true, IsClean = true, TimeTakenInMs = time };
                    case ClamScanResults.VirusDetected:
                        return new Result() { FileName = name, IsCompleted = true, IsClean = false, TimeTakenInMs = time };
                    case ClamScanResults.Error:
                    default:
                        return new Result() { FileName = name, IsCompleted = false };
                }
            }
            catch(Exception e)
            {
                return new Result() { FileName = name, IsCompleted = false };
            }
            
        }
    }

    public class Result
    {
        public bool IsClean { get; set; }
        public bool IsCompleted { get; set; }
        public long TimeTakenInMs { get; set; }

        public String FileName { get; set; }
    }
}
