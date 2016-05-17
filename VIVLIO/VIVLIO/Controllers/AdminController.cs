using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace VIVLIO.Controllers
{
    public class AdminController : Controller
    {
        private FSPCEntities DI = new FSPCEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View(DI.Users.ToList());
        }
        public ActionResult SetClientToMod(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = DI.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                user.Type = "Mod";
                DI.SaveChanges();
            }
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult Block(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = DI.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                user.Type = "Block";
                DI.SaveChanges();
            }
            return RedirectToAction("Index", "Admin");


        }

    }
}