using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trolltunga.Models;

namespace Trolltunga.Controllers
{
    public class DirectMessageController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var directMessages = _db.DirectMessages.Include(d => d.Project);
            return View(directMessages.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_db.Projects, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Id")] DirectMessage directMessage)
        {
            if (ModelState.IsValid)
            {
                directMessage.Id = Guid.NewGuid();
                _db.DirectMessages.Add(directMessage);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(_db.Projects, "Id", "Name", directMessage.ProjectId);
            return View(directMessage);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var directMessage = _db.DirectMessages.Find(id);
            if (directMessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(_db.Projects, "Id", "Name", directMessage.ProjectId);
            return View(directMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Id")] DirectMessage directMessage)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(directMessage).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(_db.Projects, "Id", "Name", directMessage.ProjectId);
            return View(directMessage);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var directMessage = _db.DirectMessages.Find(id);
            if (directMessage == null)
            {
                return HttpNotFound();
            }
            return View(directMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            var directMessage = _db.DirectMessages.Find(id);
            if (directMessage == null) return View("Error");
            _db.DirectMessages.Remove(directMessage);
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
