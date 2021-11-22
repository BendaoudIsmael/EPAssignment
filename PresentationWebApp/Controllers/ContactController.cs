using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationWebApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HandleQuery(string query)
        {
           
            ViewBag.Message = "Your query was recieved";

            //process the query e.g save it in the Database. Or send out an email
            return View("Index"); //this sends back the index.cshtml page to the user
                                  
        }
    }
}
