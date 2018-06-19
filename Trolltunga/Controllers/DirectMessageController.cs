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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DirectMessage
        public ActionResult Index(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var directMessages = db.DirectMessages.Include(d => d.Project);
            return View(directMessages.ToList());
        }

        // GET: DirectMessage/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            return View();
        }

        // POST: DirectMessage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ProjectId")] DirectMessage directMessage)
        {
            if (ModelState.IsValid)
            {
                directMessage.Id = Guid.NewGuid();
                db.DirectMessages.Add(directMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", directMessage.ProjectId);
            return View(directMessage);
        }

        // GET: DirectMessage/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectMessage directMessage = db.DirectMessages.Find(id);
            if (directMessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", directMessage.ProjectId);
            return View(directMessage);
        }

        // POST: DirectMessage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ProjectId")] DirectMessage directMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(directMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", directMessage.ProjectId);
            return View(directMessage);
        }

        // GET: DirectMessage/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DirectMessage directMessage = db.DirectMessages.Find(id);
            if (directMessage == null)
            {
                return HttpNotFound();
            }
            return View(directMessage);
        }

        // POST: DirectMessage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DirectMessage directMessage = db.DirectMessages.Find(id);
            db.DirectMessages.Remove(directMessage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
