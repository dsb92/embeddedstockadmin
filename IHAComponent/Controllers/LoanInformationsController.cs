using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IHAComponent.Models;
using Microsoft.Ajax.Utilities;

namespace IHAComponent.Controllers
{
    [Authorize]
    public class LoanInformationsController : Controller
    {
        private ComponentIHADatabaseEntities db = new ComponentIHADatabaseEntities();

        [AllowAnonymous]
        // GET: LoanInformations
        public ActionResult Index()
        {
            // Hent de låneinformationer tilknyttet et komponent, som er udlånt.
            var componentsLoaned = db.LoanInformations.Where(c => c.LoanDate != null).Include(l => l.SpecificComponent).AsNoTracking().ToList();

            var loanedQuery = componentsLoaned.GroupBy(col => col.SpecificComponent.Component);

            foreach (var item in loanedQuery)
            {
                ViewData[item.Key.ToString() + "Udlånt"] = item.Count();
            }

            // Hent de låneinformationer tilknyttet et komponent, som er reserveret.
            var componentsReserved = db.LoanInformations.Where(c => c.ReservationDate != null).Include(l => l.SpecificComponent).AsNoTracking().ToList();

            var reservedQuery = componentsReserved.GroupBy(col => col.SpecificComponent.Component);

            foreach (var item in reservedQuery)
            {
                ViewData[item.Key.ToString() + "Reserveret"] = item.Count();
            }

            return View(componentsLoaned.DistinctBy(c => c.SpecificComponent.Component));
        }

        [AllowAnonymous]
        // GET: LoanInformations/Details/5
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

            var component = specificComponent.Component1;
            ViewBag.CategoryName = component.Category.Name;
            ViewBag.ComponentName = component.Name;

            var loaninformations =
                db.LoanInformations.Where(c => c.SpecificComponent.Component == component.ComponentId)
                    .AsNoTracking()
                    .ToList();

            return View(loaninformations);
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
