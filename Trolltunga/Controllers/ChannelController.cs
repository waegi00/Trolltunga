using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trolltunga.Models;
using Trolltunga.ViewModels.Channel;

namespace Trolltunga.Controllers
{
    public class ChannelController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        
        public ActionResult Index(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var channel = _db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return View(channel);
        }
        
        public ActionResult Create(Guid projectId)
        {
            return View(new ChannelFormViewModel
            {
                ProjectId = projectId
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid? projectId, ChannelFormViewModel model)
        {
            if (projectId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!ModelState.IsValid) return View(model);
            var channel = new Channel
            {
                Id = new Guid(),
                Name = model.Name,
                ProjectId = (Guid)projectId
            };
            _db.Channels.Add(channel);
            _db.SaveChanges();
            return RedirectToAction("Index", new {  channel.Id });
        }
        
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var channel = _db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return View(new ChannelFormViewModel
            {
                Id = channel.Id,
                Name = channel.Name,
                ProjectId = channel.ProjectId
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChannelFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var channel = _db.Channels.FirstOrDefault(x => x.Id == model.Id);
            if (channel == null) return View("Error");
            channel.Id = model.Id;
            channel.Name = model.Name;
            channel.ProjectId = model.ProjectId;
            _db.Entry(channel).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", new { projectId = channel.ProjectId });
        }
        
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var channel = _db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }
            return View(new ChannelFormViewModel
            {
                Id = channel.Id,
                Name = channel.Name,
                ProjectId = channel.ProjectId
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            var channel = _db.Channels.Find(id);
            if (channel == null) return View("Error");
            _db.Channels.Remove(channel);
            _db.SaveChanges();
            return RedirectToAction("Index", new { projectId = channel.ProjectId });
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
