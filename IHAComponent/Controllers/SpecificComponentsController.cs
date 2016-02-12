using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IHAComponent.Models;

namespace IHAComponent.Controllers
{
    [Authorize]
    public class SpecificComponentsController : Controller
    {
        private ComponentIHADatabaseEntities db = new ComponentIHADatabaseEntities();

        // GET: SpecificComponents
        public ActionResult Index()
        {
            var specificComponents = db.SpecificComponents.Include(s => s.Component1);
            return View(specificComponents.ToList());
        }


        [AllowAnonymous]
        // GET: SpecificComponents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecificComponent specificComponent = db.SpecificComponents.Find(id);
            if (specificComponent == null)
            {
                return HttpNotFound();
            }
            return View(specificComponent);
        }

        // GET: SpecificComponents/Create
        public ActionResult Create()
        {
            ViewBag.Component = new SelectList(db.Components, "ComponentId", "Name");
            return View();
        }

        // POST: SpecificComponents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number,SerieNr,Component")] SpecificComponent specificComponent)
        {
            if (ModelState.IsValid)
            {
                db.SpecificComponents.Add(specificComponent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Component = new SelectList(db.Components, "ComponentId", "Name", specificComponent.Component);
            return View(specificComponent);
        }

        // GET: SpecificComponents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecificComponent specificComponent = db.SpecificComponents.Find(id);
            if (specificComponent == null)
            {
                return HttpNotFound();
            }
            ViewBag.Component = new SelectList(db.Components, "ComponentId", "Name", specificComponent.Component);
            return View(specificComponent);
        }

        // POST: SpecificComponents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,SerieNr,Component")] SpecificComponent specificComponent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specificComponent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Component = new SelectList(db.Components, "ComponentId", "Name", specificComponent.Component);
            return View(specificComponent);
        }

        // GET: SpecificComponents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecificComponent specificComponent = db.SpecificComponents.Find(id);
            if (specificComponent == null)
            {
                return HttpNotFound();
            }
            return View(specificComponent);
        }

        // POST: SpecificComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SpecificComponent specificComponent = db.SpecificComponents.Find(id);
            db.SpecificComponents.Remove(specificComponent);
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
