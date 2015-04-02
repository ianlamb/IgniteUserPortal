/** @file   FitnessModels.cs
 *  @author Ian Lamb
 *  @date   2012-10-25
 *  @ver    0.2
 *  @brief  Defines the models associated with fitness and exercises
 *          Routines, Days, Exercises, Equipment
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace Ignite.Models
{
    public class ExerciseWrapper
    {
    }

    //
    // FITNESS SERVICE HELPER
	//public class FitnessService : IgniteService
	//{
	//	// Because of the way json objects are returned by the service in an anonymous wrapper object {},
	//	// then a named object (ie. MuscleGroup), we actually use the FitnessService as the base object to 
	//	// deserialize to and then grab the matching property for the named object (that's why these properties exist)
	//	public ExerciseWrapper Exercise { get; set; }

	//	public FitnessService()
	//	{
	//		//
	//		// Change this if there is any change to the service's URL pattern after the root url and before the action
	//		// Example:      IgniteService.rootUrl       FitnessService.rootSuffix   Action
	//		//                          V                           V                  V
	//		//          [http://ignitedemo.cloudapp.net/][api/FitnessService.svc/][routine/22]
	//		//
	//		//rootSuffix = "service/FitnessService.svc/";
	//		rootSuffix = "FitnessService.svc/";
	//	}

	//	//
	//	// Get routine by routine id
	//	public async Task<Ignite.FitnessReference.Routine> GetRoutine(string routineId)
	//	{
	//		var routine = new Ignite.FitnessReference.Routine();

	//		// Send request to server
	//		HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix+"routine/"+routineId, this.cancellationToken).Result;

	//		if (response.IsSuccessStatusCode)
	//		{
	//			// Parse the response body. Blocking!
	//			var json = await response.Content.ReadAsStringAsync();

	//			// We deserialize into the FitnessService object because the json comes back wrapped in an anonymous object {}
	//			// Then the relevent object name (in this case "Routine") has to match up with a property in FitnessService
	//			// We can then extract the deserialized object and copy it to our local routine object and then send it back to the controller
	//			routine = JsonConvert.DeserializeObject<FitnessService>(json).Routine;
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//		}

	//		return routine;
	//	}

	//	// Get list of routine partials (doesn't include days and other sub-classes)
	//	public async Task<Ignite.FitnessReference.Routine> GetRoutineList(string userId)
	//	{
	//		var routine = new Ignite.FitnessReference.Routine();

	//		// Send request to server
	//		HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix+"routine/personal_trainer/"+userId, this.cancellationToken).Result;

	//		if (response.IsSuccessStatusCode)
	//		{
	//			// Parse the response body. Blocking!
	//			var json = await response.Content.ReadAsStringAsync();
	//			routine = JsonConvert.DeserializeObject<FitnessService>(json).Routines.First();
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//		}

	//		return routine;
	//	}

	//	//
	//	// Search PermanentExercises
	//	// POST FitnessService.svc/permanentExercises
	//	public async Task<List<Ignite.FitnessReference.PermanentExercise>> GetAllPermanentExercises(
	//		Ignite.FitnessReference.PermanentExerciseSearch PermanentExerciseSearch = null)
	//	{
	//		var exercises = new List<Ignite.FitnessReference.PermanentExercise>();

	//		try
	//		{
	//			// The search object can't have null values currently
	//			if (PermanentExerciseSearch == null)
	//			{
	//				PermanentExerciseSearch = new Ignite.FitnessReference.PermanentExerciseSearch();
	//				PermanentExerciseSearch.Equipment = "";
	//				PermanentExerciseSearch.Force = "";
	//				PermanentExerciseSearch.Level = "";
	//				PermanentExerciseSearch.Mechanics = "";
	//				PermanentExerciseSearch.MuscleGroup = "Lats";
	//				PermanentExerciseSearch.Name = "";
	//				PermanentExerciseSearch.Offset = "0";
	//				PermanentExerciseSearch.PermanentExerciseID = "";
	//				PermanentExerciseSearch.Rating = "";
	//				PermanentExerciseSearch.Type = "";
	//			}

	//			/* this is all useless now, because apparently you can just pass .net objects into PostAsJsonAsync() -_-
	//			var settings = new JsonSerializerSettings { ContractResolver = new SpecialContractResolver() };
	//			string searchJson = JsonConvert.SerializeObject(PermanentExerciseSearch, Formatting.None, settings);

	//			// Build the content object to send in the post request
	//			MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
	//			HttpContent content = new ObjectContent<Ignite.FitnessReference.PermanentExerciseSearch>(PermanentExerciseSearch, jsonFormatter, "application/json");
	//			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

	//			searchJson = "{ \"PermanentExerciseSearch\": { \"Equipment\": \"\", \"Force\": \"\", \"Level\": \"\", \"Mechanics\": \"\", \"MuscleGroup\": \"Lats\", \"Name\": \"\", \"PermanentExerciseID\": \"\", \"Rating\": \"\", \"Type\": \"\", \"Offset\" : \"0\" } }";
	//			var postString = new StringContent(
	//				searchJson,
	//				System.Text.Encoding.UTF8,
	//				"application/json");
	//			*/

	//			// Send request to server
	//			HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix + "permanentExercises", new { PermanentExerciseSearch }).Result;

	//			if (response.IsSuccessStatusCode)
	//			{
	//				// Parse the response body. Blocking!
	//				var json = await response.Content.ReadAsStringAsync();
	//				exercises = JsonConvert.DeserializeObject<FitnessService>(json).PermanentExercise;
	//			}
	//			else
	//			{
	//				Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//			}
	//		}
	//		catch (Exception ex)
	//		{
	//			IgniteHelper.LogError(ex);
	//			throw new Exception(ex.Message);
	//		}

	//		return exercises;
	//	}

	//	//
	//	// Get Exercise
	//	// POST FitnessService.svc/exercise/{id}
	//	public async Task<Ignite.FitnessReference.Exercise> GetExercise(string exerciseId)
	//	{
	//		var exercise = new Ignite.FitnessReference.Exercise();

	//		// Send request to server
	//		HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix + "exercise/" + exerciseId, this.cancellationToken).Result;

	//		if (response.IsSuccessStatusCode)
	//		{
	//			// Parse the response body. Blocking!
	//			var json = await response.Content.ReadAsStringAsync();
	//			exercise = JsonConvert.DeserializeObject<FitnessService>(json).Exercise.Exercise;
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//		}

	//		return exercise;
	//	}

	//	//
	//	// Get PermanentExercise
	//	// POST FitnessService.svc/permanentExercise/{id}
	//	public async Task<Ignite.FitnessReference.PermanentExercise> GetPermanentExercise(string exerciseId)
	//	{
	//		var exercise = new Ignite.FitnessReference.PermanentExercise();
	//		var json = "";

	//		// Send request to server
	//		HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix + "permanentExercise/" + exerciseId, this.cancellationToken).Result;

	//		if (response.IsSuccessStatusCode)
	//		{
	//			// Parse the response body. Blocking!
	//			json = await response.Content.ReadAsStringAsync();
	//			exercise = JsonConvert.DeserializeObject<ExerciseWrapper>(json).PermanentExercise;
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//		}

	//		return exercise;
	//	}

	//	//
	//	// Create PermanentExercise
	//	// PUT FitnessService.svc/permanentExercise
	//	public async Task<bool> CreatePermanentExercise(CreatePermanentExerciseModel PermanentExercise)
	//	{
	//		// Send request to server
	//		// Remember the request header needs to match the service API, so in this case we use Put instead of Post in the json call.
	//		HttpResponseMessage response = this.client.PutAsJsonAsync(rootSuffix + "permanentExercise", new { PermanentExercise }).Result;

	//		if (response.IsSuccessStatusCode)
	//		{
	//			var json = await response.Content.ReadAsStringAsync();
	//			// todo: check response code
	//			return true;
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//			return false;
	//		}
	//	}

	//	//
	//	// Update PermanentExercise
	//	// PUT FitnessService.svc/permanentExercise/update
	//	public async Task<bool> EditPermanentExercise(EditPermanentExerciseModel PermanentExercise)
	//	{
	//		// Send request to server
	//		// Remember the request header needs to match the service API, so in this case we use Put instead of Post in the json call.
	//		HttpResponseMessage response = this.client.PutAsJsonAsync(rootSuffix + "permanentExercise/update", new { PermanentExercise }).Result;

	//		if (response.IsSuccessStatusCode)
	//		{
	//			var json = await response.Content.ReadAsStringAsync();
	//			// todo: check response code
	//			return true;
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//			return false;
	//		}
	//	}

	//	//
	//	// GetAllEquipment
	//	// POST FitnessService.svc/equipments
	//	public async Task<List<Ignite.FitnessReference.Equipment>> GetAllEquipment()
	//	{
	//		var equip = new List<Ignite.FitnessReference.Equipment>();

	//		// Send request to server
	//		HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix + "equipments", this.cancellationToken).Result;

	//		if (response.IsSuccessStatusCode)
	//		{
	//			// Parse the response body. Blocking!
	//			var json = await response.Content.ReadAsStringAsync();
	//			equip = JsonConvert.DeserializeObject<FitnessService>(json).Equipment;
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//		}

	//		return equip;
	//	}

	//	//
	//	// GetAllMuscleGroups
	//	// POST FitnessService.svc/muscleGroup
	//	public async Task<List<Ignite.FitnessReference.MuscleGroup>> GetAllMuscleGroups()
	//	{
	//		var list = new List<Ignite.FitnessReference.MuscleGroup>();
	//		HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix + "muscleGroup", this.cancellationToken).Result;
	//		if (response.IsSuccessStatusCode)
	//		{
	//			var json = await response.Content.ReadAsStringAsync();
	//			list = JsonConvert.DeserializeObject<FitnessService>(json).MuscleGroup;
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//		}
	//		return list;
	//	}

	//	//
	//	// GetAllLevelTypes
	//	// POST FitnessService.svc/levelType
	//	public async Task<List<Ignite.FitnessReference.LevelType>> GetAllLevelTypes()
	//	{
	//		var list = new List<Ignite.FitnessReference.LevelType>();
	//		HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix + "levelType", this.cancellationToken).Result;
	//		if (response.IsSuccessStatusCode)
	//		{
	//			var json = await response.Content.ReadAsStringAsync();
	//			list = JsonConvert.DeserializeObject<FitnessService>(json).LevelType;
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//		}
	//		return list;
	//	}

	//	//
	//	// GetAllMechanicTypes
	//	// POST FitnessService.svc/mechanicType
	//	public async Task<List<Ignite.FitnessReference.MechanicType>> GetAllMechanicTypes()
	//	{
	//		var list = new List<Ignite.FitnessReference.MechanicType>();
	//		HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix + "mechanicType", this.cancellationToken).Result;
	//		if (response.IsSuccessStatusCode)
	//		{
	//			var json = await response.Content.ReadAsStringAsync();
	//			list = JsonConvert.DeserializeObject<FitnessService>(json).MechanicType;
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//		}
	//		return list;
	//	}

	//	//
	//	// GetAllForceTypes
	//	// POST FitnessService.svc/forceType
	//	public async Task<List<Ignite.FitnessReference.ForceType>> GetAllForceTypes()
	//	{
	//		var list = new List<Ignite.FitnessReference.ForceType>();
	//		HttpResponseMessage response = this.client.PostAsJsonAsync(rootSuffix + "forceType", this.cancellationToken).Result;
	//		if (response.IsSuccessStatusCode)
	//		{
	//			var json = await response.Content.ReadAsStringAsync();
	//			list = JsonConvert.DeserializeObject<FitnessService>(json).ForceType;
	//		}
	//		else
	//		{
	//			Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
	//		}
	//		return list;
	//	}
    //}

    //
    // VIEW MODELS
    //
    public class RoutineViewModel
    {
        public string RoutineId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Goal { get; set; }
        public string CurrentWeight { get; set; }
        public List<DayListViewModel> Days { get; set; }

        public RoutineViewModel()
        {
            Days = new List<DayListViewModel>();
        }
    }

    public class DayListViewModel
    {
        public string DayId { get; set; }
        public string DayNumber { get; set; }
        public List<ExerciseListViewModel> Exercises { get; set; }

        public DayListViewModel()
        {
            Exercises = new List<ExerciseListViewModel>();
        }
    }

	//public class SetListViewModel : Ignite.FitnessReference.Set
	//{
	//	public Boolean Complete { get; set; }
	//}

    public class ExerciseViewerModel
    {
        public List<ExerciseListViewModel> Exercises { get; set; }
        public List<EquipmentListViewModel> Equipment { get; set; }

        public ExerciseViewerModel()
        {
            Exercises = new List<ExerciseListViewModel>();
            Equipment = new List<EquipmentListViewModel>();
        }
    }

    public class ExerciseManagerModel
    {
        public List<ExerciseListViewModel> Exercises { get; set; }

        public ExerciseManagerModel()
        {
            Exercises = new List<ExerciseListViewModel>();
        }
    }

    public class ExerciseListViewModel
    {
        public string ExerciseId { get; set; }
        public string Name { get; set; }
        public string Equipment { get; set; }
        public string Force { get; set; }
        public string Level { get; set; }
        public string Mechanics { get; set; }
        public string MuscleGroup { get; set; }
        public string Rating { get; set; }
        public string Type { get; set; }
        public string VideoURL { get; set; }
        public string Thumbnail { get; set; }
        public HttpPostedFileBase ThumbnailFile { get; set; }
        //public List<SetListViewModel> Sets { get; set; }
    }

    public class CreatePermanentExerciseModel
    {
        public string Name { get; set; }
        public string EquipmentID { get; set; }
        public string ForceID { get; set; }
        public string MechanicID { get; set; }
        public string MuscleID { get; set; }
        public string LevelID { get; set; }
        public string VideoURL { get; set; }
        public string Thumbnail { get; set; }
    }

    public class EditPermanentExerciseModel
    {
        public string PermanentExerciseID { get; set; }
        public string Name { get; set; }
        public string EquipmentID { get; set; }
        public string ForceID { get; set; }
        public string MechanicID { get; set; }
        public string MuscleID { get; set; }
        public string LevelID { get; set; }
        public string VideoURL { get; set; }
        public string Thumbnail { get; set; }
    }

    public class EquipmentListViewModel
    {
        public string EquipmentId { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }

        public EquipmentListViewModel() { }
        public EquipmentListViewModel(string name, string thumb)
        {
            Name = name;
            Thumbnail = thumb;
        }
    }

    public class ExerciseSearchModel
    {
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public string Equipment { get; set; }
        public string Offset { get; set; }

        public ExerciseSearchModel()
        {
            Name = "";
            MuscleGroup = "";
            Equipment = "";
            Offset = "0";
        }
    }
}