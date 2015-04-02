/** @file   NutritionModels.cs
 *  @author Ian Lamb
 *  @date   2012-10-25
 *  @ver    0.2
 *  @brief  Defines the models associated with nutrition and meal plans
 *          
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace Ignite.Models
{
    //
    // FITNESS SERVICE HELPER
    public class NutritionService : IgniteService
    {
        //public Ignite.NutritionReference.NutritionPlan NutritionPlan { get; set; }

        public NutritionService()
        {
            //
            // Change this if there is any change to the service's URL pattern after the root url and before the action
            // Example:      IgniteService.rootUrl       NutritionService.rootSuffix   Action
            //                          V                           V                  V
            //          [http://ignitedemo.cloudapp.net/][api/NutritionService.svc/][NutritionPlan/22]
            //
            //rootSuffix = "service/NutritionService.svc/";
            rootSuffix = "NutritionService.svc/";
        }

        //
        // Get routine by routine id
		//public async Task<Ignite.NutritionReference.NutritionPlan> GetNutritionPlanByID(string npId)
		//{
		//	var routine = new Ignite.NutritionReference.NutritionPlan();

		//	// Send request to server
		//	HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix + "NutritionPlan/" + npId, this.cancellationToken).Result;

		//	if (response.IsSuccessStatusCode)
		//	{
		//		// Parse the response body. Blocking!
		//		var json = await response.Content.ReadAsStringAsync();

		//		// Deserialize json to .net object
		//		routine = JsonConvert.DeserializeObject<NutritionService>(json).NutritionPlan;
		//	}
		//	else
		//	{
		//		Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
		//	}

		//	return routine;
		//}
    }

    //
    // VIEW MODELS
    //
    public class FoodModel
    {
        public Guid FoodId { get; set; }
        public string Name { get; set; }
    }

    public class RecipeModel
    {
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
    }
}