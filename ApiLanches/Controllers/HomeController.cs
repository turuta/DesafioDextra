using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApiLanches.Models;

namespace ApiLanches.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
           
            var ctx = new DataContext();
            return View(ctx.Lanches);
        }

        public ActionResult Help()
        {

            return View();
        }
    }
}
