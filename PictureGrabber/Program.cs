using System;
using System.IO;
using System.Net;
using System.Threading;

namespace PictureGrabber
{
    class Program
    {
        /// <summary>
        /// args
        /// * url
        /// * filePath
        /// * intervalInMinutes
        /// * looping
        /// 
        /// http://holfuy.com/en/takeit/cam/s313.jpg
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.Out.WriteLine("The program require the following arguments");
                Console.Out.WriteLine("\"picture url\" \"Download folder file path\" isLooping intervalInMinutes");
                Console.ReadLine();
                return;
            }

            var url = args[0];
            var folderPath = args[1];
            var isLooping = Boolean.Parse(args[2]);
            var intervalInMinutes = int.Parse(args[3]);

            if (isLooping)
                Console.Out.WriteLine("Press Escape to stop program");

            do
            {
                do
                {
                    var filePath = Path.Combine(folderPath,
                        DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "_" + Guid.NewGuid() + ".jpg");
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(url, filePath);
                    }
                    Console.Out.WriteLine("" + filePath);

                    if (isLooping) Thread.Sleep(intervalInMinutes * 60 * 1000);

                } while (isLooping && !Console.KeyAvailable);

            } while (isLooping && Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
