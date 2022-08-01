using Newtonsoft.Json;
using PhotoGallery.http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.http.model
{
    public class ImageModel
    {
        public ImageModel(string title, Bitmap image, string type, string folderId, bool? favorite)
        {
            Title = title;
            Image = image;
            Type = type;
            FolderId = folderId;
            Favorite = favorite;
        }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("image")]
        [JsonConverter(typeof(ImageModelJsonConverter))]
        public Bitmap Image { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("favorite")]
        public bool? Favorite { get; set; }

        [JsonProperty("folderId")]
        public string FolderId { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

    }

    public class IDSend
    {
        [JsonProperty("id")]
        public string ID { get; set; }
    }
}
