using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using BlogDomainProject.Entities;
using BlogDomainProject.Interface;
using System.Linq;

namespace BlogDomainProject.Logic
{
    public class DomainService : IBlogDomain
    {
        /// <summary>
        /// Check if the DB has at least one user.
        /// </summary>
        /// <returns></returns>
        public bool DbHasUsers()
        {
            using (var context = new DomainDBContext())
            {
                return context.Users.Any();
            }
        }

        public bool CreateAdminUser(User user, Author author, Role role, out List<string> errorList )
        {
            errorList = new List<string>();
            var saveSuccessful = true;

            using (var context = new DomainDBContext())
            {
                try
                {
                    LogQueryToVSTrace(context);
                    
                    AutoDetectChange(context, false);

                    context.Roles.Add(role);
                    context.Users.Add(user);
                    context.Authors.Add(author);

                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationError in context.GetValidationErrors())
                    {
                        foreach (var error in entityValidationError.ValidationErrors)
                        {
                            errorList.Add("Entity: " + entityValidationError.Entry.Entity.GetType().FullName + " Property Name: " + error.PropertyName + " ErrorMessage: " + error.ErrorMessage);
                        }
                    }
                    saveSuccessful = false;
                }
                finally
                {
                    AutoDetectChange(context, true);
                }
            }

            return saveSuccessful;
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
            context.Configuration.AutoDetectChangesEnabled = detect;
        }

        /// <summary>
        /// Get user login guid using login credential
        /// </summary> 
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>string userGuid</returns>
        public string GetUserLoginGuidWithLoginCredentials(string email, string password)
        {
            using (var context = new DomainDBContext())
            {
                return context.Users
                    .Where(u => u.Email.ToLower() == email.ToLower() && u.Password.ToLower() == password.ToLower())
                    .Select(s => s.UserGuid)
                    .FirstOrDefault();
            }
        }

        public void LogQueryToVSTrace(DomainDBContext context)
        {
            context.Database.Log = message => Trace.WriteLine(message);
        }
    }
}
