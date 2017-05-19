using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DribblyAPI.Repositories
{
    public class FileRepository : IDisposable
    {
        private string uploadPath;
        private string uploadBasePath;

        public FileRepository()
        {
            uploadPath = HttpContext.Current.Server.MapPath("~/" + WebConfigurationManager.AppSettings["imageUploadPath"]);
            uploadBasePath = HttpContext.Current.Server.MapPath("~/" + WebConfigurationManager.AppSettings["fileUploadBasePath"]);
        }

        public string UploadCourtPhoto(HttpPostedFile file, string userId)
        {
            string folderPath = uploadPath + userId + '/';

            string uploadedFilePath = "";

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string ext = System.IO.Path.GetExtension(file.FileName);
                string uploadedFileName;

                do
                {
                    uploadedFileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ext;
                    uploadedFilePath = folderPath + uploadedFileName;
                } while (File.Exists(uploadedFilePath));

                file.SaveAs(uploadedFilePath);

                return uploadedFileName;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Upload(HttpPostedFile file, string subDir)
        {
            string folderPath = uploadBasePath + subDir;

            string uploadedFilePath = "";

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string ext = System.IO.Path.GetExtension(file.FileName);
                string uploadedFileName;

                do
                {
                    uploadedFileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ext;
                    uploadedFilePath = folderPath + uploadedFileName;
                } while (File.Exists(uploadedFilePath));

                file.SaveAs(uploadedFilePath);

                return uploadedFileName;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool delete(string fileName, string userId)
        {
            try
            {
                File.Delete(uploadPath + userId + '/' + fileName);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}