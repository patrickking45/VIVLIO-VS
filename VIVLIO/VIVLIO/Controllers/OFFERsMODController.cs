using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VIVLIO;

namespace VIVLIO.Controllers
{
    public class OFFERsMODController : Controller
    {
        private FSPCEntities db = new FSPCEntities();

        // GET: OFFERsMOD
        public ActionResult Index()
        {
            Users u = db.Users.Find((int)Session["userID"]);
            string typp = u.Type;
            if (typp != "Mod")
            {
                return RedirectToAction("Index", "Home");
            }

            var oFFER = db.OFFER.Include(o => o.MATIERE).Include(o => o.NIVEAU).Include(o => o.Users);
            return View(oFFER.ToList());
        }

        // GET: OFFERsMOD/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OFFER oFFER = db.OFFER.Find(id);
            if (oFFER == null)
            {
                return HttpNotFound();
            }
            return View(oFFER);
        }
        public ActionResult AcceptOffer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OFFER oFFER = db.OFFER.Find(id);
            if (oFFER == null)
            {
                return HttpNotFound();
            }
            else
            {
                oFFER.STATUS = "Selling";
                DateTime thisday = DateTime.Today;
                oFFER.POSTEDDATE = thisday;
                db.Entry(oFFER).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "OFFERsMOD");

        }
        public ActionResult RefuseOffer(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OFFER oFFER = db.OFFER.Find(id);
            if (oFFER == null)
            {
                return HttpNotFound();
            }
            else
            {
                oFFER.STATUS = "Blocked";
                db.Entry(oFFER).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "OFFERsMOD");


        }

        // GET: OFFERsMOD/Create
        public ActionResult Create()
        {
            ViewBag.MATIEREID = new SelectList(db.MATIERE, "MATIEREID", "SUBJECTMATTER");
            ViewBag.NIVEAUID = new SelectList(db.NIVEAU, "NIVEAUID", "NIVEAUNAME");
            ViewBag.USERID = new SelectList(db.Users, "UserID", "Login");
            return View();
        }

        // POST: OFFERsMOD/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OFFERID,MATIEREID,NIVEAUID,USERID,PRICE,CREATIONDATE,POSTEDDATE,STATUS,PHOTO,AUTHOR_COMPANYNAME,CONDITION,DESCRIPTION,NAME")] OFFER oFFER)
        {
            if (ModelState.IsValid)
            {
                db.OFFER.Add(oFFER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MATIEREID = new SelectList(db.MATIERE, "MATIEREID", "SUBJECTMATTER", oFFER.MATIEREID);
            ViewBag.NIVEAUID = new SelectList(db.NIVEAU, "NIVEAUID", "NIVEAUNAME", oFFER.NIVEAUID);
            ViewBag.USERID = new SelectList(db.Users, "UserID", "Login", oFFER.USERID);
            return View(oFFER);
        }

        // GET: OFFERsMOD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OFFER oFFER = db.OFFER.Find(id);
            if (oFFER == null)
            {
                return HttpNotFound();
            }
            ViewBag.MATIEREID = new SelectList(db.MATIERE, "MATIEREID", "SUBJECTMATTER", oFFER.MATIEREID);
            ViewBag.NIVEAUID = new SelectList(db.NIVEAU, "NIVEAUID", "NIVEAUNAME", oFFER.NIVEAUID);
            ViewBag.USERID = new SelectList(db.Users, "UserID", "Login", oFFER.USERID);
            return View(oFFER);
        }

        // POST: OFFERsMOD/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OFFERID,MATIEREID,NIVEAUID,USERID,PRICE,CREATIONDATE,POSTEDDATE,STATUS,PHOTO,AUTHOR_COMPANYNAME,CONDITION,DESCRIPTION,NAME")] OFFER oFFER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oFFER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MATIEREID = new SelectList(db.MATIERE, "MATIEREID", "SUBJECTMATTER", oFFER.MATIEREID);
            ViewBag.NIVEAUID = new SelectList(db.NIVEAU, "NIVEAUID", "NIVEAUNAME", oFFER.NIVEAUID);
            ViewBag.USERID = new SelectList(db.Users, "UserID", "Login", oFFER.USERID);
            return View(oFFER);
        }

        // GET: OFFERsMOD/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OFFER oFFER = db.OFFER.Find(id);
            if (oFFER == null)
            {
                return HttpNotFound();
            }
            return View(oFFER);
        }

        // POST: OFFERsMOD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OFFER oFFER = db.OFFER.Find(id);
            db.OFFER.Remove(oFFER);
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
