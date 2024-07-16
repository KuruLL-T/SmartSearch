using System;

namespace SmartSearch.Api.Services
{
    public class ImageService
    {
        private readonly string _imageStorePath;
        public ImageService(IConfiguration configuration)
        {
            _imageStorePath = configuration.GetValue<string>("ImageStorePath");            
        }
        public async Task<string> AddImage(string extention, byte[] bytes)
        {            
            string newName = Guid.NewGuid().ToString() + extention;
            string newPath = Path.Combine(_imageStorePath, newName);
            await File.WriteAllBytesAsync(newPath, bytes);
            return newName;
        }

        public void DeleteImage(string imageName)
        {
            string path = Path.Combine(_imageStorePath, imageName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task<string> GetImage(string imageName)
        {
            string ext = Path.GetExtension(imageName);
            ext = ext.Replace(".", string.Empty);
            string path = Path.Combine(_imageStorePath, imageName);
            byte[] buff = await File.ReadAllBytesAsync(path);
            string base64Str = "data:image/" + ext + ";base64,"
                + Convert.ToBase64String(buff);
            return base64Str;
        }
    }
}
