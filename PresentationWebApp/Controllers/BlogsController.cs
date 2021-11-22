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
using Microsoft.AspNetCore.Authorization;

namespace PresentationWebApp.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private IBlogservice service;
        private ICategoryService categoryService;
        private IWebHostEnvironment hostEnviorment;

        public BlogsController(IBlogservice _service, ICategoryService _categoryService, IWebHostEnvironment _hostEnviorment)
            {
            service = _service;
            categoryService = _categoryService;
            hostEnviorment = _hostEnviorment;
            }


        public IActionResult Index()
        {
            var list = service.GetBlogs();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var b = service.GetBlog(id);
            return View (b);
        }

        public IActionResult Create()
        {
            var list = categoryService.GetCategories();
            ViewBag.Categories = list;

            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public IActionResult Create(BlogCreationModel model, IFormFile logoFile)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    ViewBag.Error = "Name should not be left empty";
                }
                else
                {
                    if(logoFile != null)
                    {
                        //save the file

                        //1. genereate a new UNIQUE filename for the file
                        string newfilename = Guid.NewGuid() + System.IO.Path.GetExtension(logoFile.FileName); //genereate a serial number which will be unique

                        //2. get the absoulute path of the folder "Files"
                        string absolutePath = hostEnviorment.WebRootPath + "\\Files\\" + newfilename;

                        //3. save the file into the absoulute Path
                        using(FileStream fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
                        {
                            logoFile.CopyTo(fs);
                            fs.Close();
                        }
                        model.LogoImagePath = "\\Files\\" + newfilename;
                    }
                    service.AddBlog(model);
                    ViewBag.Message = "Blog added successfully";
                }  
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Blog was not added due to an error. try later";
            }

            var list = categoryService.GetCategories();
            ViewBag.Categories = list;
            return View();
        }

        public IActionResult Delete (int id)
        {
            try
            {
                var blog = service.GetBlog(id);
                string absoulutePathOfImageToDelete = hostEnviorment.WebRootPath + blog.LogoImagePath;

                service.DeleteBlog(id);

                System.IO.File.Delete(absoulutePathOfImageToDelete);
                //ViewBag.Message = "Blog deleted successfully";

                TempData["Message"] = "Blog deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            //ViewBag does not survive a redirection
            //TempData survives a redirection
            return RedirectToAction("Index");
        }
    }
}
