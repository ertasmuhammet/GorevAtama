using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GorevAtama.Models;

namespace GorevAtama.Controllers
{
    public class PersonelController : Controller
    {
        private DB_NETLOGEntities db = new DB_NETLOGEntities();

        // GET: Personel
        public ActionResult Index()
        {
            return View(db.TBL_PERSONEL.ToList());
        }

        // GET: Personel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_PERSONEL tBL_PERSONEL = db.TBL_PERSONEL.Find(id);
            if (tBL_PERSONEL == null)
            {
                return HttpNotFound();
            }
            return View(tBL_PERSONEL);
        }

        // GET: Personel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AD,SOYAD")] TBL_PERSONEL tBL_PERSONEL)
        {
            if (ModelState.IsValid)
            {
                db.TBL_PERSONEL.Add(tBL_PERSONEL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tBL_PERSONEL);
        }

        // GET: Personel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_PERSONEL tBL_PERSONEL = db.TBL_PERSONEL.Find(id);
            if (tBL_PERSONEL == null)
            {
                return HttpNotFound();
            }
            return View(tBL_PERSONEL);
        }

        // POST: Personel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AD,SOYAD")] TBL_PERSONEL tBL_PERSONEL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_PERSONEL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBL_PERSONEL);
        }

        // GET: Personel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_PERSONEL tBL_PERSONEL = db.TBL_PERSONEL.Find(id);
            if (tBL_PERSONEL == null)
            {
                return HttpNotFound();
            }
            return View(tBL_PERSONEL);
        }

        // POST: Personel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_PERSONEL tBL_PERSONEL = db.TBL_PERSONEL.Find(id);
            db.TBL_PERSONEL.Remove(tBL_PERSONEL);
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
