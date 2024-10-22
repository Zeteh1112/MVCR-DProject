using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using static MVCR_DProject.DBContext.DbContext;
using static MVCR_DProject.Models.NameModal;

namespace MVCR_DProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;
        public ActionResult Index()
        {
            return View();
        }
        public HomeController()
        {
            _userService = new UserService();
        }
        [HttpGet]
        public JsonResult Autocomplete(string term)
        {
            // Ensure that the term is not null or empty
            if (string.IsNullOrWhiteSpace(term))
            {
                return Json(new List<string>(), JsonRequestBehavior.AllowGet);
            }
            var users = _userService.GetUserNames(term);
            var userNames = users.Select(u => u.UD_NAME).ToList();
            return Json(userNames, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult Autocomplete(string term)
        //{
        //    var users = _userService.GetUserNames(term);
        //    var userNames = users.Select(u => u.UD_NAME).ToList();
        //    return Json(userNames, JsonRequestBehavior.AllowGet);
        //}

        //private static List<Name> products = new List<Name>
        //{
        //    new Name { Id = 1, NameList = "Anbu" },
        //    new Name { Id = 2, NameList = "Anand" },
        //    new Name { Id = 3, NameList = "Gokul" },
        //    new Name { Id = 4, NameList = "Nitish" },
        //    new Name { Id = 5, NameList = "Satish" },
        //    new Name { Id = 6, NameList = "Yogesh" },
        //};

        //public JsonResult Autocomplete(string term)
        //{
        //    var result = products
        //        .Where(p => p.NameList.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
        //        .Select(p => new { label = p.NameList, value = p.Id })
        //        .ToList();

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
    }
}