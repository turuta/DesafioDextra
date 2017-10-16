using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApiLanches.Models;

namespace ApiLanches.Controllers
{
    public class Lanches1Controller : Controller
    {
        private DataContext db = new DataContext();

        // GET: Lanches1
        public async Task<ActionResult> Index()
        {
            return View(await db.Lanches.ToListAsync());
        }

        // GET: Lanches1/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lanche lanche = await db.Lanches.FindAsync(id);
            if (lanche == null)
            {
                return HttpNotFound();
            }
            return View(lanche);
        }

        // GET: Lanches1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lanches1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdLanche,Nome,ValorTotal,Desconto")] Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                db.Lanches.Add(lanche);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lanche);
        }

        // GET: Lanches1/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lanche lanche = await db.Lanches.FindAsync(id);
            if (lanche == null)
            {
                return HttpNotFound();
            }
            return View(lanche);
        }

        // POST: Lanches1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdLanche,Nome,ValorTotal,Desconto")] Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lanche).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lanche);
        }

        // GET: Lanches1/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lanche lanche = await db.Lanches.FindAsync(id);
            if (lanche == null)
            {
                return HttpNotFound();
            }
            return View(lanche);
        }

        // POST: Lanches1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Lanche lanche = await db.Lanches.FindAsync(id);
            db.Lanches.Remove(lanche);
            await db.SaveChangesAsync();
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
