using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Web.Configuration;
using DribblyAPI.Repositories;

namespace DribblyAPI.Controllers
{
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        //private string uploadPath = "D:/RJ/Projects/dribbly-test/www/images/uploads/courts/";
        private string uploadPath;
        private FileRepository _repo;

        public FileController()
        {
            _repo = new FileRepository();
        }

        [Route("UploadCourtPhoto/{userId}")]
        public IHttpActionResult UploadCourtPhoto(string userId)
        {
            string uploadedFilePath = "";

            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

            if (files.Count > 0)
            {
                try
                {
                    uploadedFilePath = _repo.Upload(files[0], userId + "/courtPhotos/");
                    return Ok(uploadedFilePath);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            else
            {
                return BadRequest("No files to upload.");
            }
        }

        [Route("UploadProfilePic/{userId}")]
        public IHttpActionResult UploadProfilePic([FromUri]string userId)
        {
            string uploadedFilePath = "";

            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

            if (files.Count > 0)
            {
                try
                {
                    uploadedFilePath = _repo.Upload(files[0], userId + "/profilePic/");
                    return Ok(uploadedFilePath);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            else
            {
                return BadRequest("No files to upload.");
            }
        }

        //[Route("upload")]
        //public IHttpActionResult Upload()
        //{

        //    string uploadedFilePath = "";

        //    System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

        //    if (files.Count > 0)
        //    {
        //        try
        //        {
        //            if (!Directory.Exists(uploadPath))
        //            {
        //                Directory.CreateDirectory(uploadPath);
        //            }

        //            string ext = System.IO.Path.GetExtension(files[0].FileName);
        //            string uploadedFileName;

        //            do
        //            {
        //                uploadedFileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ext;
        //                uploadedFilePath = uploadPath + uploadedFileName;
        //            } while (File.Exists(uploadedFilePath));

        //            files[0].SaveAs(uploadedFilePath);

        //            return Ok(uploadedFileName);

        //        }
        //        catch (Exception ex)
        //        {
        //            return InternalServerError(ex);
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("No files to upload.");
        //    }
        //}

        [Route("deleteCourtPhoto/{fileName}/{userId}")]
        public IHttpActionResult delete(string fileName, string userId)
        {
            try
            {
                _repo.delete(fileName, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("deleteUserPhoto/{fileName}/{userId}")]
        public IHttpActionResult deleteUserPhoto(string fileName, string userId)
        {
            try
            {
                _repo.deleteUserPhoto(fileName, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
