/** @file   GymModels.cs
 *  @author Ian Lamb
 *  @date   2012-10-25
 *  @ver    0.2
 *  @brief  Defines the models associated with gym website management.
 *          Settings, Layout, Challenges, Exercises, Advertisements, Analytics.
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

namespace Ignite.Models
{
    // Contains informations associated with gym settings
    public class GymSettingsViewModel
    {
        public string GymId { get; set; }
        public string LogoURL { get; set; }
        public HttpPostedFileBase LogoFile { get; set; }

        [DisplayName("Gym Name")]
        public string GymName { get; set; }

        [DisplayName("Unit Type")]
        public string UnitType { get; set; }

        [DisplayName("Dropdown Increment")]
        public int DropdownIncrement { get; set; }

        [DisplayName("Screen Timeout")]
        public int ScreenTimeout { get; set; }

        [DisplayName("Session Timeout")]
        public int SessionTimeout { get; set; }

        [DisplayName("Header Colour")]
        public string HeaderColour { get; set; }

        [DisplayName("Body Colour")]
        public string BodyColour { get; set; }

        [DisplayName("Accent Colour")]
        public string AccentColour { get; set; }
    }

    // Contains informations associated with gym settings
    public class GymSettingsEditModel
    {
        public string GymId { get; set; }
        public string GymName { get; set; }
        public string UnitType { get; set; }
        public int DropdownIncrement { get; set; }
        public int ScreenTimeout { get; set; }
        public int SessionTimeout { get; set; }
        public string HeaderColour { get; set; }
        public string BodyColour { get; set; }
        public string AccentColour { get; set; }
    }

    public class GymLogoEditModel
    {
        public string GymId { get; set; }
        public string LogoURL { get; set; }
    }

    // TODO:
    // GymLayoutModel
    // GymChallengesModel
    // GymAdvertisementsModel
    // GymAnalyticsModel
}