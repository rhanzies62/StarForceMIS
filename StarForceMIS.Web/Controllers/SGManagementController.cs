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

        public SGManagementController()
        {
            _guardService = new GuardService();
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
            return View();
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
            return View();
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

    }
}