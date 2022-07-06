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

        public TBL_PERSONEL personelGetir()
        {
            Random random = new Random();
            List<TBL_PERSONEL> personel = new List<TBL_PERSONEL>();
            personel = db.TBL_PERSONEL.ToList();
            int count = personel.Count();
            int index = random.Next(count);
            var x = personel.ElementAt(index);
            return x;
        }
        public TBL_PERSONEL personelGetir(List<int> id)
        {
            Random random = new Random();
            List<TBL_PERSONEL> personel = new List<TBL_PERSONEL>();
            personel = db.TBL_PERSONEL.ToList();
            int index = random.Next(0,id.Count-1);
            var x = personel.ElementAt(id[index]-1);
            return x;
        }
        public TBL_PERSONEL personelSec()
        {
            int minDeger = 0;
            TBL_PERSONEL calisanGetir = new TBL_PERSONEL();
            List<TBL_PERSONEL> personel = new List<TBL_PERSONEL>();
            personel = db.TBL_PERSONEL.ToList();

            List<int> listPersonel = new List<int>();

            string[] zorlukDeger = new string[8];

            var personelId = db.TBL_GOREV.Select(t => t.PERSONELID).Distinct().ToList();

            int[] zorlukToplam = new int[8];
            int[] gorevSayisi = new int[8];
            List<int> listZorluk = new List<int>();
            for (int i = 1; i <= personel.Count; i++)
            {
                if (!personelId.Contains(i))
                {
                    listPersonel.Add(i);
                }
                else if (personelId.Contains(i))
                {
                    zorlukToplam[i - 1] = db.TBL_GOREV.Where(t => t.PERSONELID == i).Sum(a => a.ZORLUK);
                }

            }

            if (listPersonel.Count == 0)
            {
                minDeger = zorlukToplam.Min();
                for (int i = 0; i < 8; i++)
                {
                    if (zorlukToplam[i] == minDeger)
                    {
                        listZorluk.Add(i + 1);
                    }
                }
                calisanGetir = personelGetir(listZorluk);
            }

            else if (listPersonel.Count > 0 && listPersonel.Count<8)
            {
                calisanGetir = personelGetir(listPersonel);
            }
            else if (listPersonel.Count == 8)
            {
                calisanGetir = personelGetir();
            }

            return calisanGetir;
        }

        // GET: Gorev
        public ActionResult Index()
        {
            var tBL_GOREV = db.TBL_GOREV.Include(t => t.TBL_PERSONEL).OrderByDescending(x => x.ID);
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

            var personel = personelSec();
            ViewBag.PERSONELID = new SelectList(db.TBL_PERSONEL.Where(x => x.ID == personel.ID), "ID", "AD");
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
