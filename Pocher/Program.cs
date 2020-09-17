using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Pocher
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebClient();
            var html = client.DownloadString("https://audionow.de/podcast/die-pochers-hier");

            var matchesCollection = Regex.Matches(html, "<div class=\"podcast-episode.*>");
            foreach (Match match in matchesCollection)
            {
                
                var title = Regex.Match(match.Value, @"\d+ - ([\w\süöä]+)").Value;
                var url = Regex.Match(match.Value, "https:(.+).mp3").Value;

                client.DownloadFile(new Uri(url), $"{title}.mp3");
            }
        }
    }
}
