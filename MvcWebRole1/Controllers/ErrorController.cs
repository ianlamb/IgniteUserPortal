using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ignite.Controllers
{
    public class ErrorController : Controller
    {

        //
        // GET: /Static/PageNotFound
        public ActionResult PageNotFound()
        {
            return View();
        }

    }
}
