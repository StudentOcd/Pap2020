using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pap2020.Models;

namespace Pap2020.Controllers
{
    public class FaltasController : Controller
    {
        private SistemaGestaoEntities db = new SistemaGestaoEntities();

        // GET: Faltas
      
       [HttpGet]
        public ActionResult Index(int idreq)
        {
            if (Session["Id"] == null)
            {
                return View("Error");
            }

            // Recebe o id do relaório e compara o com o id do relatório que contem as faltas
            var falta = db.Falta.Where(r => r.id_relatorio == idreq);
            return View(falta.ToList());
        }
    


    

        // GET: Faltas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Falta falta = db.Falta.Find(id);
            if (falta == null)
            {
                return HttpNotFound();
            }
            return View(falta);
        }

        // GET: Faltas/Create
        public ActionResult Create()
        {
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa");
            return View();
        }

        // POST: Faltas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_falta,Data,id_relatorio")] Falta falta)
        {
            if (ModelState.IsValid)
            {
                db.Falta.Add(falta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", falta.id_relatorio);
            return View(falta);
        }

        // GET: Faltas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Falta falta = db.Falta.Find(id);
            if (falta == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", falta.id_relatorio);
            return View(falta);
        }

        // POST: Faltas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_falta,Data_Hora_Inicio,Data_Hora_Fim,id_relatorio")] Falta falta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(falta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa", falta.id_relatorio);
            return View(falta);
        }

        // GET: Faltas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Falta falta = db.Falta.Find(id);
            if (falta == null)
            {
                return HttpNotFound();
            }
            return View(falta);
        }

        // POST: Faltas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Falta falta = db.Falta.Find(id);
            db.Falta.Remove(falta);
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
