using BlogDomainProject.Entities;

namespace BlogDomainProject.Interface
{
    public interface IBlogDomain{
    
        /// <summary>
        /// Check if the DB has at least one user.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        bool DbHasUsers(DomainDBContext context);
        /// <summary>
        /// Create an admin superuser
        /// </summary>
        /// <param name="context">DbContext instance</param>
        /// <param name="user">User instance</param> 
       void CreateAdminUser(DomainDBContext context, User user, Author author);
        
        /// <summary>
        /// Find a role by its name
        /// </summary>
        /// <param name="context">DbContext instance</param>
        /// <param name="roleName">role name</param>
        /// <param name="withoutTrackingChanges">Determines if returned object should be tracked by EF or not</param>
        /// <returns></returns>
        Role GetRoleByName(DomainDBContext context, string roleName, bool withoutTrackingChanges);
        
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
        /// <param name="context">DbContext instance</param>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>string userGuid</returns>
        string GetUserLoginGuidWithLoginCredentials(DomainDBContext context, string email, string password);
    }
}
