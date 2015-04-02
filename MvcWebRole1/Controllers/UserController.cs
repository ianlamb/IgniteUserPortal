/** @file   UserController.cs
 *  @author Ian Lamb
 *  @date   2012-10-15
 *  @ver    0.3
 *  @brief  Defines the actions associated with user management (including all roles such as admin, pt, etc.)
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
using System.IO;
using Ignite.Models;

namespace Ignite.Controllers
{
    [HandleError]
    [Authorize]
    public class UserController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        //
        // GET: /User/Create
        [Authorize(Roles = "pt,admin")]
        public ActionResult Create()
        {
            //string[] roles = Roles.GetAllRoles();
            List<string> availableRoles = new List<string>();
            availableRoles.Add("client");
            if (User.IsInRole("admin"))
            {
                availableRoles.Add("pt");
                availableRoles.Add("admin");
            }
            string[] roles = availableRoles.ToArray();

            ViewBag.Roles = new SelectList(roles, "Role");

            return PartialView("_Create", new CreateUserModel());
        }

        //
        // POST: /User/Create
        [Authorize(Roles = "pt,admin")]
        [HttpPost]
        public ActionResult Create(CreateUserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Attempt to register the user
                    MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        Roles.AddUserToRole(model.UserName, model.UserRole);

                        if(User.IsInRole("pt")) { // assign the user to the pt that made the request
                            UserViewModel user = new UserViewModel(model.UserName);
                            user.Profile.AssignedPT = User.Identity.Name;
                            user.Profile.Save();
                        }

                        TempData["SuccessMessage"] = "Success: Created user '" + model.UserName + "'.";

                        if (User.IsInRole("pt"))
                            return RedirectToAction("ClientManager");
                        else
                            return RedirectToAction("UserManager");
                    }
                    else
                    {
                        ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                    }
                }

            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Error: Failed to create user.";
                Console.WriteLine(ex.Message);
            }

            TempData["PasswordLength"] = MembershipService.MinPasswordLength;
            return RedirectToAction("UserManager");
        }

        //
        // GET: /User/Edit
        [Authorize(Roles = "pt,admin")]
        public ActionResult Edit(string id)
        {
            var user = new UserViewModel(id);

            return PartialView("_Edit", user);
        }

        //
        // POST: /User/Edit
        [Authorize(Roles = "pt,admin")]
        [HttpPost]
        public ActionResult Edit(EditUserModel model)
        {
            if (!Request.IsAuthenticated)
                return View("Error");

            var systemUser = Membership.GetUser(model.User.UserName);
            systemUser.Email = model.User.Email;

            try
            {
                Membership.UpdateUser(systemUser);
                TempData["SuccessMessage"] = "Success: User updated.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: Failed to update user.";
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("List");
        }

        //
        // GET: /User/Details
        public ActionResult Details(string id)
        {
            var user = new UserViewModel(id);

            return PartialView("_Details", user);
        }

        //
        // GET: /User/Delete
        [Authorize(Roles = "pt,admin")]
        public ActionResult Delete(string id)
        {
            UserViewModel model = new UserViewModel(id);

            return PartialView("_Delete", model);
        }

        //
        // POST: /User/Delete
        [Authorize(Roles = "pt,admin")]
        [HttpPost]
        public ActionResult Delete(DeleteUserModel model)
        {
            if (!Request.IsAuthenticated)
                return View("Error");

            bool status;

            if (ModelState.IsValid)
            {
                status = Membership.DeleteUser(model.User.UserName);

                if (status)
                {
                    TempData["SuccessMessage"] = "Success: User deleted.";
                    return RedirectToAction("UserManager");
                }
            }

            TempData["ErrorMessage"] = "Error: Failed to delete user.";
            return RedirectToAction("UserManager");
        }

        //
        // GET: /User/ClientManager
        [Authorize(Roles = "pt,admin")]
        public ActionResult ClientManager(string id, string searchString)
        {
            var model = new UserManagerViewModel();
            model.UserList = UserViewModel.GetUsersByPT(User.Identity.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                model.UserList = model.UserList.Where(u => u.User.UserName.ToUpper().Contains(searchString.ToUpper())
                                       || u.User.Email.ToUpper().Contains(searchString.ToUpper())
                                       || u.Profile.Role.ToUpper().Contains(searchString.ToUpper())
                                       || u.Profile.FullName.ToUpper().Contains(searchString.ToUpper())
                                   ).ToList();
            }

            model.SelectedUser = id;
            
            return View(model);
        }

        //
        // GET: /User/UserManager
        [Authorize(Roles = "admin")]
        public ActionResult UserManager(string id, string searchString)
        {
            var model = new UserManagerViewModel();
            model.UserList = UserViewModel.GetAllUsers();

            if (!String.IsNullOrEmpty(searchString))
            {
                model.UserList = model.UserList.Where(u => u.User.UserName.ToUpper().Contains(searchString.ToUpper())
                                       || u.User.Email.ToUpper().Contains(searchString.ToUpper())
                                       || u.Profile.Role.ToUpper().Contains(searchString.ToUpper())
                                       || u.Profile.FullName.ToUpper().Contains(searchString.ToUpper())
                                   ).ToList();
            }

            model.SelectedUser = id;

            return View(model);
        }

        //
        // GET: /User/MyProfile
        [Authorize]
        public ActionResult MyProfile()
        {
            var userName = User.Identity.Name;
            var user = new UserViewModel(userName);

            // dummy data
            var php = new UserPHPModel();

            ProfileCommon profile = ProfileCommon.GetProfile(user.User.UserName);
            user.Profile = profile;
            php.Age = ((DateTime.Now.Year - profile.DOB.Year)).ToString();
            user.PHP = php;
            user.Profile.Role = Roles.GetRolesForUser(userName).FirstOrDefault();

            // get profile data
            //user.Details = user.Details.GetProfile(HttpContext.User.Identity.Name);

            // if its a pt, get his clients
            if (user.Profile.Role == "pt")
                ViewBag.Clients = UserViewModel.GetUsersByPT(user.User.UserName);

            return View(user);
        }

        //
        // GET: /User/ViewProfile
        [Authorize(Roles = "admin,pt")]
        public ActionResult ViewProfile(string id)
        {
            var user = new UserViewModel(id);
            var php = new UserPHPModel();

            try
            {
                // test permissions
                if (User.IsInRole("pt"))
                {
                    if (user.Profile.AssignedPT != User.Identity.Name)
                        return PartialView("_PermissionsError");
                }

                // retrieve profile
                php.Age = ((DateTime.Now.Year - user.Profile.DOB.Year)).ToString();
                user.PHP = php;
                user.Profile.Role = Roles.GetRolesForUser(id).FirstOrDefault();

                // build genders dropbown
                string[] genders = new string[3];
                genders[0] = "Female";
                genders[1] = "Male";
                genders[2] = "Undisclosed";
                ViewBag.Genders = new SelectList(genders, "Gender");

                // if its a pt, get his clients
                if(user.Profile.Role == "pt")
                    ViewBag.Clients = UserViewModel.GetUsersByPT(user.User.UserName);
            }
            catch (Exception ex)
            {
                IgniteHelper.LogError(ex);
            }

            // return the appropriate view based on role
            if(Roles.IsUserInRole(user.User.UserName, "admin"))
                return PartialView("_ProfileAdmin", user);
            else if(Roles.IsUserInRole(user.User.UserName, "pt"))
                return PartialView("_ProfilePT", user);
            else
                return PartialView("_ProfileClient", user);
        }

        //
        // POST: /User/EditDetails
        [Authorize]
        [HttpPost]
        public ActionResult EditDetails(EditUserModel model)
        {
            try
            {
                // update email
                UserViewModel user = new UserViewModel(model.User.UserName);
                user.User.Email = model.User.Email;
                Membership.UpdateUser(user.User);

                // save profile image
                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(model.ImageFile.FileName);

                    // generate a file name on the server based on a combination of the GymId and a timestap
                    string serverFile = model.User.UserName + "-" + DateTime.Now.ToString("yymmdd-Hmmss") + Path.GetExtension(model.ImageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/uploads"), serverFile);
                    try
                    {
                        // save the file to the specified location on the server
                        model.ImageFile.SaveAs(path);
                        // assign the generated filename to the user's profile
                        user.Profile.ProfileImage = serverFile;
                    }
                    catch (Exception ex)
                    {
                        IgniteHelper.LogError(ex);
                    }
                }

                // update profile
                user.Profile.FirstName = model.Profile.FirstName;
                user.Profile.LastName = model.Profile.LastName;
                user.Profile.DOB = model.Profile.DOB;
                user.Profile.Gender = model.Profile.Gender;
                user.Profile.Address1 = model.Profile.Address1;
                user.Profile.City = model.Profile.City;
                user.Profile.Province = model.Profile.Province;
                user.Profile.Country = model.Profile.Country;
                user.Profile.PostalCode = model.Profile.PostalCode;
                user.Profile.Save();
            }
            catch (Exception ex)
            {
                IgniteHelper.LogError(ex);
            }

            // check if this was called by an admin or pt
            if (model.User.UserName != User.Identity.Name)
            {
                if (User.IsInRole("admin"))
                    return RedirectToAction("UserManager");
                else if (User.IsInRole("pt"))
                    return RedirectToAction("ClientManager");
            }
            // otherwise it must be a user editing their own profile
            return RedirectToAction("MyProfile");
        }

        //
        // GET: /User/EditDetails
        [Authorize]
        public ActionResult EditDetails(string id)
        {
            UserViewModel user = new UserViewModel(id);

            // retrieve profile
            ProfileCommon profile = ProfileCommon.GetProfile(user.User.UserName);
            user.Profile = profile;

            return PartialView(user);
        }

        //
        // POST: /User/ViewDetails
        [HttpPost]
        [Authorize]
        public ActionResult ViewDetails(UserViewModel model)
        {
            return PartialView("_ViewDetails", model);
        }

        //
        // GET: /User/ViewDetails
        [Authorize]
        public ActionResult ViewDetails()
        {
            return PartialView("_ViewDetails");
        }

        //
        // POST: /User/UpdatePHP
        [HttpPost]
        [Authorize]
        public ActionResult UpdatePHP(EditUserModel model)
        {
            TempData["SuccessMessage"] = "Recieved request for " + model.User.UserName;

            return RedirectToAction("ViewProfile");
        }

        //
        // GET: /User/UpdatePHP
        [Authorize]
        public ActionResult UpdatePHP(string id)
        {
            var user = new UserViewModel(id);

            try
            {
                var profile = new ProfileCommon();
                var php = new UserPHPModel();

                profile.DOB = new DateTime(1981, 1, 1);
                user.Profile = profile;
                php.Age = ((DateTime.Now.Year - profile.DOB.Year)).ToString();
                user.PHP = php;
            }
            catch (Exception ex)
            {
                IgniteHelper.LogError(ex);
            }

            return PartialView(user);
        }

        //
        // GET: /User/ViewPHP
        [Authorize]
        public ActionResult ViewPHP()
        {
            return PartialView("_ViewPHP");
        }

        //
        // GET: /User/ViewPT
        [Authorize]
        public ActionResult ViewPT(string pt)
        {
            var user = new UserViewModel(pt);

            return PartialView("_ViewPT", user);
        }

        //
        // GET: /User/ListPT
        [Authorize(Roles = "pt,admin")]
        public ActionResult ListPT()
        {
            List<UserViewModel> users = UserViewModel.GetAllUsersInRole("pt");

            return PartialView("_ListPT", users);
        }

        //
        // POST: /User/AssignPT
        [HttpPost]
        [Authorize(Roles = "pt,admin")]
        public bool AssignPT(string pt, string client)
        {
            try
            {
                var user = new UserViewModel(client);
                user.Profile.AssignedPT = pt;
                user.Profile.Save();
                return true;
            }
            catch (Exception ex)
            {
                IgniteHelper.LogError(ex);
            }

            return false;
        }
    }
}
