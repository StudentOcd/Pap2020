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
    public class AvaliaçãoController : Controller
    {
        private SistemaGestaoEntities db = new SistemaGestaoEntities();

        // GET: Avaliação
        public ActionResult Index()
        {
            var avaliação = db.Avaliação.Include(a => a.Relatorio);
            return View(avaliação.ToList());
        }

        // GET: Avaliação/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliação avaliação = db.Avaliação.Find(id);
            if (avaliação == null)
            {
                return HttpNotFound();
            }
            return View(avaliação);
        }

        // GET: Avaliação/Create
        public ActionResult Create()
        {
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa");
            return View();
        }

        // POST: Avaliação/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_avaliacao,avaliacaofinal,id_relatorio")] Avaliação avaliação)
        {
            if (ModelState.IsValid)
            {
                db.Avaliação.Add(avaliação);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", avaliação.id_relatorio);
            return View(avaliação);
        }

        // GET: Avaliação/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliação avaliação = db.Avaliação.Find(id);
            if (avaliação == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", avaliação.id_relatorio);
            return View(avaliação);
        }

        // POST: Avaliação/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_avaliacao,avaliacaofinal,id_relatorio")] Avaliação avaliação)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avaliação).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", avaliação.id_relatorio);
            return View(avaliação);
        }

        // GET: Avaliação/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliação avaliação = db.Avaliação.Find(id);
            if (avaliação == null)
            {
                return HttpNotFound();
            }
            return View(avaliação);
        }

        // POST: Avaliação/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Avaliação avaliação = db.Avaliação.Find(id);
            db.Avaliação.Remove(avaliação);
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
