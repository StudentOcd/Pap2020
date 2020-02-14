using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pap2020.Models;

namespace Pap2020.Controllers
{
    public class UtilizadorsController : Controller
    {
        private SistemaGestaoEntities db = new SistemaGestaoEntities();

        // GET: Utilizadors
        public ActionResult Index()
        {
            var utilizador = db.Utilizador.Include(u => u.Tipo_Utilizador);
            return View(utilizador.ToList());
        }

        // GET: Utilizadors/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: Utilizadors/Create
        public ActionResult Create()
        {
            ViewBag.id_tipo = new SelectList(db.Tipo_Utilizador, "id_tipo", "nome_tipo");
            return View();
        }

        // POST: Utilizadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_utilizador,nome_utilizador,email_utilizador,senha_utilizador,telefone_utilizador,nr_processo,id_tipo")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                db.Utilizador.Add(utilizador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_tipo = new SelectList(db.Tipo_Utilizador, "id_tipo", "nome_tipo", utilizador.id_tipo);
            return View(utilizador);
        }

        // GET: Utilizadors/Edit/5
        public ActionResult Edit(int? id)
        {
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_utilizador,nome_utilizador,email_utilizador,senha_utilizador,telefone_utilizador,nr_processo,id_tipo")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilizador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_tipo = new SelectList(db.Tipo_Utilizador, "id_tipo", "nome_tipo", utilizador.id_tipo);
            return View(utilizador);
        }

        // GET: Utilizadors/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: Utilizadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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
