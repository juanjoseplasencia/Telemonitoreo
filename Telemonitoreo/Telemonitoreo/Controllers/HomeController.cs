using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemonitoreo.Business;

namespace Telemonitoreo.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ConfigurarMenues();
            return View();
        }

    }
}