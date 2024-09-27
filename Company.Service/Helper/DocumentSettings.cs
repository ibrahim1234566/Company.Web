using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {

            // var FolderPath = @"C:\\Users\\Magic Store\\Desktop\\new mvc\\Company.Web\\wwwroot\\Images\\";
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot//Files",FolderName);
            var FileName = $"{Guid.NewGuid}-{file.FileName}";
            var FilePath = Path.Combine(FolderPath,FileName);
            using var FileStream = new FileStream(FilePath, FileMode.Create);
            file .CopyTo(FileStream);
            return FilePath;
        }
    }
}
