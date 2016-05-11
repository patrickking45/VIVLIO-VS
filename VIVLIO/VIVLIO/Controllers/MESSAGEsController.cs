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
    public class MESSAGEsController : Controller
    {
        private FSPCEntities db = new FSPCEntities();

        // GET: MESSAGEs
        public ActionResult Index()
        {
            int userID = (int)Session["userID"];
            var mESSAGE = db.MESSAGE.Include(m => m.Users).Include(m => m.Users1).Where(m => m.Users1.UserID == userID);
            return View(mESSAGE.ToList());
        }

        // GET: MESSAGEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MESSAGE mESSAGE = db.MESSAGE.Find(id);
            if (mESSAGE == null)
            {
                return HttpNotFound();
            }
            return View(mESSAGE);
        }

        // GET: MESSAGEs/Create
        public ActionResult Create()
        {
            ViewBag.RECEIVERID = new SelectList(db.Users, "UserID", "Login");
            ViewBag.SENDERID = new SelectList(db.Users, "UserID", "Login");
            return View();
        }

        // POST: MESSAGEs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MESSAGEID,SENDERID,RECEIVERID,MESSAGETEXT,STATUS")] MESSAGE mESSAGE)
        {
            if (ModelState.IsValid)
            {
                db.MESSAGE.Add(mESSAGE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RECEIVERID = new SelectList(db.Users, "UserID", "Login", mESSAGE.RECEIVERID);
            ViewBag.SENDERID = new SelectList(db.Users, "UserID", "Login", mESSAGE.SENDERID);
            return View(mESSAGE);
        }

        // GET: MESSAGEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MESSAGE mESSAGE = db.MESSAGE.Find(id);
            if (mESSAGE == null)
            {
                return HttpNotFound();
            }
            ViewBag.RECEIVERID = new SelectList(db.Users, "UserID", "Login", mESSAGE.RECEIVERID);
            ViewBag.SENDERID = new SelectList(db.Users, "UserID", "Login", mESSAGE.SENDERID);
            return View(mESSAGE);
        }

        // POST: MESSAGEs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MESSAGEID,SENDERID,RECEIVERID,MESSAGETEXT,STATUS")] MESSAGE mESSAGE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mESSAGE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RECEIVERID = new SelectList(db.Users, "UserID", "Login", mESSAGE.RECEIVERID);
            ViewBag.SENDERID = new SelectList(db.Users, "UserID", "Login", mESSAGE.SENDERID);
            return View(mESSAGE);
        }

        // GET: MESSAGEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MESSAGE mESSAGE = db.MESSAGE.Find(id);
            if (mESSAGE == null)
            {
                return HttpNotFound();
            }
            return View(mESSAGE);
        }

        // POST: MESSAGEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MESSAGE mESSAGE = db.MESSAGE.Find(id);
            db.MESSAGE.Remove(mESSAGE);
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
