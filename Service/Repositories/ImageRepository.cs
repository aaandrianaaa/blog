using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{

    public interface IImageRepository
    {
        Task<bool> PostPhotoAsync(IFormFile file, int? userID);
        Task<byte[]> GetAvatarAsync(int id);
    }
    public class ImageRepository : IImageRepository
    {

        private readonly IHostingEnvironment _hostingEnvironment;

        public ImageRepository(IHostingEnvironment _hostingEnvironment)
        {
            this._hostingEnvironment = _hostingEnvironment;

        }

        public async Task<bool> PostPhotoAsync(IFormFile file, int? userID)
        {

            var size = file.Length;
            var fileName = userID.Value.ToString() + ".png";
            var filePath = _hostingEnvironment.ContentRootPath + "/Images" + "/" + fileName;
            if (size > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return true;

        }

        public async Task<byte[]> GetAvatarAsync(int id)
        {
            var fileName = _hostingEnvironment.ContentRootPath + "/Images" + "/" + id.ToString() + ".png";
            return File.ReadAllBytes(fileName);
        }

    }
}
