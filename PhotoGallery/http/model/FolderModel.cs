using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.http.model
{
    public class FolderModel
    {
        public FolderModel(string name, string teamId)
        {
            Name = name;
            TeamId = teamId;
        }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("teamId")]
        public string TeamId { get; set; }

    }

}
