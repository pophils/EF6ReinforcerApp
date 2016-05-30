using BlogDomainProject.Entities;
using BlogDomainProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDomainProject.Logic
{
    public class DomainService : IBlogDomain
    {

        /// <summary>
        /// Check if the DB has at least one user.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool DbHasUsers(DomainDBContext context)
        {
            return context.Users.Any();
        }


        /// <summary>
        /// Create an admin superuser
        /// </summary>
        /// <param name="context">DbContext instance</param>
        /// <param name="user">User instance</param>
        /// <returns></returns>
        public void CreateAdminUser(DomainDBContext context, User user, Author author)
        {
            context.Users.Add(user);
            context.Authors.Add(author);
        }

        /// <summary>
        /// Find a role by its name
        /// </summary>
        /// <param name="context">DbContext instance</param>
        /// <param name="roleName">role name</param>
        /// <param name="withoutTrackingChanges">Determines if returned object should be tracked by EF or not</param>
        /// <returns></returns>
        public Role GetRoleByName(DomainDBContext context, string roleName, bool withoutTrackingChanges)
        {
            if (withoutTrackingChanges)
            {
                return context.Roles.AsNoTracking().Where(r => r.Name.ToLower() == roleName.ToLower()).FirstOrDefault<Role>();
            }

            return context.Roles.Where(r => r.Name.ToLower() == roleName.ToLower()).FirstOrDefault<Role>();
        }

        /// <summary>
        /// Save changes to DB
        /// </summary>
        /// <returns>Action status</returns>
        public bool SaveChanges(DomainDBContext context)
        {
            context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Disable Auto detect changes to aid multiple insert performance or not.
        /// </summary>
        /// <param name="context">DbContext instance</param>
        /// <param name="detect">Disable or Enable AutoDetectChanges during record insert</param>
        public void AutoDetectChange(DomainDBContext context, bool detect)
        {
            if (detect)
            {
                context.Configuration.AutoDetectChangesEnabled = true;
            }
            else
            {
                context.Configuration.AutoDetectChangesEnabled = false;
            }
        }

        /// <summary>
        /// Get user login guid using login credential
        /// </summary>
        /// <param name="context">DbContext instance</param>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>string userGuid</returns>
        public string GetUserLoginGuidWithLoginCredentials(DomainDBContext context, string email, string password)
        {
           return context.Users
                         .Where(u => u.Email.ToLower() == email.ToLower() && u.Password.ToLower() == password.ToLower())
                         .Select(s => s.UserGuid)
                         .FirstOrDefault<string>();
        }        
    }
}
