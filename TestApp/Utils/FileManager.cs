using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Utils
{
    public class FileManager
    {
        public static async Task<string> UploadFile(string webRootPath ,string newFileName , IFormFile file)
        {
            var uploads = Path.Combine(webRootPath, "uploads");
            string filename = newFileName + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploads, filename);
            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
            return filename;
        }
    }
}
