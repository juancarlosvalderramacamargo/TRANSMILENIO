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
    public class BusesController : Controller
    {
        private INFRAESTRUCTURA_TRANSMILENIOEntities db = new INFRAESTRUCTURA_TRANSMILENIOEntities();

        // GET: Buses
        public ActionResult Index()
        {
            var bUSES = db.BUSES.Include(b => b.TIPOS_BUSES);
            return View(bUSES.ToList());
        }

        // GET: Buses/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.BUSES.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // GET: Buses/Create
        public ActionResult Create()
        {
            ViewBag.ID_TIPO_BUS = new SelectList(db.TIPOS_BUSES, "ID_TIPO_BUS", "TIPO_BUS");
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BUS,ID_TIPO_BUS,MARCA,MODELO,CONDUTOR,COLOR")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.BUSES.Add(bus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_TIPO_BUS = new SelectList(db.TIPOS_BUSES, "ID_TIPO_BUS", "TIPO_BUS", bus.ID_TIPO_BUS);
            return View(bus);
        }

        // GET: Buses/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.BUSES.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_TIPO_BUS = new SelectList(db.TIPOS_BUSES, "ID_TIPO_BUS", "TIPO_BUS", bus.ID_TIPO_BUS);
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BUS,ID_TIPO_BUS,MARCA,MODELO,CONDUTOR,COLOR")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_TIPO_BUS = new SelectList(db.TIPOS_BUSES, "ID_TIPO_BUS", "TIPO_BUS", bus.ID_TIPO_BUS);
            return View(bus);
        }

        // GET: Buses/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.BUSES.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Bus bus = db.BUSES.Find(id);
            db.BUSES.Remove(bus);
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
