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
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return View("Error");
            }
            int id_utilizador = Convert.ToInt32(Session["Id"]);
            //Fazer com que só apareca a lista com o mesmo id que o utilizador no momento
            int Tipo = Convert.ToInt32(Session["Tipo"]);

            switch (Tipo)
            {
                case 1:
                    string query = "Select Falta.id_falta,Falta.Data,Falta.id_relatorio from Falta,Utilizador, Relatorio where" +
                "Utilizador.id_utilizador = Relatorio.id_aluno and Relatorio.id_relatorio = Falta.id_relatorio and Utilizador.id_utilizador = " + id_utilizador;


                    var falta = db.Database.SqlQuery<Falta>(query).ToList();
                        return View(falta.ToList());

                case 3:
                    string query1 = "Select Falta.id_falta,Falta.Data,Falta.id_relatorio from Falta,Utilizador, Relatorio where" +
                  "Utilizador.id_utilizador = Relatorio.id_monitor and Relatorio.id_relatorio = Falta.id_relatorio and Utilizador.id_utilizador = " + id_utilizador;


                    var falta1 = db.Database.SqlQuery<Falta>(query1).ToList();
                    return View(falta1.ToList());


                    
                case 2:
                    string query2 = "Select Falta.id_falta,Falta.Data,Falta.id_relatorio from Falta,Utilizador, Relatorio where" +
                "Utilizador.id_utilizador = Relatorio.id_professor and Relatorio.id_relatorio = Falta.id_relatorio and Utilizador.id_utilizador = " + id_utilizador;


                    var falta2 = db.Database.SqlQuery<Falta>(query2).ToList();
                    return View(falta2.ToList());

                default:
                    return View("Error");

            }

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
