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
    public class TIPOS_RUTASController : Controller
    {
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

        // GET: TIPOS_RUTAS
        public ActionResult Index()
        {
            return View(db.TIPOS_RUTAS.ToList());
        }

        // GET: TIPOS_RUTAS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_RUTAS tIPOS_RUTAS = db.TIPOS_RUTAS.Find(id);
            if (tIPOS_RUTAS == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_RUTAS);
        }

        // GET: TIPOS_RUTAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPOS_RUTAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TIPO_RUTA,TIPO_RUTA")] TIPOS_RUTAS tIPOS_RUTAS)
        {
            if (ModelState.IsValid)
            {
                decimal idTipoRuta = db.TIPOS_RUTAS.Max(y => y.ID_TIPO_RUTA);
                tIPOS_RUTAS.ID_TIPO_RUTA = idTipoRuta + 1;

                db.TIPOS_RUTAS.Add(tIPOS_RUTAS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPOS_RUTAS);
        }

        // GET: TIPOS_RUTAS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_RUTAS tIPOS_RUTAS = db.TIPOS_RUTAS.Find(id);
            if (tIPOS_RUTAS == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_RUTAS);
        }

        // POST: TIPOS_RUTAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TIPO_RUTA,TIPO_RUTA")] TIPOS_RUTAS tIPOS_RUTAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOS_RUTAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPOS_RUTAS);
        }

        // GET: TIPOS_RUTAS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_RUTAS tIPOS_RUTAS = db.TIPOS_RUTAS.Find(id);
            if (tIPOS_RUTAS == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_RUTAS);
        }

        // POST: TIPOS_RUTAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TIPOS_RUTAS tIPOS_RUTAS = db.TIPOS_RUTAS.Find(id);
            db.TIPOS_RUTAS.Remove(tIPOS_RUTAS);
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
