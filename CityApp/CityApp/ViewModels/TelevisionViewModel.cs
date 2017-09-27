using CityApp.Helpers;
using CityApp.Models.Television;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.ViewModels
{
    public class TelevisionViewModel
    {
        public List<YoutubeItem> YoutubeItems { get; private set; }
        public string EmptyListText { get; private set; }

        public async Task LoadYoutubeData()
        {
            string url = "https://www.googleapis.com/youtube/v3/playlistItems";
            var parameters = new Dictionary<string, string>
            {
                ["part"] = "snippet",
                ["maxResults"] = "20",
                ["playlistId"] = "UUNFZx8xDMOfwcwe4WJaP7DA",
                ["key"] = "AIzaSyCut9sVr4_vkx2Z4e4y5WyT-XNIpMxDAUc",
            };
            try
            {
                var jObject = await WebService.MakeUrlRequest(url, parameters);
                var rawItems = jObject.Value<JArray>("items");
                YoutubeItems = new List<YoutubeItem>();
                if (rawItems.Count == 0)
                {
                    EmptyListText = Properties.Strings.EmptyListString;
                    return;
                }
                foreach (var item in rawItems)
                {
                    var snippet = item.Value<JObject>("snippet");
                    var youtubeItem = new YoutubeItem
                    {
                        Title = snippet.Value<string>("title"),
                        Description = snippet.Value<string>("description"),
                        ChannelTitle = snippet.Value<string>("channelTitle"),
                        PublishedAt = snippet.Value<DateTime>("publishedAt"),
                        VideoId = snippet?.Value<JObject>("resourceId")?.Value<string>("videoId"),
                        DefaultThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("default")?.Value<string>("url"),
                        MediumThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("medium")?.Value<string>("url"),
                        HighThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("high")?.Value<string>("url"),
                        StandardThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("standard")?.Value<string>("url"),
                        MaxResThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("maxres")?.Value<string>("url"),
                    };
                    YoutubeItems.Add(youtubeItem);
                }
                EmptyListText = string.Empty;
            }
            catch (Exception ex)
            {
                YoutubeItems = null;
                EmptyListText = Properties.Strings.ExceptionString + ex.Message;
            }
        }
    }
}
