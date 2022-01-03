using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using RestSharp;
using RestSharp.Authenticators;
using Domain.Models;
using Aspose.Zip;
using Aspose.Zip.Saving;


namespace PresentationWebApp.Controllers
{
    [Authorize]
    public class FileTransferController : Controller
    {
        private IWebHostEnvironment hostEnvironment;
        private ILogger<FileTransferController> logger;
        private IFileTransferService service;


        public FileTransferController(ILogger<FileTransferController> _logger, IWebHostEnvironment _hostEnvironment, IFileTransferService _service) //DI
        {
            logger = _logger;
            hostEnvironment = _hostEnvironment;
            service = _service;

        }

        public IActionResult Index()
        {
            var list = service.GetFiles();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.FileTransfer = service.GetFiles();
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]

        public IActionResult Create(FileTransferModel model, IFormFile file)
        {
            try
            {
                //1. Generate a new unique filename
                string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);

                //2 get the absoulute path of the folder "UserFiles"
                string absolutePath = hostEnvironment.WebRootPath + "\\UserFiles\\" + newFilename;

                if(file != null)
                    {
                    string absolutePathWithFilename = hostEnvironment.WebRootPath + "\\UserFiles\\" + newFilename;

                    using (FileStream fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                        fs.Close();
                    }
                    model.FilePath = newFilename;
                }

                using (FileStream zipFile = System.IO.File.Open(absolutePath + ".zip", FileMode.Create))
                {
                    using (FileStream source1 = System.IO.File.Open(absolutePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var archive = new Archive(new ArchiveEntrySettings(null, new TraditionalEncryptionSettings(model.Password))))
                        {
                            archive.CreateEntry(newFilename, source1);

                            archive.Save(zipFile, new ArchiveSaveOptions() { Encoding = System.Text.Encoding.ASCII, ArchiveComment = "Files are compressed"});
                        }
                    }
                }

            service.AddFileTransfer(model);
                ViewBag.Message = "File added successfully";
                SendSimpleMessage(model, newFilename + ".zip");
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, "Error occured while uploading file" + file.FileName);
                ViewBag.Error = "File was not added due to an error. Please try again later.";
            }

            return View();

        }

        string domain = "http://ismaelbendaoud-001-site1.htempurl.com/UserFiles/";

        //URL for site: http://ismaelbendaoud-001-site1.htempurl.com/

        public void SendSimpleMessage(FileTransferModel file, string newFilename)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api","b90ffe48beac683db14c64f033438dba-1831c31e-af67ed02");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox6171ca2219c746e78ae3450a4e7e90fb.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "ismael.bendaoud@outlook.com");
            request.AddParameter("to", file.ReceiverEmail);
            request.AddParameter("subject", file.Title);
            request.AddParameter("text", file.Message + "\n The password is " + file.Password + "\n Please click on the link in order to download the file: \n" + domain + newFilename);
            request.Method = Method.POST;
            client.Execute(request);
        }
    }
}

