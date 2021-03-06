﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace VIVLIO.Controllers
{
    public class ConnexionRelController : Controller
    {
        private FSPCEntities db = new FSPCEntities();
        // GET: ConnexionRel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn(String login, String password, String remember)
        {
            HttpCookie uCookie = Request.Cookies.Get("userName");
            Session["signedIn"] = "false";

            //Affichage en fonction du cookie
            if (uCookie != null)
            {
                if (uCookie.Value != null)
                {
                    //Si le username est sauvegardé (Client)
                    if (uCookie.Value.Length > 0)
                    {

                        ViewBag.id = uCookie.Value;
                        ViewBag.check = "yes";
                    }
                    //En cas d'exception..
                    else
                    {
                        ViewBag.id = "";
                        ViewBag.check = "";
                    }
                }
            }
            //Si pas de cookie
            else
            {
                ViewBag.id = "";
                ViewBag.check = "";
            }

            //Sign in
            if (login != null && password != null)
            {
                if (login != "" && password != "")
                {
                    //Cherche dans les Users prédéfinis
                    foreach (var p in db.Users)
                    {
                        if (p != null)
                        {
                            //User trouvé
                            if (login.Equals(p.Login) && password.Equals(p.Password))
                            {
                                //Se souvenir de l'identifiant
                                if (remember == "yes")
                                {
                                    uCookie = new HttpCookie("userName");
                                    uCookie.Value = login;
                                    uCookie.Expires = DateTime.Now.AddDays(7);
                                    Response.Cookies.Add(uCookie);
                                }
                                //Oublier l'identifiant
                                else
                                {
                                    uCookie = new HttpCookie("userName");
                                    uCookie.Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies.Add(uCookie);
                                }
                                Session["type"] = p.Type;//type du user connecté
                                Session["userName"] = p.Prenom; //Nom du user connecté
                                Session["userID"] = p.UserID;
                                Session["signedIn"] = "true"; //Confirmation de status de connection
                                                              /*A modifier*/
                                if (p.Type == "Mod")
                                {
                                    return RedirectToAction("Index", "OFFERsMOD");
                                }
                                if (p.Type == "Admin")
                                {
                                    return RedirectToAction("Index", "Admin");
                                }
                                if (p.Type != "Block")
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                                
                            }

                            else
                            {
                                ViewBag.Comment = "Identifiant ou mot de passe invalide";
                            }
                        }

                    }
                }
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string login, string prenom, string nom, string email, string phonenumber, string school, HttpPostedFileBase photo, string password, string confirm)
        {
            if (login != null && password != null && prenom != null && nom != null && email != null && confirm != null)
            {
                if (login != "" && password != "" && prenom != "" && nom != "" && email != "" && confirm != "")
                {
                    using (db)
                    {
                        Users u = new Users();
                        u.Login = login;
                        u.Prenom = prenom;
                        u.Name = nom;
                        u.Email = email;
                        u.PhoneNumber = phonenumber;
                        u.CollègeName = school;
                        //u.Photo = photo;
                        u.Password = (string)Crypto.HashPassword(password);
                        db.Users.Add(u);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("SignIn", "ConnexionRel");
        }

        public ActionResult SignOut()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
    }
}