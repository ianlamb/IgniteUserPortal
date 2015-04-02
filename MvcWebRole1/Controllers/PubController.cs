/** @file   PubController.cs
 *  @author Ian Lamb
 *  @date   2012-10-15
 *  @ver    0.2
 *  @brief  Defines the actions associated static content like the home page, about, etc
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ignite.Models;

namespace Ignite.Controllers
{
    [HandleError]
    public class PubController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        //
        // GET: /Pub/Index
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Main", "Dashboard");

            return View();
        }

        //
        // GET: /Pub/About
        public ActionResult About()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Main", "Dashboard");

            return View();
        }

        //
        // GET: /Pub/Careers
        public ActionResult Careers()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Main", "Dashboard");

            return View();
        }
    }
}
