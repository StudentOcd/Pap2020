using Pap2020.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Pap2020.Controllers
{
    public class DiasController : Controller
    {
        private SistemaGestaoEntities db = new SistemaGestaoEntities();

        // GET: Dias
        [Authorize]
        public ActionResult Index(int? id)
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
                    string query = "Select Dia.data_hora,Dia.conteudo,Dia.id_relatorio from Dia,Utilizador, Relatorio where Utilizador.id_utilizador = Relatorio.id_professor and Relatorio.id_relatorio = Dia.id_relatorio and Utilizador.id_utilizador =" + id_utilizador;


                    var falta = db.Database.SqlQuery<Dia>(query).ToList();
                    return View(falta.ToList());

                case 2:
                    string query1 = "Select Dia.data_hora,Dia.conteudo,Dia.id_relatorio from Dia,Utilizador, Relatorio where Utilizador.id_utilizador = Relatorio.id_monitor and Relatorio.id_relatorio = Dia.id_relatorio and Utilizador.id_utilizador =" + id_utilizador;


                    var falta1 = db.Database.SqlQuery<Dia>(query1).ToList();
                    return View(falta1.ToList());



                case 3:
                    string query2 = "Select Dia.data_hora,Dia.conteudo,Dia.id_relatorio from Dia,Utilizador, Relatorio where Utilizador.id_utilizador = Relatorio.id_aluno and Relatorio.id_relatorio = Dia.id_relatorio and Utilizador.id_utilizador =" + id_utilizador;


                    var falta2 = db.Database.SqlQuery<Dia>(query2).ToList();
                    return View(falta2.ToList());

                default:
                    return View("Error");

            }

        

    }


        // GET: Dias/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.id_relatorio = new SelectList(db.Relatorio, "id_relatorio", "nome_empresa");
            return View();
        }

        // POST: Dias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
