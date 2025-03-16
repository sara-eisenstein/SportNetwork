using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace Service.Helpers
{
    public static class FileHelper
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        public static string SaveImageToDisk(IFormFile file)
        {
            if (file == null)
                return null;

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(extension))
                throw new Exception("Invalid file type. Only JPG, PNG, and GIF are allowed.");

            if (file.Length > MaxFileSize)
                throw new Exception("File size exceeds the allowed limit of 5MB.");

            var mediaPath = Path.Combine(Environment.CurrentDirectory, "media");
            if (!Directory.Exists(mediaPath))
            {
                Directory.CreateDirectory(mediaPath);
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(mediaPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName; // מחזיר רק את שם הקובץ, לא נתיב מלא
        }
    }
}

