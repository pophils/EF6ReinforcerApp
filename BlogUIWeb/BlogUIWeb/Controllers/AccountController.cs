using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogDomainProject;
using BlogDomainProject.Entities;
using BlogDomainProject.Interface;
using BlogUIWeb.ViewDTO;

namespace BlogUIWeb.Controllers
{
    public class AccountController : Controller
    {
        private static readonly IBlogDomain DBFactory = DomainModule.blogDomainFactory();


        public ActionResult Login()
        {
            return View();
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

                    if (DBFactory.CreateAdminUser(newUser, newSuperAuthor, adminRole, out errorList))
                    {
                        return Json(new JsonMessage()
                        {
                            Success = true,
                            ResponseMessage = newUser.UserGuid
                        }, JsonRequestBehavior.DenyGet);
                    }
                    return Json(new ErrorJsonMessages()
                    {
                        Success = false,
                        ResponseMessages = errorList
                    }, JsonRequestBehavior.DenyGet);
                }
            }

            var userGuid = DBFactory.GetUserLoginGuidWithLoginCredentials(email, password);

            return Json(string.IsNullOrEmpty(userGuid) ?
                    new JsonMessage() { Success = false, ResponseMessage = "Please enter a valid login credentials." } :
                    new JsonMessage() { Success = true, ResponseMessage = userGuid }, JsonRequestBehavior.DenyGet);

        }
    }
}