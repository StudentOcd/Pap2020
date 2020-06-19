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
    public class BloqueiosController : Controller
    {
        private SistemaGestaoEntities db = new SistemaGestaoEntities();

        // GET: Bloqueios
        public ActionResult Index()
        {
            var bloqueio = db.Bloqueio.Include(b => b.Relatorio);
            return View(bloqueio.ToList());
        }

        // GET: Bloqueios/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloqueio bloqueio = db.Bloqueio.Find(id);
            if (bloqueio == null)
            {
                return HttpNotFound();
            }
            return View(bloqueio);
        }

        // GET: Bloqueios/Create
        public ActionResult Create()
        {
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa");
            return View();
        }

        // POST: Bloqueios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dia,is_locked,id_relatorio")] Bloqueio bloqueio)
        {
            if (ModelState.IsValid)
            {
                db.Bloqueio.Add(bloqueio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", bloqueio.id_relatorio);
            return View(bloqueio);
        }

        // GET: Bloqueios/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloqueio bloqueio = db.Bloqueio.Find(id);
            if (bloqueio == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", bloqueio.id_relatorio);
            return View(bloqueio);
        }

        // POST: Bloqueios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dia,is_locked,id_relatorio")] Bloqueio bloqueio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloqueio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", bloqueio.id_relatorio);
            return View(bloqueio);
        }

        // GET: Bloqueios/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloqueio bloqueio = db.Bloqueio.Find(id);
            if (bloqueio == null)
            {
                return HttpNotFound();
            }
            return View(bloqueio);
        }

        // POST: Bloqueios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            Bloqueio bloqueio = db.Bloqueio.Find(id);
            db.Bloqueio.Remove(bloqueio);
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
