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
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var messages = _db.Messages.Include(m => m.ApplicationUser).Include(m => m.Channel);
            return View(messages.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = _db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(_db.Users, "Id", "UserName");
            ViewBag.ChannelId = new SelectList(_db.Channels, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Id")] Message message)
        {
            if (!ModelState.IsValid) return View(message);
            message.Id = Guid.NewGuid();
            _db.Messages.Add(message);
            _db.SaveChanges();
            return RedirectToAction("Index", "Channel", new { id = message.ChannelId });
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = _db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(_db.Users, "Id", "UserName", message.ApplicationUserId);
            ViewBag.ChannelId = new SelectList(_db.Channels, "Id", "Name", message.ChannelId);
            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,ApplicationUserId,ChannelId")] Message message)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(message).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(_db.Users, "Id", "UserName", message.ApplicationUserId);
            ViewBag.ChannelId = new SelectList(_db.Channels, "Id", "Name", message.ChannelId);
            return View(message);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = _db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            var message = _db.Messages.Find(id);
            if (message == null) return View("Error");
            _db.Messages.Remove(message);
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
