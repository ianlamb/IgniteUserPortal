/** @file   DashboardModels.cs
 *  @author Ian Lamb
 *  @date   2012-10-25
 *  @ver    0.2
 *  @brief  Defines the models associated with the user dashboards
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
    //
    // VIEW MODELS
    //
    public class DashAdminViewModel
    {
        public List<UserViewModel> StaffList { get; set; }
        public List<string> ActivityList { get; set; }
        public StatisticsModel Stats { get; set; }

        public DashAdminViewModel()
        {
            StaffList = UserViewModel.GetAllUsersInRole("pt");
            Stats = new StatisticsModel();
            ActivityList = new List<string>();
        }
    }

    public class DashPTViewModel
    {
        public List<UserViewModel> ClientList { get; set; }
        public List<string> ActivityList { get; set; }
        public StatisticsModel Stats { get; set; }

        public DashPTViewModel()
        {
            ClientList = UserViewModel.GetUsersByPT(HttpContext.Current.User.Identity.Name);
            Stats = new StatisticsModel();
            ActivityList = new List<string>();
        }
    }

    public class DashClientViewModel
    {
        public List<string> ProgressList { get; set; }

        public DashClientViewModel()
        {
        }
    }

    public class StatisticsModel
    {
        public int ClientsAtGym { get; set; }
        public int ClientsOnline { get; set; }
        public int TrainersOnline { get; set; }

        public StatisticsModel()
        {
        }
    }
}