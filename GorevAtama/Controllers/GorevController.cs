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
    public class GorevController : Controller
    {
        private DB_NETLOGEntities db = new DB_NETLOGEntities();

        // GET: Gorev
        public ActionResult Index()
        {
            var tBL_GOREV = db.TBL_GOREV.Include(t => t.TBL_PERSONEL);
            return View(tBL_GOREV.ToList());
        }

        // GET: Gorev/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_GOREV tBL_GOREV = db.TBL_GOREV.Find(id);
            if (tBL_GOREV == null)
            {
                return HttpNotFound();
            }
            return View(tBL_GOREV);
        }

        // GET: Gorev/Create
        public ActionResult Create()
        {
            ViewBag.PERSONELID = new SelectList(db.TBL_PERSONEL, "ID", "AD");
            return View();
        }

        // POST: Gorev/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AD,ZORLUK,PERSONELID")] TBL_GOREV tBL_GOREV)
        {
            if (ModelState.IsValid)
            {
                db.TBL_GOREV.Add(tBL_GOREV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PERSONELID = new SelectList(db.TBL_PERSONEL, "ID", "AD", tBL_GOREV.PERSONELID);
            return View(tBL_GOREV);
        }

        // GET: Gorev/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_GOREV tBL_GOREV = db.TBL_GOREV.Find(id);
            if (tBL_GOREV == null)
            {
                return HttpNotFound();
            }
            ViewBag.PERSONELID = new SelectList(db.TBL_PERSONEL, "ID", "AD", tBL_GOREV.PERSONELID);
            return View(tBL_GOREV);
        }

        // POST: Gorev/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AD,ZORLUK,PERSONELID")] TBL_GOREV tBL_GOREV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_GOREV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PERSONELID = new SelectList(db.TBL_PERSONEL, "ID", "AD", tBL_GOREV.PERSONELID);
            return View(tBL_GOREV);
        }

        // GET: Gorev/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_GOREV tBL_GOREV = db.TBL_GOREV.Find(id);
            if (tBL_GOREV == null)
            {
                return HttpNotFound();
            }
            return View(tBL_GOREV);
        }

        // POST: Gorev/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_GOREV tBL_GOREV = db.TBL_GOREV.Find(id);
            db.TBL_GOREV.Remove(tBL_GOREV);
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
