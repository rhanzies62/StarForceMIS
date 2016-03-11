using StarForceMIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarForceMIS.Web.Controllers
{
    [Authorize]
    public class SGManagementController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            var guards = new List<GuardViewModel>();
            return View(guards);
        }

        [HttpGet]
        public ActionResult Schedule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Schedule(ScheduleViewModel model) 
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult NewGuard() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewGuard(GuardViewModel model) 
        {
            return View(model);
        }
    }
}