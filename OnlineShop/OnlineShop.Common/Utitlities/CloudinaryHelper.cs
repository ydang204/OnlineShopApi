using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OnlineShop.Common.SettingOptions;
using System.Collections.Generic;

namespace OnlineShop.Common.Utitlities
{
    public class CloudinaryHelper
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryHelper(IOptions<CloudinaryOptions> cloudinaryOptions)
        {
            var cloudinarySettings = cloudinaryOptions.Value;

            Account cloudinaryAccount = new Account(
               cloudinarySettings.CloudName,
               cloudinarySettings.ApiKey,
               cloudinarySettings.ApiSecret
           );

            _cloudinary = new Cloudinary(cloudinaryAccount);
        }

        public ImageUploadResult UploadImage(IFormFile image)
        {
            var uploadResult = new ImageUploadResult();
            if (image.Length > 0)
            {
                using (var stream = image.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.Name, stream),
                        Transformation = new Transformation().Width(800).Height(800).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            return uploadResult;
        }

        public List<ImageUploadResult> UploadImages(List<IFormFile> images)
        {
            var uploadResults = new List<ImageUploadResult>();
            foreach (var image in images)
            {
                uploadResults.Add(UploadImage(image));
            }

            return uploadResults;
        }
    }
}