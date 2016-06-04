using System.Collections.Generic;
using BlogDomainProject.Entities;

namespace BlogDomainProject.Interface
{
    public interface IBlogDomain{
    
        /// <summary>
        /// Check if the DB has at least one user.
        /// </summary> 
        /// <returns></returns>
        bool DbHasUsers();

        /// <summary>
        /// Create an admin superuser
        /// </summary> 
        /// <param name="user">User instance</param>
        /// <param name="author">Author instance</param>
        /// <param name="role">Role instance</param>
        /// <param name="errorList"></param> 
        bool CreateAdminUser(User user, Author author, Role role, out List<string> errorList);
        
        /// <summary>
        /// Save changes to DB
        /// </summary>
        /// <param name="context">DbContext instance</param>
        /// <returns>Action status</returns>
        bool SaveChanges(DomainDBContext context);

        /// <summary>
        /// Disable Auto detect changes to aid multiple insert performance or not.
        /// </summary>
         /// <param name="context">DbContext instance</param>
        /// <param name="detect">Disable or Enable AutoDetectChanges during record insert</param>
        void AutoDetectChange(DomainDBContext context, bool detect);

        /// <summary>
        /// Get user login guid using login credential
        /// </summary> 
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>string userGuid</returns>
        string GetUserLoginGuidWithLoginCredentials(string email, string password);

        /// <summary>
        /// Output queries and commands sent by EF
        /// </summary>
        /// <param name="context"></param>
        void LogQueryToVSTrace(DomainDBContext context);
    }
}
