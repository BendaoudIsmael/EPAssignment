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

        public FileTransferController(ILogger<FileTransferController> _logger, IWebHostEnvironment _hostEnvironment)
        {
            logger = _logger;
            hostEnvironment = _hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
