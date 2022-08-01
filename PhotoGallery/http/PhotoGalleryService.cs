using Newtonsoft.Json;
using PhotoGallery.http.model;
using PhotoGallery.Properties;
using PhotoGallery.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.http
{
    public class PhotoGalleryService
    {

        public async Task<ImageModel[]> ListImages(string folderId)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync($"{Properties.Settings.Default.API_URL}/images/list?folderId={folderId}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                NotificationBroadCaster.displayError(await response.Content.ReadAsStringAsync());
                return null;
            }
            NotificationBroadCaster.displaySuccess("Successfully retrieved images from folder");

            ImageModel[] imageDataList = JsonConvert.DeserializeObject<ImageModel[]>(await response.Content.ReadAsStringAsync(), new ImageModelJsonConverter());
            return imageDataList;
        }

        public async Task<ImageModel> GetImageInfo(string id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync($"{Properties.Settings.Default.API_URL}/images/info?id={id}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                NotificationBroadCaster.displayError(await response.Content.ReadAsStringAsync());
                return null;
            }
            NotificationBroadCaster.displaySuccess("Successfully retrieved image info");

            ImageModel imageData = JsonConvert.DeserializeObject<ImageModel>(await response.Content.ReadAsStringAsync(), new ImageModelJsonConverter());
            return imageData;
        }

        public async Task<ImageModel> UploadImage(string title, Bitmap image, string type, string folderId, bool? favorite)
        {
            ImageModel payload = new ImageModel(title,image,type,folderId,favorite);

            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(Properties.Settings.Default.API_URL + "/images/add", content);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                NotificationBroadCaster.displayError(await response.Content.ReadAsStringAsync());
                return null;
            }
            NotificationBroadCaster.displaySuccess("Successfully uploaded image to folder");

            ImageModel newImageData = JsonConvert.DeserializeObject<ImageModel>(await response.Content.ReadAsStringAsync(),new ImageModelJsonConverter());
            return newImageData;
        }

        public async Task<bool> DeleteImage(string id)
        {
            bool res;
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"{Properties.Settings.Default.API_URL}/images/delete?id={id}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    NotificationBroadCaster.displayError(await response.Content.ReadAsStringAsync());
                    return false;
                }

                string successFlag = await response.Content.ReadAsStringAsync();
                res = int.Parse(successFlag) == 1;
            } catch (Exception ex)
            {
                NotificationBroadCaster.displayError("Error occurred while parsing response");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                return false;
            }
            NotificationBroadCaster.displaySuccess("Successfully deleted image");
            return res;
        }
        
        public async Task<ImageModel> ToggleFavorite(string id)
        {
            HttpClient client = new HttpClient();
            var tmp = new IDSend()
            {
                ID = id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(tmp), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(Properties.Settings.Default.API_URL + "/images/favorite", content);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                NotificationBroadCaster.displayError(await response.Content.ReadAsStringAsync());
                return null;
            }
            NotificationBroadCaster.displaySuccess("Successfully toggled image favorite status");

            ImageModel imageData = JsonConvert.DeserializeObject<ImageModel>(await response.Content.ReadAsStringAsync(), new ImageModelJsonConverter());
            return imageData;
        }

        public async Task<FolderModel> CreateFolder(string name)
        {
            FolderModel payload = new FolderModel(name, Settings.Default.TeamId);
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(Properties.Settings.Default.API_URL + "/folders/add", content);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                NotificationBroadCaster.displayError(await response.Content.ReadAsStringAsync());
                return null;
            }
            NotificationBroadCaster.displaySuccess("Successfully created a new folder");

            FolderModel folder = JsonConvert.DeserializeObject<FolderModel>(await response.Content.ReadAsStringAsync());
            return folder;
        }

        public async Task<bool> DeleteFolder(string id)
        {
            bool res;
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"{Properties.Settings.Default.API_URL}/folders/delete?id={id}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    NotificationBroadCaster.displayError(await response.Content.ReadAsStringAsync());
                    return false;
                }
                NotificationBroadCaster.displaySuccess("Successfully deleted folder");

                string successFlag = await response.Content.ReadAsStringAsync();
                res = int.Parse(successFlag) == 1;
            } catch (Exception ex)
            {
                NotificationBroadCaster.displayError("Error occurred while parsing response");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                return false;
            }
            return res;
        }

        public async Task<FolderModel> GetFolderInfo(string id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Properties.Settings.Default.API_URL}/folders/info?id={id}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                NotificationBroadCaster.displayError(await response.Content.ReadAsStringAsync());
                return null;
            }
            NotificationBroadCaster.displaySuccess("Successfully retrieved folder info");

            FolderModel folder = JsonConvert.DeserializeObject<FolderModel>(await response.Content.ReadAsStringAsync());
            return folder;
            
        }

        public async Task<FolderModel[]> ListFolders(string id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Properties.Settings.Default.API_URL}/folders/list");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                NotificationBroadCaster.displayError(await response.Content.ReadAsStringAsync());
                return null;
            }
            NotificationBroadCaster.displaySuccess("Successfully retrieved folder info");

            FolderModel[] folder = JsonConvert.DeserializeObject<FolderModel[]>(await response.Content.ReadAsStringAsync());
            return folder;

        }


    }
}
