using StarForceMIS.BLL.Interface;
using StarForceMIS.BLL.Services;
using StarForceMIS.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarForceMIS.Web.Controllers
{
    [Authorize]
    public class SGManagementController : Controller
    {

        private readonly IGuardService _guardService;
        private readonly IMonthlyScheduleService _monthlyScheduleService;

        public SGManagementController()
        {
            _guardService = new GuardService();
            _monthlyScheduleService = new MonthlyScheduleService();
        }

        // GET: Default
        public ActionResult Index()
        {
            var guards = _guardService.RetrieveGuards();
            return View(guards);
        }

        [HttpGet]
        public ActionResult Schedule()
        {
            var scheduleViewModel = new ScheduleViewModel();
            scheduleViewModel.ScheduleLookUp = _monthlyScheduleService.GetMonthlySchedule();

            long todayTicks = DateTime.UtcNow.Ticks;

            foreach (var i in scheduleViewModel.ScheduleLookUp)
            {
                if (todayTicks >= i.From.Ticks && todayTicks <= i.To.Ticks)
                {
                    scheduleViewModel.ScheduleID = i.ID;
                }
            }
            scheduleViewModel.Schedules = new GuardScheduleViewModel();
            scheduleViewModel.Schedules.DayShift = _guardService.RetrieveScheduleGuard(1, scheduleViewModel.ScheduleID);
            scheduleViewModel.Schedules.NightShift= _guardService.RetrieveScheduleGuard(2, scheduleViewModel.ScheduleID);
            scheduleViewModel.GuardDetails = new List<GuardViewModel>();
            return View(scheduleViewModel);
        }

        public ActionResult RetrieveSchedule(long scheduleID)
        {
            var Schedules = new GuardScheduleViewModel();
            Schedules.DayShift = _guardService.RetrieveScheduleGuard(1, scheduleID);
            Schedules.NightShift = _guardService.RetrieveScheduleGuard(2, scheduleID);
            return PartialView("partial/_GuardSchedules", Schedules);
        }

        [HttpPost]
        public ActionResult Schedule(ScheduleViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult NewGuard(long id = 0)
        {
            var model = _guardService.GetGuardByID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult NewGuard(GuardViewModel model)
        {
            if (ModelState.IsValid)
            {
                Callback<GuardViewModel> result;
                if (model.ID == 0)
                    result = _guardService.AddNewGuard(model);
                else
                    result = _guardService.UpdateGuard(model);

                if (!result.Success)
                {
                    ModelState.AddModelError("Error", result.Message);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("NewGuard", new { id = result.Data.ID });
                }

            }
            return View(model);

        }

        [HttpGet]
        public ActionResult Search(string querystring) 
        {
            var result = _guardService.SearchGuard(querystring);
            return PartialView("partial/_GuardList", result);
        }

        [HttpGet]
        public ActionResult SearchSchedule(string querystring)
        {
            var result = _guardService.SearchGuard(querystring);
            return PartialView("partial/_GuardListSchedule", result);
        }

        [HttpGet]
        public ActionResult Attendance()
        {
            var guards = _guardService.RetrieveGuards();
            return View(guards);
        }

        [HttpGet]
        public ActionResult DayOffSchedule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase picture, long id)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = string.Format("{0}.jpg", id);
                    var path = Path.Combine(Server.MapPath("~/guardpics/"), fileName);
                    file.SaveAs(path);
                }
            }
            return RedirectToAction("NewGuard", new { id = id });
        }

        [HttpGet]
        public ActionResult GetUserImage(long id)
        {
            string imgPath = Server.MapPath(string.Format("~/guardpics/{0}.jpg", id));
            if (!System.IO.File.Exists(imgPath))
                imgPath = Server.MapPath("~/guardpics/noavatar.jpg");

            return File(imgPath, "application/jpeg");

        }

        [HttpGet]
        public ActionResult GetGuardDetails(long id)
        {
            var model = _guardService.GetGuardByID(id);
            return PartialView("partial/_GuardDetailsScheduleReadOnly",model);
        }

        [HttpPost]
        public ActionResult ScheduleGuard(GuardViewModel model)
        {
            var result = _guardService.ScheduleGuard(model);
            return PartialView("partial/_ScheduleCallback", result);
        }

    }
}