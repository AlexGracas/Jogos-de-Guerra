using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JogosDeGuerraModel;

namespace JogosDeGuerraWebAPI.Controllers
{
    public class BatalhasMVCController : Controller
    {
        private ModelJogosDeGuerra db = new ModelJogosDeGuerra();

        // GET: BatalhasMVC
        public ActionResult Index()
        {
            var batalhas = db.Batalhas
                .Include(b => b.ExercitoBranco)
                .Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoBranco.Usuario)
                .Include(b => b.ExercitoPreto.Usuario)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Turno)
                .Include(b => b.Vencedor);
            return View(batalhas.ToList());
        }

        // GET: BatalhasMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batalha batalha = db.Batalhas.Find(id);
            if (batalha == null)
            {
                return HttpNotFound();
            }
            return View(batalha);
        }

        // GET: BatalhasMVC/Create
        public ActionResult Create()
        {
            ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id");
            ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id");
            ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id");
            ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id");
            ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id");
            return View();
        }

        // POST: BatalhasMVC/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TabuleiroId,ExercitoBrancoId,ExercitoPretoId,VencedorId,TurnoId,Estado")] Batalha batalha)
        {
            if (ModelState.IsValid)
            {
                db.Batalhas.Add(batalha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoBrancoId);
            ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoPretoId);
            ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id", batalha.TabuleiroId);
            ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id", batalha.TurnoId);
            ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id", batalha.VencedorId);
            return View(batalha);
        }

        // GET: BatalhasMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batalha batalha = db.Batalhas.Find(id);
            if (batalha == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoBrancoId);
            ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoPretoId);
            ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id", batalha.TabuleiroId);
            ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id", batalha.TurnoId);
            ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id", batalha.VencedorId);
            return View(batalha);
        }

        // POST: BatalhasMVC/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TabuleiroId,ExercitoBrancoId,ExercitoPretoId,VencedorId,TurnoId,Estado")] Batalha batalha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batalha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoBrancoId);
            ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoPretoId);
            ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id", batalha.TabuleiroId);
            ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id", batalha.TurnoId);
            ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id", batalha.VencedorId);
            return View(batalha);
        }

        [Route("Tabuleiro/{id}")]
        public ActionResult Tabuleiro(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Batalha batalha = db.Batalhas.Find(id);
            if (batalha == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = batalha.Id;
            return View(batalha);
        }

        // GET: BatalhasMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batalha batalha = db.Batalhas.Find(id);
            if (batalha == null)
            {
                return HttpNotFound();
            }
            return View(batalha);
        }

        // POST: BatalhasMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Batalha batalha = db.Batalhas.Find(id);
            db.Batalhas.Remove(batalha);
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
