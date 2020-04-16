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
    public class DiasController : Controller
    {
        private SistemaGestaoEntities db = new SistemaGestaoEntities();

        // GET: Dias
        public ActionResult Index()
        {
            var dia = db.Dia.Include(d => d.Relatorio);
            return View(dia.ToList());
        }

        // GET: Dias/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dia dia = db.Dia.Find(id);
            if (dia == null)
            {
                return HttpNotFound();
            }
            return View(dia);
        }

        // GET: Dias/Create
        public ActionResult Create()
        {
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa");
            return View();
        }

        // POST: Dias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "data_hora,conteudo,id_relatorio")] Dia dia)
        {
            if (ModelState.IsValid)
            {
                db.Dia.Add(dia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", dia.id_relatorio);
            return View(dia);
        }

        // GET: Dias/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dia dia = db.Dia.Find(id);
            if (dia == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", dia.id_relatorio);
            return View(dia);
        }

        // POST: Dias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "data_hora,conteudo,id_relatorio")] Dia dia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", dia.id_relatorio);
            return View(dia);
        }

        // GET: Dias/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dia dia = db.Dia.Find(id);
            if (dia == null)
            {
                return HttpNotFound();
            }
            return View(dia);
        }

        // POST: Dias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            Dia dia = db.Dia.Find(id);
            db.Dia.Remove(dia);
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
