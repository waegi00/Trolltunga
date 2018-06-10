using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Trolltunga.Models;
using Trolltunga.ViewModels.Project;

namespace Trolltunga.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(_db.Projects.ToList());
        }

        public ActionResult Dashboard(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        public ActionResult Create()
        {
            return View(
                new ProjectFormViewModel
                {
                    AllTasks = _db.Tasks.ToList(),
                    AllUsers = _db.Users.ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Id")] ProjectFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new ProjectFormViewModel
                {
                    AllTasks = _db.Tasks.ToList(),
                    AllUsers = _db.Users.ToList()
                });
            }
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Participants = _db.Users.Where(x => model.Participants.Contains(x.Id)).ToList(),
            };

            project.Channels.Add(new Channel
            {
                Id = Guid.NewGuid(),
                Project = project,
                Name = "#main"
            });

            _db.Projects.Add(project);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var projectViewModel = new ProjectFormViewModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Participants = project.Participants.Select(x => x.Id).ToList(),
                AllTasks = _db.Tasks.ToList(),
                AllUsers = _db.Users.ToList()
            };
            return View(projectViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new ProjectFormViewModel
                {
                    AllTasks = _db.Tasks.ToList(),
                    AllUsers = _db.Users.ToList()
                });
            }
            var project = _db.Projects.FirstOrDefault(x => x.Id == model.Id);
            project.Name = model.Name;
            project.Description = model.Description;
            project.Participants.Clear();
            project.Participants = _db.Users.Where(x => model.Participants.Contains(x.Id)).ToList();
            _db.Entry(project).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            var project = _db.Projects.Find(id);
            if (project == null) return View("Error");
            _db.Projects.Remove(project);
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
