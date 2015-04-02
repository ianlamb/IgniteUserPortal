/** @file   FitnessController.cs
 *  @author Ian Lamb
 *  @date   2012-10-15
 *  @ver    0.3
 *  @brief  Defines the actions associated with fitness and exercises
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.IO;
using Ignite.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Ignite.Controllers
{

    [HandleError]
    [Authorize]
    public class FitnessController : Controller, IDisposable
    {
		//private FitnessService fitnessSvc;
		//private List<Ignite.FitnessReference.MuscleGroup> muscleGroups;
		//private List<Ignite.FitnessReference.Equipment> equipments;
		//private List<Ignite.FitnessReference.ForceType> forceTypes;
		//private List<Ignite.FitnessReference.MechanicType> mechanicTypes;
		//private List<Ignite.FitnessReference.LevelType> levelTypes;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            // FitnessModels.cs > FitnessService
            //fitnessSvc = new FitnessService();

            
        }

        // get all relevant exercise information and populate the viewbag with those options
        private void InitExerciseDropdowns()
        {
            try
            {
                // FitnessModels.cs > FitnessService.GetAllEquipment
				//equipments = fitnessSvc.GetAllEquipment().Result;
				//List<string> equipmentOptions = new List<string>();
				//foreach (var e in equipments)
				//{
				//	equipmentOptions.Add(e.Name);
				//}
				//ViewBag.EquipmentOptions = new SelectList(equipmentOptions.ToArray(), "Equipment");

				//// FitnessModels.cs > FitnessService.GetAllMuscleGroups
				//muscleGroups = fitnessSvc.GetAllMuscleGroups().Result;
				//List<string> muscleGroupOptions = new List<string>();
				//foreach (var m in muscleGroups)
				//{
				//	muscleGroupOptions.Add(m.Name);
				//}
				//ViewBag.MuscleGroupOptions = new SelectList(muscleGroupOptions, "MuscleGroup");

				//// FitnessModels.cs > FitnessService.GetAllForceTypes
				//forceTypes = fitnessSvc.GetAllForceTypes().Result;
				//List<string> forceOptions = new List<string>();
				//foreach (var m in forceTypes)
				//{
				//	forceOptions.Add(m.Name);
				//}
				//ViewBag.ForceOptions = new SelectList(forceOptions, "Force");

				//// FitnessModels.cs > FitnessService.GetAllLevelTypes
				//levelTypes = fitnessSvc.GetAllLevelTypes().Result;
				//List<string> levelOptions = new List<string>();
				//foreach (var m in levelTypes)
				//{
				//	levelOptions.Add(m.Name);
				//}
				//ViewBag.LevelOptions = new SelectList(levelOptions, "Level");

				//// FitnessModels.cs > FitnessService.GetAllMechanicTypes
				//mechanicTypes = fitnessSvc.GetAllMechanicTypes().Result;
				//List<string> mechanicsOptions = new List<string>();
				//foreach (var m in mechanicTypes)
				//{
				//	mechanicsOptions.Add(m.Name);
				//}
				//ViewBag.MechanicsOptions = new SelectList(mechanicsOptions, "Mechanics");
            }
            catch (Exception ex)
            {
                IgniteHelper.LogError(ex);
            }
        }

        //
        // GET: /Fitness/MyFitnessPlan
        public ActionResult MyFitnessPlan()
        {
            var routine = new RoutineViewModel();

			//try
			//{
			//	// FitnessModels.cs > FitnessService.GetRoutine
			//	//Ignite.FitnessReference.Routine r = fitnessSvc.GetRoutine("3F36C3F3-7780-4B94-BCD9-97B450A6BF18").Result;

			//	// map data to view model
			//	routine.RoutineId = r.RoutineID;
			//	routine.UserId = r.UserID;
			//	routine.Name = r.Name;
			//	routine.Goal = "Weight Loss";
			//	routine.CurrentWeight = "190";
			//	foreach (var d in r.Days.ToList())
			//	{
			//		var newDay = new DayListViewModel();
			//		newDay.DayId = d.RoutineDayID;
			//		newDay.DayNumber = d.DayNumber;
			//		foreach (var pe in d.Exercises.ToList())
			//		{
			//			var e = fitnessSvc.GetPermanentExercise(pe.PermanentExerciseID).Result;
			//			//var pe = new Ignite.FitnessReference.PermanentExercise();
			//			pe.VideoURL = "http://www.youtube.com/embed/6hmdvQv-SYs";

			//			var newExercise = new ExerciseListViewModel();
			//			newExercise.ExerciseId = pe.PermanentExerciseID;
			//			newExercise.Name = pe.PermanentName;
			//			newExercise.VideoURL = pe.VideoURL;
			//			newExercise.Thumbnail = pe.Thumbnail;
			//			newExercise.Sets = new List<SetListViewModel>();
			//			foreach (var set in pe.Sets)
			//			{
			//				var newSet = new SetListViewModel();
			//				newSet.SetID = set.SetID;
			//				newSet.SetNumber = set.SetNumber;
			//				newSet.RepNumber = set.RepNumber;
			//				newSet.Intensity = set.Intensity;
			//				newSet.ExtensionData = set.ExtensionData;
			//				newExercise.Sets.Add(newSet);
			//			}
			//			newExercise.Equipment = e.Equipment;
			//			// not sure why some of these attributes aren't showing up in the exercise model
			//			newExercise.Force = e.Force;
			//			newExercise.Level = e.Level;
			//			newExercise.Mechanics = e.Mechanics;
			//			newExercise.MuscleGroup = e.MuscleGroup;
			//			newExercise.Rating = "5";  //e.Rating;
			//			newDay.Exercises.Add(newExercise);
			//		}
			//		routine.Days.Add(newDay);
			//	}
			//}
			//catch (Exception ex)
			//{
			//	Console.WriteLine(ex.Message);
			//	TempData["ErrorMessage"] = "Error retrieving the routine: \n" + ex.Message;
			//}
            return View(routine);
        }

        //
        // GET: /Fitness/ExerciseViewer
        public ActionResult ExerciseViewer()
        {
            var viewModel = new ExerciseViewerModel();

            return View(viewModel);
        }

        //
        // GET: /Fitness/ListExercises
        public ActionResult ListExercises(ExerciseSearchModel search)
        {
            var exerList = new List<ExerciseListViewModel>();
            //var exercises = new List<Ignite.FitnessReference.PermanentExercise>();
            //var PermanentExerciseSearch = new Ignite.FitnessReference.PermanentExerciseSearch();
			//PermanentExerciseSearch.Equipment = String.IsNullOrEmpty(search.Equipment) ? "" : search.Equipment;
			//PermanentExerciseSearch.MuscleGroup = String.IsNullOrEmpty(search.MuscleGroup) ? "" : search.MuscleGroup;
			//PermanentExerciseSearch.Offset = String.IsNullOrEmpty(search.Offset) ? "0" : search.Offset;
			//PermanentExerciseSearch.Force = "";
			//PermanentExerciseSearch.Level = "";
			//PermanentExerciseSearch.Mechanics = "";
			//PermanentExerciseSearch.Name = String.IsNullOrEmpty(search.Name) ? "" : search.Name;
			//PermanentExerciseSearch.PermanentExerciseID = "";
			//PermanentExerciseSearch.Rating = "";
			//PermanentExerciseSearch.Type = "";
            
			//try
			//{
			//	// FitnessModels.cs > FitnessService.GetAllPermanentExercises
			//	exercises = fitnessSvc.GetAllPermanentExercises(PermanentExerciseSearch).Result;

			//	// map data to view model
			//	foreach (var pe in exercises)
			//	{
			//		var newEx = new ExerciseListViewModel();
			//		newEx.ExerciseId = pe.PermanentExerciseID != null ? pe.PermanentExerciseID : "nullid";
			//		newEx.Name = pe.Name;
			//		newEx.Equipment = pe.Equipment;
			//		newEx.Force = pe.Force;
			//		newEx.Level = pe.Level;
			//		newEx.Mechanics = pe.Mechanics;
			//		newEx.MuscleGroup = pe.MuscleGroup;
			//		newEx.Rating = "5";  //pe.Rating;
			//		newEx.Type = pe.Type;
			//		newEx.VideoURL = pe.VideoURL;
			//		newEx.Thumbnail = pe.Thumbnail;
			//		exerList.Add(newEx);
			//	}
			//}
			//catch (Exception ex)
			//{
			//	Console.WriteLine(ex.Message);
			//	TempData["ErrorMessage"] = "Error retrieving the exercises: " + ex.Message;
			//}

            return PartialView("_ListExercises", exerList);
        }

        //
        // GET: /Fitness/ListEquipment
        public ActionResult ListEquipment()
        {
            var viewModel = new List<EquipmentListViewModel>();
			//var equipment = new List<Ignite.FitnessReference.Equipment>();

			//try
			//{
			//	// FitnessModels.cs > FitnessService.GetAllEquipment
			//	equipment = fitnessSvc.GetAllEquipment().Result;

			//	// map data to view model
			//	foreach (var eq in equipment)
			//	{
			//		var newEquip = new EquipmentListViewModel();
			//		newEquip.EquipmentId = eq.EquipmentID != null ? eq.EquipmentID : "nullid";
			//		newEquip.Name = eq.Name;
			//		newEquip.Thumbnail = eq.ThumbnailURL;
			//		viewModel.Add(newEquip);
			//	}
			//}
			//catch (Exception ex)
			//{
			//	IgniteHelper.LogError(ex);
			//	TempData["ErrorMessage"] = "Error retrieving exercise list: " + ex.Message;
			//}

            return PartialView("_ListEquipment", viewModel);
        }

        //
        // GET: /Fitness/RoutineManager
        [Authorize(Roles = "pt,admin")]
        public ActionResult RoutineManager()
        {
            var viewModel = new List<RoutineViewModel>();

            try
            {
                // TODO: everything
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(viewModel);
        }

        //
        // GET: /Fitness/ExerciseManager
        [Authorize(Roles = "admin")]
        public ActionResult ExerciseManager(string searchString = "")
        {
            var viewModel = new List<ExerciseListViewModel>();
			//var exercises = new List<Ignite.FitnessReference.PermanentExercise>();
			//var PermanentExerciseSearch = new Ignite.FitnessReference.PermanentExerciseSearch();
			//PermanentExerciseSearch.Equipment = "";
			//PermanentExerciseSearch.Force = "";
			//PermanentExerciseSearch.Level = "";
			//PermanentExerciseSearch.Mechanics = "";
			//PermanentExerciseSearch.MuscleGroup = "";
			//PermanentExerciseSearch.Name = searchString;
			//PermanentExerciseSearch.Offset = "0";
			//PermanentExerciseSearch.PermanentExerciseID = "";
			//PermanentExerciseSearch.Rating = "";
			//PermanentExerciseSearch.Type = "";

			//try
			//{
			//	// FitnessModels.cs > FitnessService.GetAllPermanentExercises
			//	exercises = fitnessSvc.GetAllPermanentExercises(PermanentExerciseSearch).Result;

			//	// map data to view model
			//	foreach (var pe in exercises)
			//	{
			//		var newEx = new ExerciseListViewModel();
			//		newEx.ExerciseId = pe.PermanentExerciseID != null ? pe.PermanentExerciseID : "nullid";
			//		newEx.Name = pe.Name;
			//		newEx.Equipment = pe.Equipment;
			//		newEx.Force = pe.Force;
			//		newEx.Level = pe.Level;
			//		newEx.Mechanics = pe.Mechanics;
			//		newEx.MuscleGroup = pe.MuscleGroup;
			//		newEx.Rating = pe.Rating;
			//		newEx.Type = pe.Type;
			//		newEx.Thumbnail = pe.Thumbnail;
			//		newEx.VideoURL = pe.VideoURL;
			//		viewModel.Add(newEx);
			//	}
			//}
			//catch (Exception ex)
			//{
			//	IgniteHelper.LogError(ex);
			//	TempData["ErrorMessage"] = "Error retrieving exercise list: " + ex.Message;
			//}

            return View(viewModel);
        }

        //
        // GET: /Fitness/CreatePermanentExercise
        [Authorize(Roles = "admin")]
        public ActionResult CreatePermanentExercise()
        {
            var viewModel = new ExerciseListViewModel();

            InitExerciseDropdowns();

            return PartialView("_CreatePermanentExercise", viewModel);
        }

        //
        // GET: /Fitness/EditPermanentExercise
        [Authorize(Roles = "admin")]
        public ActionResult EditPermanentExercise(string id)
        {
            var exercise = new ExerciseListViewModel();
			//var dbExercise = new Ignite.FitnessReference.PermanentExercise();
            
			//InitExerciseDropdowns();

			//try
			//{
			//	// FitnessModels.cs > FitnessService.GetPermanentExercise
			//	dbExercise = fitnessSvc.GetPermanentExercise(id).Result;

			//	exercise.ExerciseId = dbExercise.PermanentExerciseID != null ? dbExercise.PermanentExerciseID : "nullid";
			//	exercise.Name = dbExercise.Name;
			//	exercise.Equipment = dbExercise.Equipment;
			//	exercise.Force = dbExercise.Force;
			//	exercise.Level = dbExercise.Level;
			//	exercise.Mechanics = dbExercise.Mechanics;
			//	exercise.MuscleGroup = dbExercise.MuscleGroup;
			//	exercise.Rating = dbExercise.Rating;
			//	exercise.Type = dbExercise.Type;
			//	exercise.Thumbnail = dbExercise.Thumbnail;
			//	exercise.VideoURL = dbExercise.VideoURL;
			//}catch(Exception ex){
			//	IgniteHelper.LogError(ex);
			//}

            return PartialView("_EditPermanentExercise", exercise);
        }

        //
        // POST: /Fitness/CreatePermanentExercise
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreatePermanentExercise(ExerciseListViewModel exercise)
        {
            InitExerciseDropdowns();
            var newEx = new CreatePermanentExerciseModel();

            // save thumbnail
            if (exercise.ThumbnailFile != null && exercise.ThumbnailFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(exercise.ThumbnailFile.FileName);

                // generate a file name on the server based on a combination of the GymId and a timestap
                string serverFile = "ex-" + DateTime.Now.ToString("yymmdd-Hmmss") + Path.GetExtension(exercise.ThumbnailFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/uploads"), serverFile);
                try
                {
                    // save the file to the specified location on the server
                    exercise.ThumbnailFile.SaveAs(path);
                    // assign the generated filename to the user's profile
                    exercise.Thumbnail = "/Content/uploads/" + serverFile;
                }
                catch (Exception ex)
                {
                    IgniteHelper.LogError(ex);
                }
            }

            // map new model
            newEx.Name = exercise.Name;
			//newEx.MuscleID = muscleGroups.Find(m => m.Name == exercise.MuscleGroup).MuscleGroupID;
			//newEx.EquipmentID = equipments.Find(m => m.Name == exercise.Equipment).EquipmentID;
			//newEx.ForceID = forceTypes.Find(m => m.Name == exercise.Force).ForceTypeID;
			//newEx.LevelID = levelTypes.Find(m => m.Name == exercise.Level).LevelTypeID;
			//newEx.MechanicID = mechanicTypes.Find(m => m.Name == exercise.Mechanics).MechanicTypeID;
			//newEx.Thumbnail = exercise.Thumbnail;
			//newEx.VideoURL = exercise.VideoURL; // todo: video uploads

			//// FitnessModels.cs > FitnessService.CreatePermanentExercise
			//if (fitnessSvc.CreatePermanentExercise(newEx).Result)
			//	TempData["SuccessMessage"] = "New workout created: " + newEx.Name;
			//else
			//	TempData["ErrorMessage"] = "Error creating workout.";

            return RedirectToAction("ExerciseManager");
        }

        //
        // POST: /Fitness/EditPermanentExercise
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditPermanentExercise(ExerciseListViewModel exercise)
        {
            InitExerciseDropdowns();
            var newEx = new EditPermanentExerciseModel();

            // save  thumbnail
            if (exercise.ThumbnailFile != null && exercise.ThumbnailFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(exercise.ThumbnailFile.FileName);

                // generate a file name on the server based on a combination of the GymId and a timestap
                string serverFile = "ex-" + DateTime.Now.ToString("yymmdd-Hmmss") + Path.GetExtension(exercise.ThumbnailFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/uploads"), serverFile);
                try
                {
                    // save the file to the specified location on the server
                    exercise.ThumbnailFile.SaveAs(path);
                    // assign the generated path to the exercise
                    exercise.Thumbnail = "/Content/uploads/" + serverFile;
                }
                catch (Exception ex)
                {
                    IgniteHelper.LogError(ex);
                }
            }

            // map new model
            newEx.PermanentExerciseID = exercise.ExerciseId;
            newEx.Name = exercise.Name;
			//newEx.MuscleID = muscleGroups.Find(m => m.Name == exercise.MuscleGroup).MuscleGroupID;
			//newEx.EquipmentID = equipments.Find(m => m.Name == exercise.Equipment).EquipmentID;
			//newEx.ForceID = forceTypes.Find(m => m.Name == exercise.Force).ForceTypeID;
			//newEx.LevelID = levelTypes.Find(m => m.Name == exercise.Level).LevelTypeID;
			//newEx.MechanicID = mechanicTypes.Find(m => m.Name == exercise.Mechanics).MechanicTypeID;
			//newEx.Thumbnail = exercise.Thumbnail;
			//newEx.VideoURL = exercise.VideoURL; // todo: video uploads

			//// FitnessModels.cs > FitnessService.EditPermanentExercise
			//if (fitnessSvc.EditPermanentExercise(newEx).Result)
			//	TempData["SuccessMessage"] = "Updated workout: " + newEx.Name;
			//else
			//	TempData["ErrorMessage"] = "Error updating workout.";

            return RedirectToAction("ExerciseManager");
        }

        //
        // POST: /Fitness/UpdateSet
		//[HttpPost]
		//public bool UpdateSet(SetListViewModel set)
		//{
		//	bool setComplete = set.Complete;

		//	return setComplete;
		//}
    }
}
