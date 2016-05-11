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
    public class OFFERsController : Controller
    {
        static int _MAX = 5;
        private FSPCEntities db = new FSPCEntities();

        // GET: OFFERs
        [HttpGet]
        public ActionResult Index(int? page)
        {
            var oFFER = db.OFFER.Include(o => o.MATIERE).Include(o => o.NIVEAU).Include(o => o.Users);

            if (page == null)
            {
                //var offers4page = ListOffersByPage(oFFER.ToList(), 1);
            }
            else
            {
                //var offers4page = ListOffersByPage(oFFER.ToList(), page);
            }



            ViewBag.Num = oFFER.Count() / _MAX;

            //Get All Matieres
            var matieres = from m in db.MATIERE select m.SUBJECTMATTER;
            ViewBag.ListofMatiere = matieres.ToList();

            //Get All Niveaux
            var niveaux = from n in db.NIVEAU select n.NIVEAUNAME;
            ViewBag.ListofNiveau = niveaux.ToList();

            //return View(offers4page);
            return View(oFFER);
        }

        [HttpPost, ActionName("Index")]
        public ActionResult IndexPost(int? page, string Matiere, string Niveau) {

            var oFFER = db.OFFER.Include(o => o.MATIERE).Include(o => o.NIVEAU).Include(o => o.Users);

            if (Matiere != "")
            {
                if (Niveau != "")
                {
                    oFFER = db.OFFER.Include(o => o.MATIERE).Include(o => o.NIVEAU).Include(o => o.Users).Where(o => o.MATIERE.SUBJECTMATTER == Matiere).Where(o => o.NIVEAU.NIVEAUNAME == Niveau);
                }
                else
                {
                    oFFER = db.OFFER.Include(o => o.MATIERE).Include(o => o.NIVEAU).Include(o => o.Users).Where(o => o.MATIERE.SUBJECTMATTER == Matiere);
                }
            }
            else {
                if (Niveau != "")
                {
                    oFFER = db.OFFER.Include(o => o.MATIERE).Include(o => o.NIVEAU).Include(o => o.Users).Where(o => o.NIVEAU.NIVEAUNAME == Niveau);
                }
                else
                {
                    oFFER = db.OFFER.Include(o => o.MATIERE).Include(o => o.NIVEAU).Include(o => o.Users);
                }
            }

            //Get All Matieres
            var matieres = from m in db.MATIERE select m.SUBJECTMATTER;
            ViewBag.ListofMatiere = matieres.ToList();

            //Get All Niveaux
            var niveaux = from n in db.NIVEAU select n.NIVEAUNAME;
            ViewBag.ListofNiveau = niveaux.ToList();

            return View(oFFER);
        }

        private System.Collections.Generic.List<VIVLIO.OFFER> ListOffersByPage(System.Collections.Generic.List<VIVLIO.OFFER> allOffers, int page)
        {
            System.Collections.Generic.List<OFFER> allOffersFinal = new System.Collections.Generic.List<OFFER>();


            for (var j = ((page - 1) * _MAX + 1); j <= (page * _MAX); j++)
            {
                if (allOffers.Count >= j)
                {
                    allOffersFinal[(j - 1) / _MAX + 1] = allOffers[j];
                }
                else
                {
                    break;
                }

            }

            return allOffersFinal;
        }


        //  [HttpPost]
        //  public ActionResult Index(SortingPagingInfo info)
        //  {
        //         IQueryable<OFFER> query = null;
        //          switch (info.SortField)
        //          {
        //             case "CustomerID":
        // query = (info.SortDirection == "ascending" ?
        // db.OFFER.OrderBy(c => c.OFFERID) :
        // db.OFFER.OrderByDescending(c => c.OFFERID));
        //                break;
        //            case "CompanyName":
        //query = (info.SortDirection == "ascending" ?
        //db.OFFER.OrderBy(c => c.POSTEDDATE) :
        // db.OFFER.OrderByDescending(c => c.POSTEDDATE));
        //                break;
        //           case "ContactName":
        // query = (info.SortDirection == "ascending" ?
        // db.OFFER.OrderBy(c => c.NIVEAUID) :
        //db.OFFER.OrderByDescending(c => c.NIVEAUID));
        //               break;
        //           case "Country":
        //query = (info.SortDirection == "ascending" ?
        //db.OFFER.OrderBy(c => c.PRICE) :
        //db.OFFER.OrderByDescending(c => c.PRICE));
        //                break;
        //}
        //query = query.Skip(info.CurrentPageIndex
        //             * info.PageSize).Take(info.PageSize);
        // ViewBag.SortingPagingInfo = info;
        //List<OFFER> model = query.ToList();
        //        return View(model);

        //}




        // GET: OFFERs/Details/5
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

        // GET: OFFERs/Create
        public ActionResult Create()
        {
            ViewBag.MATIEREID = new SelectList(db.MATIERE, "MATIEREID", "SUBJECTMATTER");
            ViewBag.NIVEAUID = new SelectList(db.NIVEAU, "NIVEAUID", "NIVEAUNAME");
            ViewBag.USERID = new SelectList(db.Users, "UserID", "Login");
            return View();
        }

        // POST: OFFERs/Create
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

        // GET: OFFERs/Edit/5
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

        // POST: OFFERs/Edit/5
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

        // GET: OFFERs/Delete/5
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

        // POST: OFFERs/Delete/5
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