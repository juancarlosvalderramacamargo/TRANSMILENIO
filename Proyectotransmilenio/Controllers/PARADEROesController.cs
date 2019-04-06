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
    public class PARADEROesController : Controller
    {
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

        // GET: PARADEROes
        public ActionResult Index()
        {
            var pARADEROS = db.PARADEROS.Include(p => p.TIPOS_PARADEROS).Include(p => p.VIA);
            return View(pARADEROS.ToList());
        }

        // GET: PARADEROes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARADERO pARADERO = db.PARADEROS.Find(id);
            if (pARADERO == null)
            {
                return HttpNotFound();
            }
            return View(pARADERO);
        }

        // GET: PARADEROes/Create
        public ActionResult Create()
        {
            ViewBag.ID_TIPO_PAR = new SelectList(db.TIPOS_PARADEROS, "ID_TIPO_PAR", "TIPO_PARADERO");
            ViewBag.ID_VIA = new SelectList(db.VIAS, "ID_VIA", "NOMBRE_VIA");
            return View();
        }

        // POST: PARADEROes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PARADERO,ID_TIPO_PAR,ID_VIA,NOMBRE_PARADERO,UBICACION_PARADERO,POSICION_VIA")] PARADERO pARADERO)
        {
            if (ModelState.IsValid)
            {
                decimal idParadero = db.PARADEROS.Max(y => y.ID_PARADERO);
                pARADERO.ID_PARADERO = idParadero + 1;

                db.PARADEROS.Add(pARADERO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_TIPO_PAR = new SelectList(db.TIPOS_PARADEROS, "ID_TIPO_PAR", "TIPO_PARADERO", pARADERO.ID_TIPO_PAR);
            ViewBag.ID_VIA = new SelectList(db.VIAS, "ID_VIA", "NOMBRE_VIA", pARADERO.ID_VIA);
            return View(pARADERO);
        }

        // GET: PARADEROes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARADERO pARADERO = db.PARADEROS.Find(id);
            if (pARADERO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_TIPO_PAR = new SelectList(db.TIPOS_PARADEROS, "ID_TIPO_PAR", "TIPO_PARADERO", pARADERO.ID_TIPO_PAR);
            ViewBag.ID_VIA = new SelectList(db.VIAS, "ID_VIA", "NOMBRE_VIA", pARADERO.ID_VIA);
            return View(pARADERO);
        }

        // POST: PARADEROes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PARADERO,ID_TIPO_PAR,ID_VIA,NOMBRE_PARADERO,UBICACION_PARADERO,POSICION_VIA")] PARADERO pARADERO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pARADERO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_TIPO_PAR = new SelectList(db.TIPOS_PARADEROS, "ID_TIPO_PAR", "TIPO_PARADERO", pARADERO.ID_TIPO_PAR);
            ViewBag.ID_VIA = new SelectList(db.VIAS, "ID_VIA", "NOMBRE_VIA", pARADERO.ID_VIA);
            return View(pARADERO);
        }

        // GET: PARADEROes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARADERO pARADERO = db.PARADEROS.Find(id);
            if (pARADERO == null)
            {
                return HttpNotFound();
            }
            return View(pARADERO);
        }

        // POST: PARADEROes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PARADERO pARADERO = db.PARADEROS.Find(id);
            db.PARADEROS.Remove(pARADERO);
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
