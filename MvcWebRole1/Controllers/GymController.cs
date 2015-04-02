/** @file   GymController.cs
 *  @author Ian Lamb
 *  @date   2012-10-25
 *  @ver    0.2
 *  @brief  Defines the models associated with gym website management. This includes several aspects such as:
 *          Settings, Layout, Challenges, Exercises, Advertisements, Analytics.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Ignite.Models;
using System.IO;

namespace Ignite.Controllers
{
    [HandleError]
    [Authorize]
    public class GymController : Controller, IDisposable
    {
        //private FitnessService fitnessSvc = null;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            //fitnessSvc = new FitnessService();
        }

        //
        // GET: /Gym/Customizer
        [Authorize(Roles = "admin")]
        public ActionResult Customizer()
        {
            // TODO: get settings from service
            
            // dummy data
            var settings = new GymSettingsViewModel();
            settings.GymId = "asdf123";
            settings.GymName = "Igniteck Gym";
            settings.LogoURL = Session["LogoURL"] != null ? Session["LogoURL"].ToString() : "../images/logo.png";
            settings.UnitType = "imperial";
            settings.DropdownIncrement = 10;
            settings.ScreenTimeout = 300;
            settings.SessionTimeout = 600;
            settings.HeaderColour = Session["HeaderColour"] != null ? Session["HeaderColour"].ToString() : "#222222";
            settings.BodyColour = Session["BodyColour"] != null ? Session["BodyColour"].ToString() : "#ffffff";
            settings.AccentColour = Session["AccentColour"] != null ? Session["AccentColour"].ToString() : "#2AA6CA";

            // components
            string[] unitTypes = new string[2];
            unitTypes[0] = "Metric";
            unitTypes[1] = "Imperial";
            ViewBag.UnitTypes = new SelectList(unitTypes, "UnitType");

            return View(settings);
        }

        //
        // POST: /Gym/Settings
        [HttpPost]
        public ActionResult Settings(GymSettingsViewModel settings)
        {
            if (settings.LogoFile != null && settings.LogoFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(settings.LogoFile.FileName);

                // generate a file name on the server based on a combination of the GymId and a timestap
                string serverFile = settings.GymId + "-" + DateTime.Now.ToString("yymmdd-Hmmss") + Path.GetExtension(settings.LogoFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/uploads"), serverFile);
                try
                {
                    // save the file to the specified location on the server
                    settings.LogoFile.SaveAs(path);
                    Session["LogoURL"] = serverFile;
                }
                catch (Exception ex)
                {
                    IgniteHelper.LogError(ex);
                }
            }

            TempData["SuccessMessage"] = "Settings have been saved!";

            // TODO: Save settings to db

            // Set settings in the session
            Session["gymid"] = settings.GymId;
            Session["gymname"] = settings.GymName;
            Session["unittype"] = settings.UnitType;
            Session["dropdowninc"] = settings.DropdownIncrement;
            Session.Timeout = settings.SessionTimeout > 0 ? settings.SessionTimeout : 60;
            Session["HeaderColour"] = settings.HeaderColour;
            Session["BodyColour"] = settings.BodyColour;
            Session["AccentColour"] = settings.AccentColour;
            
            return RedirectToAction("Customizer");
        }

        //
        // GET: /Gym/ChallengeManager
        [Authorize(Roles = "admin")]
        public ActionResult ChallengeManager()
        {
            return View();
        }

        //
        // GET: /Gym/AdvertManager
        [Authorize(Roles = "admin")]
        public ActionResult AdvertManager()
        {
            return View();
        }
    }
}
