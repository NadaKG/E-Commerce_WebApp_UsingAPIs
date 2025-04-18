﻿using Application.Service_Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Service
{
    public class PhotoService: IPhotoService
    {
        public IConfiguration Configuration { get; }
        private Cloudinary _cloudinary;
        private CloudinarySettings _cloudinarySettings;


        public PhotoService(IConfiguration configuration)
        {
            Configuration = configuration;
            _cloudinarySettings = Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();

            Account account = new Account(
                _cloudinarySettings.CloudName,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret
                );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);

            }
            return uploadResult;

        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
