using System;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace BulkDL {
    class App {
        private static double _elapsedTime;
        private readonly static int _totalNumberOfPages = 62840; // Currently the total nummber of pages on the BTTV API.
        private static long _generateId() => (long.MaxValue + DateTime.Now.ToBinary()) / 10000; // 草

        static void Main(string[] args)
        {
            for(var i =62600; i < _totalNumberOfPages; i+= 100) 
            {
                FetchAndDownload(offset: i + 100);
                LogUtil.LogWhite("Sleeping thread for 10 seconds.");
                Thread.Sleep(10000);
            }

            LogUtil.LogWhite($"The operation took {_elapsedTime} seconds to complete.");
            Console.ReadKey();
        }

        static void DownloadEmote(Emote emote)
        {
            using(var Client = new WebClient())
            {
                try
                {
                    if (!File.Exists($@"C:\Users\Vezqi\Desktop\images\{emote.combined}"))
                    {
                        Client.DownloadFile(emote.url, $@"C:\Users\Vezqi\Desktop\images\{emote.combined}");
                        LogUtil.LogGreen($"Successfully downloaded {emote.combined} ({emote.id})");
                    }
                    else
                    {
                        LogUtil.LogYellow($@"{emote.combined} ({emote.id}) already exists.");
                        Client.DownloadFile(emote.url, $@"C:\Users\Vezqi\Desktop\images\duplicates\{emote.name}__{_generateId()}{emote.extension}");
                    }
                }
                catch (Exception e) {
                       LogUtil.LogRed(e.ToString());
                }
}
        }

        static void FetchAndDownload(int offset)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            LogUtil.LogMagenta($"Starting request at offset {offset}");
            var url = $"https://api.betterttv.net/3/emotes/shared/top?offset={offset}&limit=100";

            using(var Client = new WebClient())
            {
                var ResponseString = Client.DownloadString(url);
                var ResponseData = JArray.Parse(ResponseString);

                foreach(var EmoteEnumerator in ResponseData)
                {
                    var root = EmoteEnumerator["emote"];
                    var EmoteObject = new Emote()
                    {
                        id = (string)root["id"],
                        name = (string)root["code"],
                        extension = $".{root["imageType"]}",
                        url = $"https://cdn.betterttv.net/emote/{root["id"]}/3x",
                        combined = $"{root["code"]}.{root["imageType"]}"
                    };

                    DownloadEmote(emote: EmoteObject);

                }

                stopwatch.Stop();
                var SecondsElapsed = Math.Round(stopwatch.Elapsed.TotalSeconds);
                var ElapsedFormat = SecondsElapsed > 1 ? $"{SecondsElapsed} seconds" : $"{SecondsElapsed} second.";
                LogUtil.LogCyan($"Offset {offset} operation took {ElapsedFormat}");
                _elapsedTime += SecondsElapsed;

            }

        }

    }
}