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
    public class MATIEREsController : Controller
    {
        private FSPCEntities db = new FSPCEntities();

        // GET: MATIEREs
        public ActionResult Index()
        {
            return View(db.MATIERE.ToList());
        }

        // GET: MATIEREs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATIERE mATIERE = db.MATIERE.Find(id);
            if (mATIERE == null)
            {
                return HttpNotFound();
            }
            return View(mATIERE);
        }

        // GET: MATIEREs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MATIEREs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATIEREID,SUBJECTMATTER")] MATIERE mATIERE)
        {
            if (ModelState.IsValid)
            {
                db.MATIERE.Add(mATIERE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mATIERE);
        }

        // GET: MATIEREs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATIERE mATIERE = db.MATIERE.Find(id);
            if (mATIERE == null)
            {
                return HttpNotFound();
            }
            return View(mATIERE);
        }

        // POST: MATIEREs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATIEREID,SUBJECTMATTER")] MATIERE mATIERE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mATIERE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mATIERE);
        }

        // GET: MATIEREs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MATIERE mATIERE = db.MATIERE.Find(id);
            if (mATIERE == null)
            {
                return HttpNotFound();
            }
            return View(mATIERE);
        }

        // POST: MATIEREs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MATIERE mATIERE = db.MATIERE.Find(id);
            db.MATIERE.Remove(mATIERE);
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
