using PhotoGallery.Properties;
using System.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PhotoGallery.http.model;
using PhotoGallery.http;

namespace PhotoGallery
{
    public partial class ImageData : Control
    {

        #region Properties
        public string ID { get; set; }
        public DateTime DateModifiedProp { get; set; }
        public ImageType Extension { get; set; }
        public bool IsFavoriteProp { get; set; }
        public string TitleProp { get; set; }
        public Image Source { get; set; }
        public bool SourceIsSelected { get; }
        #endregion

        public ImageData(ImageModel imageFromServer)
        {
            InitializeComponent();

            #region Parsing
            ID = imageFromServer.ID;
            Source = imageFromServer.Image;
            switch (imageFromServer.Type)
            {
                case "png":
                    Extension = ImageType.PNG;
                    break;
                case "jpeg":
                case "jpg":
                    Extension = ImageType.JPG;
                    break;
                case "svg":
                    Extension = ImageType.SVG;
                    break;
            }
            TitleProp = imageFromServer.Title;
            IsFavoriteProp = imageFromServer.Favorite ?? false;
            DateModifiedProp = imageFromServer.CreatedAt;
            #endregion Parsing
        }

        public ImageData(string filePath)
        {
            InitializeComponent();

            #region Parsing
            string[] fileNameAndExt = filePath.Substring(filePath.LastIndexOfAny(new char[] { '\\', '/' }) + 1).Split('.');
            Source = new Bitmap(Image.FromFile(filePath), 160, 80);
            DateModifiedProp = new FileInfo(filePath).LastWriteTime;
            switch (fileNameAndExt[1].ToLower())
            {
                case "png":
                    Extension = ImageType.PNG;
                    break;
                case "jpeg":
                case "jpg":
                    Extension = ImageType.JPG;
                    break;
                case "svg":
                    Extension = ImageType.SVG;
                    break;
            }
            TitleProp = $"{fileNameAndExt[0]}.{fileNameAndExt[1].ToLower()}";
            #endregion Parsing

        }
    }
}
