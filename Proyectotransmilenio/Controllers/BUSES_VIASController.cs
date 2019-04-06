using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyectotransmilenio.Models;

namespace Proyectotransmilenio.Controllers
{
    public class BUSES_VIASController : Controller
    {
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

        // GET: BUSES_VIAS
        public ActionResult Index()
        {
            return View(db.BUSES_VIAS.ToList());
        }

        // GET: BUSES_VIAS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BUSES_VIAS bUSES_VIAS = db.BUSES_VIAS.Find(id);
            if (bUSES_VIAS == null)
            {
                return HttpNotFound();
            }
            return View(bUSES_VIAS);
        }

        // GET: BUSES_VIAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BUSES_VIAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TIPO_BUS,ID_VIA")] BUSES_VIAS bUSES_VIAS)
        {
            if (ModelState.IsValid)
            {
                db.BUSES_VIAS.Add(bUSES_VIAS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bUSES_VIAS);
        }

        // GET: BUSES_VIAS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BUSES_VIAS bUSES_VIAS = db.BUSES_VIAS.Find(id);
            if (bUSES_VIAS == null)
            {
                return HttpNotFound();
            }
            return View(bUSES_VIAS);
        }

        // POST: BUSES_VIAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TIPO_BUS,ID_VIA")] BUSES_VIAS bUSES_VIAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bUSES_VIAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bUSES_VIAS);
        }

        // GET: BUSES_VIAS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BUSES_VIAS bUSES_VIAS = db.BUSES_VIAS.Find(id);
            if (bUSES_VIAS == null)
            {
                return HttpNotFound();
            }
            return View(bUSES_VIAS);
        }

        // POST: BUSES_VIAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            BUSES_VIAS bUSES_VIAS = db.BUSES_VIAS.Find(id);
            db.BUSES_VIAS.Remove(bUSES_VIAS);
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
