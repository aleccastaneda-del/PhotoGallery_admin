using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.http.model
{
    class ImageModelJsonConverter : JsonConverter<Bitmap>
    {

        public override Bitmap ReadJson(JsonReader reader, Type objectType, Bitmap existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            byte[] tmp = Convert.FromBase64String(reader.Value as string);
            ImageConverter converter = new ImageConverter();
            return (Bitmap)converter.ConvertFrom(tmp);
        }

        public override void WriteJson(JsonWriter writer, Bitmap value, JsonSerializer serializer)
        {
            ImageConverter converter = new ImageConverter();
            byte[] tmp = (byte[])converter.ConvertTo(value, typeof(byte[]));
            writer.WriteValue(Convert.ToBase64String(tmp));
        }
    }
}
