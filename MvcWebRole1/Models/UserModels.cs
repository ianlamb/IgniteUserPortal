/** @file   UserModels.cs
 *  @author Ian Lamb
 *  @date   2012-10-15
 *  @ver    0.1
 *  @brief  Defines the models associated with user management
 *          
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Profile;

namespace Ignite.Models
{
    // The View model for a user which contains all associated information, 
    // including things like Membership, Profile and Personal Health Profile
    public class UserViewModel
    {
        public MembershipUser User { get; set; }
        public ProfileCommon Profile { get; set; }
        public UserPHPModel PHP { get; set; }

        public UserViewModel()
        {
            try
            {
                this.User = Membership.GetUser();
                this.Profile = ProfileCommon.GetProfile();
            }
            catch (Exception ex)
            {
                IgniteHelper.LogError(ex);
            }
        }
        public UserViewModel(string id)
        {
            try
            {
                this.User = Membership.GetUser(id);
                this.Profile = ProfileCommon.GetProfile(id);
            }
            catch (Exception ex)
            {
                IgniteHelper.LogError(ex);
            }
        }

        public static List<UserViewModel> GetAllUsers()
        {
            var users = new List<UserViewModel>();

            try
            {
                var systemUsers = Membership.GetAllUsers();
                foreach (MembershipUser u in systemUsers)
                {
                    var role = Roles.GetRolesForUser(u.UserName).Length > 0 ? Roles.GetRolesForUser(u.UserName).First() : "";
                    var pf = ProfileCommon.GetProfile(u.UserName);
                    pf.Role = role;
                    users.Add(new UserViewModel()
                    {
                        User = u,
                        Profile = pf
                    });
                }
            }
            catch (Exception ex)
            {
                IgniteHelper.LogError(ex);
            }

            return users;
        }

        public static List<UserViewModel> GetAllUsersInRole(string searchRole)
        {
            var users = new List<UserViewModel>();

            try
            {
                var systemUsers = Membership.GetAllUsers();
                foreach (MembershipUser u in systemUsers)
                {
                    var role = Roles.GetRolesForUser(u.UserName).Length > 0 ? Roles.GetRolesForUser(u.UserName).First() : "";
                    var pf = ProfileCommon.GetProfile(u.UserName);
                    pf.Role = role;
                    if (role.Equals(searchRole))
                    {
                        users.Add(new UserViewModel()
                        {
                            User = u,
                            Profile = pf
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                IgniteHelper.LogError(ex);
            }

            return users;
        }

        public static List<UserViewModel> GetUsersByPT(string ptName)
        {
            var systemUsers = Membership.GetAllUsers();
            var users = new List<UserViewModel>();
            foreach (MembershipUser u in systemUsers)
            {
                var role = Roles.GetRolesForUser(u.UserName).Length > 0 ? Roles.GetRolesForUser(u.UserName).First() : "";
                var pf = ProfileCommon.GetProfile(u.UserName);
                pf.Role = role;
                if (Roles.IsUserInRole(u.UserName, "client") && pf.AssignedPT == ptName)
                {
                    users.Add(new UserViewModel()
                    {
                        User = u,
                        Profile = pf
                    });
                }
            }

            return users;
        }

        public static UserViewModel GetUser(string id)
        {
            var user = new UserViewModel();

            try
            {
                var systemUser = Membership.GetUser(id);
                user.User = systemUser;
                user.Profile = ProfileCommon.GetProfile(id);
            }
            catch (Exception ex)
            {
                IgniteHelper.LogError(ex);
            }

            return user;
        }
    }

    // Contains simple details like location, date of birth, etc.
    public class ProfileCommon : ProfileBase
    {
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                // construct best fit name for the user, priority is FullName > FirstName > LastName > UserName
                // User always has a UserName so this should never return empty
                string name = "";
                if ((string)GetPropertyValue("FirstName") != "")
                {
                    name = (string)GetPropertyValue("FirstName");
                    if ((string)GetPropertyValue("LastName") != "")
                        name += " " + (string)GetPropertyValue("LastName");
                }
                else if ((string)GetPropertyValue("FirstName") != "")
                    name = " " + (string)GetPropertyValue("LastName");
                else
                    name = this.UserName;
                return name;
            }
        }

        [Display(Name = "First Name")]
        public string FirstName
        {
            get
            {
                return (string)GetPropertyValue("FirstName");
            }
            set
            {
                if(!IsAnonymous) SetPropertyValue("FirstName", value);
            }
        }

        [Display(Name = "Last Name")]
        public string LastName
        {
            get
            {
                return (string)GetPropertyValue("LastName");
            }
            set
            {
                if (!IsAnonymous) SetPropertyValue("LastName", value);
            }
        }

        [Display(Name = "Gender")]
        public string Gender
        {
            get
            {
                return (string)GetPropertyValue("Gender");
            }
            set
            {
                if (!IsAnonymous) SetPropertyValue("Gender", value);
            }
        }

        [Display(Name = "Address")]
        public string Address1
        {
            get
            {
                return (string)GetPropertyValue("Address1");
            }
            set
            {
                if (!IsAnonymous) SetPropertyValue("Address1", value);
            }
        }

        [Display(Name = "City")]
        public string City
        {
            get
            {
                return (string)GetPropertyValue("City");
            }
            set
            {
                if (!IsAnonymous) SetPropertyValue("City", value);
            }
        }

        [Display(Name = "Province")]
        public string Province
        {
            get
            {
                return (string)GetPropertyValue("Province");
            }
            set
            {
                if (!IsAnonymous) SetPropertyValue("Province", value);
            }
        }

        [Display(Name = "Country")]
        public string Country
        {
            get
            {
                return (string)GetPropertyValue("Country");
            }
            set
            {
                if (!IsAnonymous) SetPropertyValue("Country", value);
            }
        }

        [Display(Name = "Postal Code")]
        public string PostalCode
        {
            get
            {
                return (string)GetPropertyValue("PostalCode");
            }
            set
            {
                if (!IsAnonymous) SetPropertyValue("PostalCode", value);
            }
        }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB
        {
            get
            {
                return (DateTime)GetPropertyValue("DOB");
            }
            set
            {
                if (!IsAnonymous) SetPropertyValue("DOB", value);
            }
        }

        public string ProfileImage
        {
            get
            {
                return (string)GetPropertyValue("ProfileImage");
            }
            set
            {
                if (!IsAnonymous) SetPropertyValue("ProfileImage", value);
            }
        }
        
        public string AssignedPT
        {
            get
            {
                return (string)GetPropertyValue("AssignedPT");
            }
            set
            {
                if (!IsAnonymous) SetPropertyValue("AssignedPT", value);
            }
        }

        public string Role { get; set; }

        public ProfileCommon() { }

        public static ProfileCommon GetProfile(string username)
        {
            // classic cast throws exceptions which are easier to debug than using the 'as' keyword
            return (ProfileCommon)ProfileBase.Create(username, true);
        }

        public static ProfileCommon GetProfile()
        {
            return (ProfileCommon)ProfileBase.Create(Membership.GetUser().UserName, true);
        }
    }

    // Contains all information associated with Personal Health Profile
    public class UserPHPModel
    {
        public string Age { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public int RestSystolicBP { get; set; }
        public int RestDiastolicBP { get; set; }
        public int RestHeartRate { get; set; }
        public int BodyFatPercent { get; set; }
        public int FatWeight { get; set; }
        public int FatFreeWeight { get; set; }
        public int BodyWater { get; set; }
        public double Chest { get; set; }
        public double LeftArm { get; set; }
        public double RightArm { get; set; }
        public double Waist { get; set; }
        public double Hips { get; set; }
        public double LeftThigh { get; set; }
        public double RightThigh { get; set; }
        public double LeftCalf { get; set; }
        public double RightCalf { get; set; }
        public string Comments { get; set; }

        public UserPHPModel() { }
    }

    // This is the information passed to edit a user
    public class EditUserModel
    {
        public EditUser User { get; set; }
        public EditProfile Profile { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        public class EditUser
        {
            [Required]
            public string UserName { get; set; }
            public string Email { get; set; }
        }

        public class EditProfile
        {
            public string ProfileImage { get; set; }
            public DateTime DOB { get; set; }
            public string FullName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public string Address1 { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
            public string Country { get; set; }
            public string PostalCode { get; set; }
        }
    }

    // This only needs to contain a username for the user to be deleted
    public class DeleteUserModel
    {
        public DeleteUser User { get; set; }

        public class DeleteUser
        {
            [Required]
            public string UserName { get; set; }
        }
    }

    // CreateUserModel is similar to the default RegisterModel but it also has a user role
    public class CreateUserModel
    {
        [Required]
        [DisplayName("User role")]
        public string UserRole { get; set; }

        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }
    }

    public class UserManagerViewModel
    {
        public IEnumerable<Ignite.Models.UserViewModel> UserList { get; set; }
        public string SelectedUser { get; set; }

        public UserManagerViewModel() { }
    }
}