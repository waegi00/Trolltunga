using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Trolltunga.Models;

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
            Developer developer = _db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Developer developer)
        {
            if (!ModelState.IsValid) return View(developer);
            _db.Developers.Add(developer);
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
            return View(developer);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Developer developer)
        {
            if (!ModelState.IsValid) return View(developer);
            _db.Entry(developer).State = EntityState.Modified;
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
