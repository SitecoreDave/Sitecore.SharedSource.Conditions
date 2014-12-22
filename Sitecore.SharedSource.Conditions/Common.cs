using System;
using System.Web.Security;

namespace Sitecore.SharedSource.Conditions
{
    public class Common
    {
        public static string GetUserProfile(string profileKey)
        {
            var profileKeyValue = Context.User.Profile[profileKey];
            if (String.IsNullOrEmpty(profileKeyValue)) profileKeyValue = GetMembershipUserProfile(profileKey);

            return profileKeyValue;
        }

        public static string GetMembershipUserProfile(string profileKey)
        {
            var user = Membership.GetUser();
            if (user == null) return null;

            switch (profileKey.ToLower())
            {
                case "creationdate":
                    return user.CreationDate.ToString("g");
                case "lastactivitydate":
                    return user.LastActivityDate.ToString("g");
                case "lastlockoutdate":
                    return user.LastLockoutDate.ToString("g");
                case "lastlogindate":
                    return user.LastLoginDate.ToString("g");
                case "lastpasswordchangeddate":
                    return user.LastPasswordChangedDate.ToString("g");
            }
            return null;
        }
    }
}
