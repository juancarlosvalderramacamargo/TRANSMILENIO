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
    public class RUTAsController : Controller
    {
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

        // GET: RUTAs
        public ActionResult Index()
        {
            var rUTAS = db.RUTAS.Include(r => r.Bus).Include(r => r.TIPOS_RUTAS);
            return View(rUTAS.ToList());
        }

        // GET: RUTAs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUTA rUTA = db.RUTAS.Find(id);
            if (rUTA == null)
            {
                return HttpNotFound();
            }
            return View(rUTA);
        }

        // GET: RUTAs/Create
        public ActionResult Create()
        {
            ViewBag.ID_BUS = new SelectList(db.BUSES, "ID_BUS", "MARCA");
            ViewBag.ID_TIPO_RUTA = new SelectList(db.TIPOS_RUTAS, "ID_TIPO_RUTA", "TIPO_RUTA");
            return View();
        }

        // POST: RUTAs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_RUTA,ID_BUS,ID_TIPO_RUTA,NOMBRE_RUTA")] RUTA rUTA)
        {
            if (ModelState.IsValid)
            {
                decimal idRuta = db.RUTAS.Max(y => y.ID_RUTA);
                rUTA.ID_RUTA = idRuta + 1;

                db.RUTAS.Add(rUTA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_BUS = new SelectList(db.BUSES, "ID_BUS", "MARCA", rUTA.ID_BUS);
            ViewBag.ID_TIPO_RUTA = new SelectList(db.TIPOS_RUTAS, "ID_TIPO_RUTA", "TIPO_RUTA", rUTA.ID_TIPO_RUTA);
            return View(rUTA);
        }

        // GET: RUTAs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUTA rUTA = db.RUTAS.Find(id);
            if (rUTA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_BUS = new SelectList(db.BUSES, "ID_BUS", "MARCA", rUTA.ID_BUS);
            ViewBag.ID_TIPO_RUTA = new SelectList(db.TIPOS_RUTAS, "ID_TIPO_RUTA", "TIPO_RUTA", rUTA.ID_TIPO_RUTA);
            return View(rUTA);
        }

        // POST: RUTAs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_RUTA,ID_BUS,ID_TIPO_RUTA,NOMBRE_RUTA")] RUTA rUTA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rUTA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_BUS = new SelectList(db.BUSES, "ID_BUS", "MARCA", rUTA.ID_BUS);
            ViewBag.ID_TIPO_RUTA = new SelectList(db.TIPOS_RUTAS, "ID_TIPO_RUTA", "TIPO_RUTA", rUTA.ID_TIPO_RUTA);
            return View(rUTA);
        }

        // GET: RUTAs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUTA rUTA = db.RUTAS.Find(id);
            if (rUTA == null)
            {
                return HttpNotFound();
            }
            return View(rUTA);
        }

        // POST: RUTAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            RUTA rUTA = db.RUTAS.Find(id);
            db.RUTAS.Remove(rUTA);
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
