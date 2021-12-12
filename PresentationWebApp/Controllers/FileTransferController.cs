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

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]

        public IActionResult Create(FileCreationModel model, IFormFile logoFile)
        {
            try
            {
                logger.Log(LogLevel.Information, "User accessed the Create method");

                if (string.IsNullOrEmpty(model.Email))
                {
                    ViewBag.Error = "Name should not be left empty";
                }
                else
                {
                    if (logoFile != null)
                    {
                        string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(logoFile.FileName);
                        logger.Log(LogLevel.Information, $"Guid generated for file {logoFile.FileName} is {newFilename}");

                        string absolutePath = hostEnvironment.WebRootPath + "\\Files\\" + newFilename;
                        logger.Log(LogLevel.Information, $"Absolute path read is {absolutePath}");
                     

                        using (FileStream fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
                        {
                            logoFile.CopyTo(fs);
                            fs.Close();
                        }

                        logger.Log(LogLevel.Information, "File was saved successfully");
                        model.FilePath = "\\Files\\" + newFilename;
                    }

                    service.AddFileTransfer(model);
                    ViewBag.Message = "Blog added successfully";
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, "Error occurred while uploading file " + logoFile.FileName);
                ViewBag.Error = "Blog was not added due to an error. try later";
            }

            return View();
        }
    }
}

