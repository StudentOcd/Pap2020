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
    public class Tipo_UtilizadorController : Controller
    {
        private SistemaGestaoEntities db = new SistemaGestaoEntities();

        // GET: Tipo_Utilizador
        public ActionResult Index()
        {
            return View(db.Tipo_Utilizador.ToList());
        }

        // GET: Tipo_Utilizador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Utilizador tipo_Utilizador = db.Tipo_Utilizador.Find(id);
            if (tipo_Utilizador == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Utilizador);
        }
         
        // GET: Tipo_Utilizador/Create adas
        public ActionResult Create() 
        {
            return View();
        }

        // POST: Tipo_Utilizador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo,nome_tipo")] Tipo_Utilizador tipo_Utilizador)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Utilizador.Add(tipo_Utilizador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Utilizador);
        }

        // GET: Tipo_Utilizador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Utilizador tipo_Utilizador = db.Tipo_Utilizador.Find(id);
            if (tipo_Utilizador == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Utilizador);
        }

        // POST: Tipo_Utilizador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo,nome_tipo")] Tipo_Utilizador tipo_Utilizador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Utilizador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Utilizador);
        }

        // GET: Tipo_Utilizador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Utilizador tipo_Utilizador = db.Tipo_Utilizador.Find(id);
            if (tipo_Utilizador == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Utilizador);
        }

        // POST: Tipo_Utilizador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Utilizador tipo_Utilizador = db.Tipo_Utilizador.Find(id);
            db.Tipo_Utilizador.Remove(tipo_Utilizador);
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
