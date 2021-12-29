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

namespace PresentationWebApp.Controllers
{
    [Authorize]
    public class FileTransferController : Controller
    {
        private IWebHostEnvironment hostEnvironment;
        private ILogger<FileTransferController> logger;
        private IFileTransferService service;


        public FileTransferController(ILogger<FileTransferController> _logger, IWebHostEnvironment _hostEnvironment, IFileTransferService _service)
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
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]

        public IActionResult Create(FileTransferModel model, IFormFile file)
        {
            try
            {
                if (string.IsNullOrEmpty(model.ReceiverEmail))
                {
                    ViewBag.Error = "Name should not be left empty";
                }
                else
                {
                    if(file != null)
                    {
                        //save the file

                        //1. Generate a new unique filename
                        string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);

                        //2 get the absoulute path of the folder "UserFiles"
                        string absolutePath = hostEnvironment.WebRootPath + "\\UserFiles\\" + newFilename;

                        //3. save the file into that absolute file
                        using (FileStream fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
                        {
                            file.CopyTo(fs);
                            fs.Close();
                        }
                        model.FilePath = "\\UserFiles\\" + newFilename;
                    }

                    service.AddFileTransfer(model);
                    ViewBag.Message = "File added successfully";
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, "Error occured while uploading file" + file.FileName);
                ViewBag.Error = "Blog was not added due to an error. try later";
            }

            return View();
        }

        string domain = "http://ismaelbendaoud-001-site1.htempurl.com";

        public void SendSimpleMessage(FileTransferModel file, string newFilePath)
        {
       //https://app.mailgun.com/app/sending/domains/sandbox6171ca2219c746e78ae3450a4e7e90fb.mailgun.org


            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            "b90ffe48beac683db14c64f033438dba-1831c31e-af67ed02");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox6171ca2219c746e78ae3450a4e7e90fb.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "ismael.bendaoud@outlook.com");
            request.AddParameter("to",file.ReceiverEmail);

            request.AddParameter("subject", file.Title);
            request.AddParameter("text", file.Message + "\n The password is" + file.Password + "\n Please click on the link in order to download the file: \n" + domain + newFilePath); //it has to include the link to the file to be downloaded + the password
            request.Method = Method.POST;
            client.Execute(request);
        }
    }
}

