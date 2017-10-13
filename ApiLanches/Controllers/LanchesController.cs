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
    public class LanchesController : Controller
    {
        private DataContext db = new DataContext();
       



        // GET: Lanches
        public async Task<ActionResult> Index()
        {
            ViewBag.Ingredientes = db.Ingredientes;
            var ingredientes = db.Ingredientes;
            var lanche1 = db.Lanches;
            List<Ingrediente> list;
            List<Ingrediente> list2;
            List<Ingrediente> list3;
            List<Ingrediente> list4;
            foreach (var lanche in lanche1)
            {
                switch (lanche.IdLanche)
                {
                    
                    case 1:
                        list = new List<Ingrediente>();
                        list.Add(ingredientes.Find(1));
                        list.Add(ingredientes.Find(2));
                        

                        lanche.Ingredientes.AddRange(list);

                        continue;
                    case 2:
                        list2 = new List<Ingrediente>();
                        list2.Add(ingredientes.Find(1));
                        list2.Add(ingredientes.Find(2));

                        lanche.Ingredientes.AddRange(list2);
                        continue;
                    case 3:
                        list3 = new List<Ingrediente>();
                        list3.Add(ingredientes.Find(2));
                        list3.Add(ingredientes.Find(3));
                        list3.Add(ingredientes.Find(4));

                        lanche.Ingredientes.AddRange(list3);
                        continue;
                    case 4:
                        list4 = new List<Ingrediente>();
                        list4.Add(ingredientes.Find(1));
                        list4.Add(ingredientes.Find(2));

                        lanche.Ingredientes.AddRange(list4);
                        continue;
                    default:
                        break;
                }
            }
           

            return View(lanche1);
        }

        // GET: Lanches/Details/5
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

        // GET: Lanches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lanches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdLanche,Nome")] Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                db.Lanches.Add(lanche);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lanche);
        }

        // GET: Lanches/Edit/5
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

        // POST: Lanches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdLanche,Nome")] Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lanche).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lanche);
        }

        // GET: Lanches/Delete/5
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

        // POST: Lanches/Delete/5
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
