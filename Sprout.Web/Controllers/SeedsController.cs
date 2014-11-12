using Sprout.Data.Domain;
using Sprout.Data.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        public ActionResult SaveSeedProject(HttpPostedFileBase fileUpload, Project model)
        {
            model.Active = true;
            model.OriginationDate = DateTime.Now;
            model.StageId = Convert.ToInt16(ProjectStages.Seeds);

            var errors = ModelState.Where(v => v.Value.Errors.Any());

            if (ModelState.IsValid)
            {
                // Save the image file
                var fileName = Path.GetFileName(model.TitleThumbImageLink);
                var dir = ConfigurationManager.AppSettings["ProjectImagesDirectory"].ToString();

                var storageDir = dir + Path.DirectorySeparatorChar + fileName;

                if (!System.IO.File.Exists(fileName))
                {
                    fileUpload.SaveAs(dir + Path.DirectorySeparatorChar + fileName);
                }

                model.ProjectOriginatorId = 1;
                model.StageId = 1;
                model.OriginationDate = DateTime.Now;

                var repository = new SeedsRepository();
                var saveResults = repository.SaveSeedsProject(model);
                ViewBag.SaveSuccesful = saveResults.SaveSuccessful;
            }
            else
            {
                // Do something
            }            

            return View("~/Views/Seeds/Start.cshtml", model);
        }
    }
}
