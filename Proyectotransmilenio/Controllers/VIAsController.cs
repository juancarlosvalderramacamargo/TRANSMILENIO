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
    public class VIAsController : Controller
    {
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

        // GET: VIAs
        public ActionResult Index()
        {
            return View(db.VIAS.ToList());
        }

        // GET: VIAs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIA vIA = db.VIAS.Find(id);
            if (vIA == null)
            {
                return HttpNotFound();
            }
            return View(vIA);
        }

        // GET: VIAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VIAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_VIA,NOMBRE_VIA")] VIA vIA)
        {
            if (ModelState.IsValid)
            {
                decimal idVia = db.VIAS.Max(y => y.ID_VIA);
                vIA.ID_VIA = idVia + 1;

                db.VIAS.Add(vIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vIA);
        }

        // GET: VIAs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIA vIA = db.VIAS.Find(id);
            if (vIA == null)
            {
                return HttpNotFound();
            }
            return View(vIA);
        }

        // POST: VIAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_VIA,NOMBRE_VIA")] VIA vIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vIA);
        }

        // GET: VIAs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIA vIA = db.VIAS.Find(id);
            if (vIA == null)
            {
                return HttpNotFound();
            }
            return View(vIA);
        }

        // POST: VIAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            VIA vIA = db.VIAS.Find(id);
            db.VIAS.Remove(vIA);
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
