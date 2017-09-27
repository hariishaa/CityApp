using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.Models.Television
{
    public class YoutubeItem
    {
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChannelTitle { get; set; }
        public string DefaultThumbnailUrl { get; set; }
        public string MediumThumbnailUrl { get; set; }
        public string HighThumbnailUrl { get; set; }
        public string StandardThumbnailUrl { get; set; }
        public string MaxResThumbnailUrl { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
