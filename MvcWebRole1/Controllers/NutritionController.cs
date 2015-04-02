/** @file   NutritionController.cs
 *  @author Ian Lamb
 *  @date   2012-10-15
 *  @ver    0.2
 *  @brief  Defines the actions associated with nutrition and meal plans
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
    [Authorize]
    public class NutritionController : Controller
    {
        private NutritionService nutritionSvc = null;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            nutritionSvc = new NutritionService();
        }

        //
        // GET: /Nutrition/MyNutritionPlan
        public ActionResult MyNutritionPlan()
        {
            //Ignite.NutritionReference.NutritionPlan nutritionPlan = nutritionSvc.GetNutritionPlanByID("6F8D487D-D803-4E7A-88F7-BD1561C1AA1D").Result;
            
            return View();
        }

        //
        // GET: /Nutrition/NutritionManager
        [Authorize(Roles = "pt,admin")]
        public ActionResult NutritionManager()
        {
            return View();
        }
    }
}
