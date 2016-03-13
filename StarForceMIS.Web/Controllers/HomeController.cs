using StarForceMIS.BLL.Interface;
using StarForceMIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarForceMIS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult GuardSchedule()
        {
            var model = new GuardScheduleViewModel();
            model.DayShift = new List<ScheduleDetailViewModel>();
            model.NightShift = new List<ScheduleDetailViewModel>();
            return View(model);
        }


        [ChildActionOnly]
        public ActionResult GuardList() 
        {
            var model = new List<GuardViewModel>();
            return View(model);
        }

        [HttpPost]
        public ActionResult GuardSchedule(DateTime query)
        {
            var model = new GuardScheduleViewModel();
            model.DayShift = new List<ScheduleDetailViewModel>();
            model.NightShift = new List<ScheduleDetailViewModel>();


            model.DayShift.Add(new ScheduleDetailViewModel()
            {
                GuardID = 1,
                Name = "Renato Cebu",
                Position = "Security Officer",
                ScheduleDate = DateTime.UtcNow
            });

            model.NightShift.Add(new ScheduleDetailViewModel()
            {
                GuardID = 1,
                Name = "Renato Cebu",
                Position = "Security Officer",
                ScheduleDate = DateTime.UtcNow
            });
            return View(model);
        }

        public ActionResult Search(DateTime date)
        {
            return PartialView("partial/Schedules");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}