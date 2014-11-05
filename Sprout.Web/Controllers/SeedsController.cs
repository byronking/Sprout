using Sprout.Data.Domain;
using Sprout.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sprout.Web.Controllers
{
    public class SeedsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Seeds = "active";
            return View();
        }

        public ActionResult Start()
        {
            ViewBag.Seeds = "active";
            return View();
        }

        [HttpPost]
        public ActionResult SaveSeedProject(Project model)
        {
            var testy = model;
            model.Active = true;
            model.OriginationDate = DateTime.Now;
            model.StageId = Convert.ToInt16(ProjectStages.Seeds);

            var repository = new SeedsRepository();
            var saveSuccessful = repository.SaveSeedsProject(model);
            ViewBag.SaveSuccesful = saveSuccessful;

            return View("~/Views/Seeds/Start.cshtml", model);
        }
    }
}
