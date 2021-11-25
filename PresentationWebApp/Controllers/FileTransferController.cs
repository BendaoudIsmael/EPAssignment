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
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
