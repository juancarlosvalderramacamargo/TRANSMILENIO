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
    public class TIPOS_BUSESController : Controller
    {
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

        // GET: TIPOS_BUSES
        public ActionResult Index()
        {
            return View(db.TIPOS_BUSES.ToList());
        }

        // GET: TIPOS_BUSES/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_BUSES tIPOS_BUSES = db.TIPOS_BUSES.Find(id);
            if (tIPOS_BUSES == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_BUSES);
        }

        // GET: TIPOS_BUSES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPOS_BUSES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TIPO_BUS,TIPO_BUS")] TIPOS_BUSES tIPOS_BUSES)
        {
            if (ModelState.IsValid)
            {
                decimal idTipoBus = db.TIPOS_BUSES.Max(y => y.ID_TIPO_BUS);
                tIPOS_BUSES.ID_TIPO_BUS = idTipoBus + 1;

                db.TIPOS_BUSES.Add(tIPOS_BUSES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPOS_BUSES);
        }

        // GET: TIPOS_BUSES/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_BUSES tIPOS_BUSES = db.TIPOS_BUSES.Find(id);
            if (tIPOS_BUSES == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_BUSES);
        }

        // POST: TIPOS_BUSES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TIPO_BUS,TIPO_BUS")] TIPOS_BUSES tIPOS_BUSES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOS_BUSES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPOS_BUSES);
        }

        // GET: TIPOS_BUSES/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_BUSES tIPOS_BUSES = db.TIPOS_BUSES.Find(id);
            if (tIPOS_BUSES == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_BUSES);
        }

        // POST: TIPOS_BUSES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TIPOS_BUSES tIPOS_BUSES = db.TIPOS_BUSES.Find(id);
            db.TIPOS_BUSES.Remove(tIPOS_BUSES);
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
