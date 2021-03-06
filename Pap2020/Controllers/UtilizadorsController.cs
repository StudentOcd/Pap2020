﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Pap2020.Models;

namespace Pap2020.Controllers
{
    public class UtilizadorsController : Controller
    {
        private SistemaGestaoEntities db = new SistemaGestaoEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Views/Utilizadors/Login/Login.cshtml");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login","Utilizadors");
        }

        [HttpPost]

        public ActionResult Login(UserLoginView userLogin)
        {
            if (ModelState.IsValid)
            {
                string passwordEncrypted = Crypto.crypto.GenerateSHA256String(userLogin.Password);
                var v = db.Utilizador.Where(u => u.nome_utilizador == userLogin.Username && u.senha_utilizador == passwordEncrypted);
                
                if (v != null)
                {
                    //Criar objeto que passe toda informação session
                    try
                    {

                        Session["username"] = v.First().nome_utilizador;
                        Session["Id"] = v.First().id_utilizador;
                        Session["Tipo"] = v.First().id_tipo;
                        FormsAuthentication.SetAuthCookie(v.First().nome_utilizador, false);
                    }
                    catch (InvalidOperationException e) {
                        TempData["errorMessage"] = "Email e/ou password erradas! Tente novamente";
                        return View("Error", new HandleErrorInfo(new Exception("Email e/ou password erradas! Tente novamente"), "Utilizadors", "Login"));
                    }
                    return RedirectToAction("Index","Relatorios");
                }
              
            }

            return View();
        }
        [Authorize]
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return View("Error");
            }
            var utilizador = db.Utilizador.Include(u => u.Tipo_Utilizador);
            return View(utilizador.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (Session["Id"] == null)
            {
                return View("Error");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizador.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }

        [Authorize]
        public ActionResult Create()
        {


            ViewBag.id_tipo = new SelectList(db.Tipo_Utilizador, "id_tipo", "nome_tipo");
            return View();
        }

        // POST: Utilizadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_utilizador,nome_utilizador,email_utilizador,senha_utilizador,telefone_utilizador,nr_processo,id_tipo")] Utilizador utilizador)
        {
            int Tipo = Convert.ToInt32(Session["Tipo"]);
            if (Tipo == 3 || Tipo == 0)
            {
                return View("Error");
            }

            if (Session["Id"] == null)
            {
                return View("Error");
            }
            if (ModelState.IsValid)
            {
                utilizador.senha_utilizador = Crypto.crypto.GenerateSHA256String(utilizador.senha_utilizador);

                db.Utilizador.Add(utilizador);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_tipo = new SelectList(db.Tipo_Utilizador, "id_tipo", "nome_tipo", utilizador.id_tipo);
            return View(utilizador);
        }

        // GET: Utilizadors/Create
        public ActionResult Register()
        {
            int Tipo = Convert.ToInt32(Session["Tipo"]);
            if (Tipo == 3 || Tipo == 0)
            {
                return View("Error");
            }

            ViewBag.id_tipo = new SelectList(db.Tipo_Utilizador, "id_tipo", "nome_tipo");
            return View("~/Views/Utilizadors/Register/Register.cshtml");
        }

        // POST: Utilizadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "id_utilizador,nome_utilizador,email_utilizador,senha_utilizador,telefone_utilizador,nr_processo,id_tipo")] Utilizador utilizador)
        {
            int Tipo = Convert.ToInt32(Session["Tipo"]);
            if (Tipo == 3 || Tipo == 0)
            {
                return View("Error");
            }

            if (db.Utilizador.Any(u => u.email_utilizador == utilizador.email_utilizador))
            {
                ModelState.AddModelError("Email", "Esse email já se encontra registado no sistema. Tente novamente!");
            }
            if (ModelState.IsValid)
            {
                utilizador.senha_utilizador = Crypto.crypto.GenerateSHA256String(utilizador.senha_utilizador);

                db.Utilizador.Add(utilizador);
                TempData["successMessage"] = "Utilizador registado com sucesso";
                db.SaveChanges();
                return RedirectToAction("Login", "Utilizadors");
            }

            ViewBag.id_tipo = new SelectList(db.Tipo_Utilizador, "id_tipo", "nome_tipo", utilizador.id_tipo);
            return View(utilizador);
        }

        // GET: Utilizadors/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            int Tipo = Convert.ToInt32(Session["Tipo"]);
            if (Tipo == 3 || Tipo == 0)
            {
                return View("Error");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizador.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_tipo = new SelectList(db.Tipo_Utilizador, "id_tipo", "nome_tipo", utilizador.id_tipo);
            return View(utilizador);
        }

        // POST: Utilizadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "id_utilizador,nome_utilizador,email_utilizador,senha_utilizador,telefone_utilizador,nr_processo,id_tipo")] Utilizador utilizador)
        {
            int Tipo = Convert.ToInt32(Session["Tipo"]);
            if (Tipo == 3 || Tipo == 0)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                db.Entry(utilizador).State = EntityState.Modified;
                utilizador.senha_utilizador = Crypto.crypto.GenerateSHA256String(utilizador.senha_utilizador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_tipo = new SelectList(db.Tipo_Utilizador, "id_tipo", "nome_tipo", utilizador.id_tipo);
            return View(utilizador);
        }

        // GET: Utilizadors/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            int Tipo = Convert.ToInt32(Session["Tipo"]);
            if (Tipo == 3 || Tipo == 0)
            {
                return View("Error");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizador.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            if (db.Relatorio.Any(e => e.id_aluno == id || db.Relatorio.Any(x => x.id_monitor == id || db.Relatorio.Any(a => a.id_professor == id))))
            {
                var handleErrorInfo = new HandleErrorInfo(new Exception("Não é possível remover o Utilizador dado que existe(m) relatórios pertencentes ao mesmo!"), "Utilizadors", "Index");
                return View("Error", handleErrorInfo);
            }
            return View(utilizador);
          
        }

        // POST: Utilizadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int Tipo = Convert.ToInt32(Session["Tipo"]);
            if (Tipo == 3 || Tipo == 0)
            {
                return View("Error");
            }

            Utilizador utilizador = db.Utilizador.Find(id);
            db.Utilizador.Remove(utilizador);
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
