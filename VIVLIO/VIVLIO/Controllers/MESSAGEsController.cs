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
        [HttpGet]
        public ActionResult Index(String inbox)
        {
            int userID = (int)Session["userID"];
            var mESSAGE = db.MESSAGE.Include(m => m.Users).Include(m => m.Users1).Where(m => m.Users.UserID == userID).OrderByDescending(m => m.MESSAGEID);
            if (inbox == "Send")
            {
                mESSAGE = db.MESSAGE.Include(m => m.Users).Include(m => m.Users1).Where(m => m.Users1.UserID == userID).OrderByDescending(m => m.MESSAGEID);
            }
            else {
                mESSAGE = db.MESSAGE.Include(m => m.Users).Include(m => m.Users1).Where(m => m.Users.UserID == userID).OrderByDescending(m => m.MESSAGEID);
            }

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
                return RedirectToAction("Index");
            }

            if ((int)Session["UserID"] == mESSAGE.RECEIVERID)
            {
                if (mESSAGE.STATUS == "UNSEEN")
                {
                    mESSAGE.STATUS = "SEEN";
                    db.Entry(mESSAGE).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return View(mESSAGE);
            }
            else {
                if ((int)Session["UserID"] == mESSAGE.SENDERID) {
                    return View(mESSAGE);
                }
                return RedirectToAction("Index");
            }

            
        }

        // GET: MESSAGEs/Create
        public ActionResult Create()
        {
            ViewBag.RECEIVERID = new SelectList(db.Users, "UserID", "Login");
            return View();
        }

        // POST: MESSAGEs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MESSAGEID,Subject,MESSAGETEXT")] MESSAGE mESSAGE, string RECEIVERID)
        {
            if (ModelState.IsValid)
            {
                var receiver = db.Users.Where(u => u.Login == RECEIVERID).FirstOrDefault();

                mESSAGE.SENDERID = (int)Session["userID"];
                mESSAGE.RECEIVERID = receiver.UserID;
                mESSAGE.STATUS = "UNSEEN";
                db.MESSAGE.Add(mESSAGE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RECEIVERID = new SelectList(db.Users, "UserID", "Login", mESSAGE.RECEIVERID);
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

        public ActionResult userAutoComplete()
        {
            string term = Request.QueryString["term"].ToLower();
            var result = from u in db.Users
                         where u.Login.ToLower().Contains(term)
                         select u.Login;
            return Json(result,JsonRequestBehavior.AllowGet);
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
