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
    public class RUTAS_PARADEROSController : Controller
    {
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

        // GET: RUTAS_PARADEROS
        public ActionResult Index()
        {
            return View(db.RUTAS_PARADEROS.ToList());
        }

        // GET: RUTAS_PARADEROS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUTAS_PARADEROS rUTAS_PARADEROS = db.RUTAS_PARADEROS.Find(id);
            if (rUTAS_PARADEROS == null)
            {
                return HttpNotFound();
            }
            return View(rUTAS_PARADEROS);
        }

        // GET: RUTAS_PARADEROS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RUTAS_PARADEROS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PARADERO,ID_RUTA,POSICION_RUTA")] RUTAS_PARADEROS rUTAS_PARADEROS)
        {
            if (ModelState.IsValid)
            {
                db.RUTAS_PARADEROS.Add(rUTAS_PARADEROS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rUTAS_PARADEROS);
        }

        // GET: RUTAS_PARADEROS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUTAS_PARADEROS rUTAS_PARADEROS = db.RUTAS_PARADEROS.Find(id);
            if (rUTAS_PARADEROS == null)
            {
                return HttpNotFound();
            }
            return View(rUTAS_PARADEROS);
        }

        // POST: RUTAS_PARADEROS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PARADERO,ID_RUTA,POSICION_RUTA")] RUTAS_PARADEROS rUTAS_PARADEROS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rUTAS_PARADEROS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rUTAS_PARADEROS);
        }

        // GET: RUTAS_PARADEROS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUTAS_PARADEROS rUTAS_PARADEROS = db.RUTAS_PARADEROS.Find(id);
            if (rUTAS_PARADEROS == null)
            {
                return HttpNotFound();
            }
            return View(rUTAS_PARADEROS);
        }

        // POST: RUTAS_PARADEROS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            RUTAS_PARADEROS rUTAS_PARADEROS = db.RUTAS_PARADEROS.Find(id);
            db.RUTAS_PARADEROS.Remove(rUTAS_PARADEROS);
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
