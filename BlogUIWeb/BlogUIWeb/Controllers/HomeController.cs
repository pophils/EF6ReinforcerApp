using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogDomainProject;
using BlogDomainProject.Interface;

namespace BlogUIWeb.Controllers
{
    public class HomeController : Controller
    {
        /// Session is explicitly used to control user authentication and authorization
        /// in lieu of identity API.
        /// Note: this is just a lightweight app and this auth approach should never be used
        /// in a production app.
        /// 
        private static readonly IBlogDomain DBFactory = DomainModule.blogDomainFactory();

        public ActionResult Index()
        {
            var userGuid = Session["user_guid"];

            if (userGuid == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (Session["role_name"] == null)
                Session["role_name"] = DBFactory.GetUserRoleName(userGuid.ToString()); 

            // fetch the first 10 blogs.
            return View(DBFactory.GetBlogs(1,10));
        }
    }
}