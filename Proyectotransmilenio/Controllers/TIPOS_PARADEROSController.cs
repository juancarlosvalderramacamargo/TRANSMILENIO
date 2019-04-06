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
    public class TIPOS_PARADEROSController : Controller
    {
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

        // GET: TIPOS_PARADEROS
        public ActionResult Index()
        {
            return View(db.TIPOS_PARADEROS.ToList());
        }

        // GET: TIPOS_PARADEROS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_PARADEROS tIPOS_PARADEROS = db.TIPOS_PARADEROS.Find(id);
            if (tIPOS_PARADEROS == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_PARADEROS);
        }

        // GET: TIPOS_PARADEROS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPOS_PARADEROS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TIPO_PAR,TIPO_PARADERO")] TIPOS_PARADEROS tIPOS_PARADEROS)
        {
            if (ModelState.IsValid)
            {
                decimal idTipoParadero = db.TIPOS_PARADEROS.Max(y => y.ID_TIPO_PAR);
                tIPOS_PARADEROS.ID_TIPO_PAR = idTipoParadero + 1;

                db.TIPOS_PARADEROS.Add(tIPOS_PARADEROS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPOS_PARADEROS);
        }

        // GET: TIPOS_PARADEROS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_PARADEROS tIPOS_PARADEROS = db.TIPOS_PARADEROS.Find(id);
            if (tIPOS_PARADEROS == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_PARADEROS);
        }

        // POST: TIPOS_PARADEROS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TIPO_PAR,TIPO_PARADERO")] TIPOS_PARADEROS tIPOS_PARADEROS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOS_PARADEROS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPOS_PARADEROS);
        }

        // GET: TIPOS_PARADEROS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_PARADEROS tIPOS_PARADEROS = db.TIPOS_PARADEROS.Find(id);
            if (tIPOS_PARADEROS == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_PARADEROS);
        }

        // POST: TIPOS_PARADEROS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TIPOS_PARADEROS tIPOS_PARADEROS = db.TIPOS_PARADEROS.Find(id);
            db.TIPOS_PARADEROS.Remove(tIPOS_PARADEROS);
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
