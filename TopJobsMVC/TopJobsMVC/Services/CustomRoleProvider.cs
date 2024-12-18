using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TopJobsMVC.Services
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get; set; }

        // Implement the GetRolesForUser method to retrieve roles from your custom source (e.g., database)
        public override string[] GetRolesForUser(string username)
        {
            
            if (username == "Admin")
            {
                return new string[] { "Admin", "JobSeeker", "Employer" }; 
            }

            if (username == "JobSeeker")
            {
                return new string[] { "JobSeeker" };
            }

            if (username == "Employer")
            {
                return new string[] { "Employer" }; 
            }

            return new string[] { "None" }; 
        }

        // You can implement other required methods for RoleProvider, but these can be empty if not used
        public override bool IsUserInRole(string username, string roleName)
        {
            var roles = GetRolesForUser(username);
            return roles.Contains(roleName);
        }

        // The following methods are part of the RoleProvider interface but can be empty for this case
        public override void CreateRole(string roleName) { }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) { return false; }
        public override string[] GetAllRoles() { return new string[] { }; }
        public override string[] GetUsersInRole(string roleName) { return new string[] { }; }
        public override bool RoleExists(string roleName) { return true; }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
    }
}