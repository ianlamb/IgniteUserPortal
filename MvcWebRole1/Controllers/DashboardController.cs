/** @file   DashboardController.cs
 *  @author Ian Lamb
 *  @date   2012-10-15
 *  @ver    0.2
 *  @brief  Defines the actions associated with the dashboard (analytics)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ignite.Models;

namespace Ignite.Controllers
{
    [HandleError]
    [Authorize]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/Main
        [Authorize]
        public ActionResult Main()
        {
            if(Request.RequestContext.HttpContext.User.IsInRole("admin"))
                return RedirectToAction("Admin");
            if (Request.RequestContext.HttpContext.User.IsInRole("pt"))
                return RedirectToAction("Trainer");
            else
                return RedirectToAction("Client");
        }

        //
        // GET: /Dashboard/Admin
        [Authorize]
        public ActionResult Admin()
        {
            var model = new DashAdminViewModel();

            return View(model);
        }

        //
        // GET: /Dashboard/Trainer
        [Authorize]
        public ActionResult Trainer()
        {
            var model = new DashPTViewModel();
            model.Stats.ClientsOnline = Membership.GetNumberOfUsersOnline();

            return View(model);
        }

        //
        // GET: /Dashboard/Client
        [Authorize]
        public ActionResult Client()
        {
            var model = new DashClientViewModel();

            return View(model);
        }

        //
        // GET: /Dashboard/App
        [Authorize]
        public ActionResult App()
        {
            return View();
        }

        //
        // GET: /Dashboard/News
        [Authorize]
        public ActionResult News()
        {
            return PartialView("_News");
        }
    }
}
