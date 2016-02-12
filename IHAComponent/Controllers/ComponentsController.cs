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
    public class ComponentsController : Controller
    {
        private ComponentIHADatabaseEntities db = new ComponentIHADatabaseEntities();

        [AllowAnonymous]
        // GET: Components
        public ActionResult Index(string searchString)
        {
            if (searchString != null)
            {
                var searchedComponent = db.Components
                    .Where(c => c.Name.ToLower()
                    .Contains(searchString.ToLower()))
                    .Include(c => c.Category)
                    .AsNoTracking();

                if (searchedComponent.Any())
                {
                    ViewBag.Search = "Fandt " + searchedComponent.Count()+ " komponenter";
                    return View(searchedComponent.ToList());
                }
                ViewBag.Search = "Ingen komponenter fundet";
                return View();
            }
            var components = db.Components.Include(c => c.Category);
            ViewBag.Search = "";
            return View(components.ToList());
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ViewImage(int id)
        {
            var component = db.Components.Find(id);

            if (component == null)
            {
                return HttpNotFound();
            }

            byte[] buffer = component.ImageBytes;
            if (buffer == null)
            {
                return Content("");
            }

            return File(buffer, "image/jpg", string.Format("{0}.jpg", id));
            
        }

        // GET: CategoryComponent/Loan/5
        public ActionResult Loan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Check if the specific component is not already loaned.
            var loanInformation = db.LoanInformations.SingleOrDefault(r => r.SpecificComponentId == id);
            if (loanInformation == null)
            {
                ViewBag.IsComponentAvailable = true;
                loanInformation = new LoanInformation();
                var specificComponent = db.SpecificComponents.Find(id);
                loanInformation.SpecificComponentId = specificComponent.Id;
                // To display the data about the component, the student wants to loan.
                loanInformation.SpecificComponent = specificComponent;
            }
            else
            {
                ViewBag.IsComponentAvailable = false;
                var specificComponent = loanInformation.SpecificComponent;
                var component = loanInformation.SpecificComponent.Component1;
                ViewBag.ComponentName = component.Name;
                ViewBag.ComponentNumber = specificComponent.Number;
                ViewBag.ComponentSerieNr = specificComponent.SerieNr;
            }

            ViewBag.ReservationId = new SelectList(db.Students, "Id", "StudentId");

            return View(loanInformation);
        }

        // POST: CategoryComponent/Loan/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Loan([Bind(Include = "Id,LoanDate,ReturnDate,IsEmailSend,ReservationDate,ReservationId,SpecificComponentId")] LoanInformation loanInformation)
        {
            if (ModelState.IsValid)
            {
                loanInformation.LoanDate = DateTime.Now;

                db.LoanInformations.Add(loanInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReservationId = new SelectList(db.Students, "Id", "StudentId");

            return View(loanInformation);
        }

        [AllowAnonymous]
        // GET: Components/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // GET: Components/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");

            return View();
        }

        // POST: Components/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComponentId,Name,Info,Datasheet,Image,ImageBytes,ManufacturerLink,AdminComment,UserComment,CategoryId")] Component component)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(component.Image))
                {
                    var webClient = new WebClient();
                    try
                    {
                        byte[] imageBytes = webClient.DownloadData(component.Image);
                        component.ImageBytes = imageBytes;
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
                db.Components.Add(component);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", component.CategoryId);
            return View(component);
        }

        // GET: Components/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", component.CategoryId);
            return View(component);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComponentId,Number,SerieNr,Name,Info,Datasheet,Image,ImageBytes,ManufacturerLink,AdminComment,UserComment,CategoryId")] Component component)
        {
            if (ModelState.IsValid)
            {
                /* NOT WORKING - How to keep a byte[]? */
                //if (!String.IsNullOrEmpty(component.Image))
                //{
                //    Component editedComponent = db.Components.Find(component.ComponentId);

                //    // Check if the image URL is different from the one saved in the database.
                //    // If it is, download the new one.
                //    if (!editedComponent.Image.Equals(component.Image))
                //    {
                //        var webClient = new WebClient();
                //        try
                //        {
                //            byte[] imageBytes = webClient.DownloadData(component.Image);
                //            component.ImageBytes = imageBytes;
                //        }
                //        catch (Exception e)
                //        {

                //        }
                //    }
                //}

                var webClient = new WebClient();
                try
                {
                    byte[] imageBytes = webClient.DownloadData(component.Image);
                    component.ImageBytes = imageBytes;
                }
                catch (Exception e)
                {

                }
                
                db.Entry(component).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", component.CategoryId);
            return View(component);
        }

        // GET: Components/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Component component = db.Components.Find(id);
            db.Components.Remove(component);
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
