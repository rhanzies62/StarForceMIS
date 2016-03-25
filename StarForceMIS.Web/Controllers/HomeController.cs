using StarForceMIS.BLL.Interface;
using StarForceMIS.BLL.Services;
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
        private readonly IGuardService _guardService;
        private readonly IMonthlyScheduleService _monthlyScheduleService;
        public HomeController() 
        {
            _guardService = new GuardService();
            _monthlyScheduleService = new MonthlyScheduleService();
        }


        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult GuardSchedule()
        {
            var model = new GuardScheduleViewModel();
            var currentSched = _monthlyScheduleService.RetrieveCurrentSchedule();
            model.DayShift = _guardService.RetrieveScheduleGuard(1, currentSched);
            model.NightShift = _guardService.RetrieveScheduleGuard(2, currentSched);
            return View(model);
        }


        [ChildActionOnly]
        public ActionResult GuardList() 
        {
            var guards = _guardService.RetrieveGuards();
            return View(guards);
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