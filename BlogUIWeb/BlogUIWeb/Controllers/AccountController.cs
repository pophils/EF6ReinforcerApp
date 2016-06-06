using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BlogDomainProject;
using BlogDomainProject.Entities;
using BlogDomainProject.Interface;
using BlogDomainProject.Utils;
using BlogUIWeb.ViewDTO;

namespace BlogUIWeb.Controllers
{
    public class AccountController : Controller
    {
        /// Session is explicitly used to control user authentication and authorization
        /// in lieu of identity API.
        /// Note: this is just a lightweight app and this auth approach should never be used
        /// in a production app.
        /// 
        private static readonly IBlogDomain DBFactory = DomainModule.blogDomainFactory();
         

        public ActionResult Login()
        {
            var userGuid = Session["user_guid"];

            if (userGuid == null)
            {
                return View();
            }

            if(Session["role_name"] == null)
                Session["role_name"] = DBFactory.GetUserRoleName(userGuid.ToString());
          
            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Json("Please enter the email", JsonRequestBehavior.DenyGet);
            }

            if (string.IsNullOrEmpty(password))
            {
                return Json("Please enter your password", JsonRequestBehavior.DenyGet);
            }


            //create a new admin account with an admin role.
            if (email.ToLower().Equals("admin") && password.ToLower().Equals("adminpass"))
            {
                // First login request, create an admin user as an author with admin role
                if (!DBFactory.DbHasUsers())
                {
                    List<string> errorList;

                    var adminRole = new Role() { CreatedBy = "system", Name = "admin" };
                    var newUser = new User()
                    {
                        Email = "admin",
                        Password = "adminpass",
                        CreatedBy = "system",
                        UserGuid = Guid.NewGuid().ToString(),
                        RoleId = adminRole.Id
                    };
                    var newSuperAuthor = new Author()
                    {
                        Name = "Admin",
                        CreatedBy = "SystemUser",
                        UserId = newUser.Id
                    };

                    if (!DBFactory.CreateAdminUser(newUser, newSuperAuthor, adminRole, out errorList))
                        return Json(new ErrorJsonMessages()
                        {
                            Success = false,
                            ResponseMessages = errorList
                        }, JsonRequestBehavior.DenyGet);

                    Session["user_guid"] = newUser.UserGuid;
                    Session["role_name"] = UserRoleStruct.Admin;

                    return Json(new JsonMessage()
                    {
                        Success = true,
                        ResponseMessage = newUser.UserGuid
                    }, JsonRequestBehavior.DenyGet);
                }
            }

            var userGuid = DBFactory.GetUserLoginGuidWithLoginCredentials(email, password);

            if (string.IsNullOrEmpty(userGuid))
            {
                return
                    Json(new JsonMessage()
                    {
                        Success = false,
                        ResponseMessage = "Please enter a valid login credentials."
                    });
            }

            Session["user_guid"] = userGuid;

            var roleName = DBFactory.GetUserRoleName(userGuid);

            if (roleName == UserRoleStruct.Admin)
            {
                Session["role_name"] = UserRoleStruct.Admin;
            }
            else if (roleName == UserRoleStruct.Author)
            {
                Session["role_name"] = UserRoleStruct.Author;
            }
            else
            {
                Session["role_name"] = UserRoleStruct.Reader;   
            }

            return
                Json(new JsonMessage() { Success = true, ResponseMessage = userGuid }, JsonRequestBehavior.DenyGet);
        }
    }
}