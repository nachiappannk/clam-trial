using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using nClam;

namespace clam_av_runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var clam = new ClamClient("localhost", 3310);

            var sw = Stopwatch.StartNew();

            var scanResult = await clam.ScanFileOnServerAsync("/test/text.mp4");  //any file you would like!

            long ticks = sw.ElapsedTicks;
            Console.WriteLine(sw.ElapsedMilliseconds);

            switch (scanResult.Result)
            {
                case ClamScanResults.Clean:
                    Console.WriteLine("The file is clean!");
                    break;
                case ClamScanResults.VirusDetected:
                    Console.WriteLine("Virus Found!");
                    Console.WriteLine("Virus name: {0}", scanResult.InfectedFiles.First().VirusName);
                    break;
                case ClamScanResults.Error:
                    Console.WriteLine("Woah an error occured! Error: {0}", scanResult.RawResult);
                    break;
            }

        }
    }
}
