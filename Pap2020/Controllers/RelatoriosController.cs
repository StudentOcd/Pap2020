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
    public class RelatoriosController : Controller
    {
        private SistemaGestaoEntities db = new SistemaGestaoEntities();

     
        public ActionResult Index()
        {
            if ( Session["Id"] == null)
            {
                return View("Error");
            }
            int id = Convert.ToInt32(Session["Id"]);
            //Fazer com que só apareca a lista com o mesmo id que o utilizador no momento
           int Tipo =Convert.ToInt32( Session["Tipo"]);
           
            switch (Tipo)
            {

                case 1:  
                   var relatorio = db.Relatorio.Where(r => r.id_aluno.Equals(id));
                    return View(relatorio.ToList());
                  
                    
                case 3:   
                  var  relatorio1 = db.Relatorio.Where(r => r.id_monitor.Equals(id));
                    
                    return View(relatorio1.ToList());
                   
                case 2:
                   var  relatorio2 = db.Relatorio.Where(r => r.id_professor.Equals(id));
                    if(relatorio2  == null)
                    {
                        return View("Error");
                    }
                    return View(relatorio2.ToList());

                default:
                    return View("Error");
                    
            }
           
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relatorio relatorio = db.Relatorio.Find(id);
            if (relatorio == null)
            {
                return HttpNotFound();
            }
            return View(relatorio);
        }

        // GET: Relatorios/Create
        public ActionResult Create()
        {
            ViewBag.id_aluno = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador");
            ViewBag.id_monitor = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador");
            ViewBag.id_professor = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador");
            return View();
        }

        // POST: Relatorios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_relatorio,nome_empresa,NIF,email_empresa,id_aluno,id_professor,id_monitor")] Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                db.Relatorio.Add(relatorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_aluno = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador", relatorio.id_aluno);
            ViewBag.id_monitor = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador", relatorio.id_monitor);
            ViewBag.id_professor = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador", relatorio.id_professor);
            return View(relatorio);
        }

        // GET: Relatorios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relatorio relatorio = db.Relatorio.Find(id);
            if (relatorio == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_aluno = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador", relatorio.id_aluno);
            ViewBag.id_monitor = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador", relatorio.id_monitor);
            ViewBag.id_professor = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador", relatorio.id_professor);
            return View(relatorio);
        }

        // POST: Relatorios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_relatorio,nome_empresa,NIF,email_empresa,id_aluno,id_professor,id_monitor")] Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relatorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_aluno = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador", relatorio.id_aluno);
            ViewBag.id_monitor = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador", relatorio.id_monitor);
            ViewBag.id_professor = new SelectList(db.Utilizador, "id_utilizador", "nome_utilizador", relatorio.id_professor);
            return View(relatorio);
        }

        // GET: Relatorios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relatorio relatorio = db.Relatorio.Find(id);
            if (relatorio == null)
            {
                return HttpNotFound();
            }

            if (db.Relatorio.Any(e => e.id_relatorio == id))
            {
                var handleErrorInfo = new HandleErrorInfo(new Exception("Não é possível remover o Relatório dado que existe(m) utilizadores pertencentes ao mesmo!"),"Relatorio","Index");
                return View("Error", handleErrorInfo);
            }
            return View(relatorio);
        }

        // POST: Relatorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Relatorio relatorio = db.Relatorio.Find(id);
            db.Relatorio.Remove(relatorio);
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
