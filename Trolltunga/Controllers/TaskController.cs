using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trolltunga.Models;
using Trolltunga.ViewModels.Task;

namespace Trolltunga.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index(Guid projectId)
        {
            return RedirectToAction("Dashboard", "Project", new { id = projectId });
        }

        public ActionResult Create(Guid projectId)
        {
            return View(new TaskFormViewModel
            {
                AllUsers = _db.Projects.Find(projectId)?.Participants.ToList(),
                ProjectId = projectId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var task = new Task
            {
                Id = new Guid(),
                Name = model.Name,
                Description = model.Description,
                Status = Status.Todo,
                ProjectId = model.ProjectId,
                Assignees = _db.Users.Where(x => model.Participants.Contains(x.Id)).ToList()
            };
            _db.Tasks.Add(task);
            _db.SaveChanges();
            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = _db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(new TaskFormViewModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Participants = task.Assignees.Select(x => x.Id).ToList(),
                AllUsers = _db.Projects.Find(task.Project.Id)?.Participants.ToList(),
                ProjectId = task.ProjectId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var task = _db.Tasks.FirstOrDefault(x => x.Id == model.Id);
            if (task == null) return View("Error");
            task.Id = model.Id;
            task.Name = model.Name;
            task.Description = model.Description;
            task.Assignees.Clear();
            task.Assignees = _db.Users.Where(x => model.Participants.Contains(x.Id)).ToList();
            task.ProjectId = model.ProjectId;
            _db.Entry(task).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }

        public ActionResult ChangeStatus(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = _db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(new TaskStatusViewModel
            {
                Id = task.Id,
                OldStatus = task.Status.ToString(),
                NewStatus = task.Status
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(TaskStatusViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var task = _db.Tasks.FirstOrDefault(x => x.Id == model.Id);
            if (task == null) return View("Error");
            task.Id = model.Id;
            task.Status = model.NewStatus;
            _db.Entry(task).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", new { projectId = task.ProjectId });
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = _db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(new TaskFormViewModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Participants = task.Assignees.Select(x => x.Id).ToList(),
                AllUsers = _db.Projects.Find(task.Project.Id)?.Participants.ToList(),
                ProjectId = task.ProjectId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            var task = _db.Tasks.Find(id);
            if (task == null) return View("Error");
            _db.Tasks.Remove(task);
            _db.SaveChanges();
            return RedirectToAction("Index", new { projectId = task.ProjectId });
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
