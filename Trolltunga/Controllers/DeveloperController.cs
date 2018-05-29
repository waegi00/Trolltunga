using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Trolltunga.Models;
using Trolltunga.ViewModels.Developer;

namespace Trolltunga.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(_db.Developers.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var developer = _db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        public ActionResult Create()
        {
            return View(
                new DeveloperViewModel
                {
                    AllProjects = _db.Projects.ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Id")] DeveloperViewModel developerViewModel)
        {
            if (!ModelState.IsValid) return View(developerViewModel);
            _db.Developers.Add(developerViewModel);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var developer = _db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(
                new DeveloperViewModel
                {
                    Name = developer.Name,
                    Projects = developer.Projects,
                    AllProjects = _db.Projects.ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Id")] DeveloperViewModel developerViewModel)
        {
            if (!ModelState.IsValid) return View(developerViewModel);
            _db.Entry(developerViewModel).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var developer = _db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var developer = _db.Developers.Find(id);
            if (developer == null) return View("Error");
            _db.Developers.Remove(developer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
