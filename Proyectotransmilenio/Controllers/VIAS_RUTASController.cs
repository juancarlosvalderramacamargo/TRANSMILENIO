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
    public class VIAS_RUTASController : Controller
    {
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

        // GET: VIAS_RUTAS
        public ActionResult Index()
        {
            return View(db.VIAS_RUTAS.ToList());
        }

        // GET: VIAS_RUTAS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIAS_RUTAS vIAS_RUTAS = db.VIAS_RUTAS.Find(id);
            if (vIAS_RUTAS == null)
            {
                return HttpNotFound();
            }
            return View(vIAS_RUTAS);
        }

        // GET: VIAS_RUTAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VIAS_RUTAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_VIA,ID_RUTA")] VIAS_RUTAS vIAS_RUTAS)
        {
            if (ModelState.IsValid)
            {
                db.VIAS_RUTAS.Add(vIAS_RUTAS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vIAS_RUTAS);
        }

        // GET: VIAS_RUTAS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIAS_RUTAS vIAS_RUTAS = db.VIAS_RUTAS.Find(id);
            if (vIAS_RUTAS == null)
            {
                return HttpNotFound();
            }
            return View(vIAS_RUTAS);
        }

        // POST: VIAS_RUTAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_VIA,ID_RUTA")] VIAS_RUTAS vIAS_RUTAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vIAS_RUTAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vIAS_RUTAS);
        }

        // GET: VIAS_RUTAS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIAS_RUTAS vIAS_RUTAS = db.VIAS_RUTAS.Find(id);
            if (vIAS_RUTAS == null)
            {
                return HttpNotFound();
            }
            return View(vIAS_RUTAS);
        }

        // POST: VIAS_RUTAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            VIAS_RUTAS vIAS_RUTAS = db.VIAS_RUTAS.Find(id);
            db.VIAS_RUTAS.Remove(vIAS_RUTAS);
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
