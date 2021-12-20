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

namespace PresentationWebApp.Controllers
{
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
    }
}

