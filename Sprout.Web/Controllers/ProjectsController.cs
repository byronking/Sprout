using Sprout.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sprout.Web.Controllers
{
    public class ProjectsController : Controller
    {
        [HttpGet]
        public ActionResult Index(int projectId)
        {
            var repository = new SeedsRepository();
            var project = repository.GetActiveSeedProjectById(projectId);

            ViewBag.Seeds = "active";
            return View(project);
        }
    }
}
