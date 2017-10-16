using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ApiLanches.Models;

namespace ApiLanches.Controllers
{
    public class LanchesApiController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/LanchesApi
        public IQueryable<Lanche> GetLanches()
        {
            return db.Lanches;
        }



        // GET: api/LanchesApi/5
        [ResponseType(typeof(Lanche))]
        public async Task<IHttpActionResult> GetLanche(long id)
        {
            Lanche lanche = await db.Lanches.FindAsync(id);
            if (lanche == null)
            {
                return NotFound();
            }
            
            var ing1 = db.Ingredientes.Find(1);
            var ing2 =  db.Ingredientes.Find(2);
            var ing3 = db.Ingredientes.Find(3);
            var ing4 = db.Ingredientes.Find(4);
            var ing5 = db.Ingredientes.Find(5);

            switch (id)
            {
                case 1:
                    lanche.Ingredientes.Add(ing2);
                    lanche.Ingredientes.Add(ing3);
                    lanche.Ingredientes.Add(ing5);
                    break;
                case 2:
                    lanche.Ingredientes.Add(ing3);
                    lanche.Ingredientes.Add(ing5);
                    break;
                case 3:
                    lanche.Ingredientes.Add(ing4);
                    lanche.Ingredientes.Add(ing3);
                    lanche.Ingredientes.Add(ing5);
                    break;
                case 4:
                    lanche.Ingredientes.Add(ing4);
                    lanche.Ingredientes.Add(ing2);
                    lanche.Ingredientes.Add(ing3);
                    lanche.Ingredientes.Add(ing5);
                    break;
            }


            return Ok(lanche);
        }

        // PUT: api/LanchesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLanche(long id, Lanche lanche)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lanche.IdLanche)
            {
                return BadRequest();
            }

            db.Entry(lanche).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LancheExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/LanchesApi
        [ResponseType(typeof(Lanche))]
        public async Task<IHttpActionResult> PostLanche(Lanche lanche)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lanches.Add(lanche);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lanche.IdLanche }, lanche);
        }

        // DELETE: api/LanchesApi/5
        [ResponseType(typeof(Lanche))]
        public async Task<IHttpActionResult> DeleteLanche(long id)
        {
            Lanche lanche = await db.Lanches.FindAsync(id);
            if (lanche == null)
            {
                return NotFound();
            }

            db.Lanches.Remove(lanche);
            await db.SaveChangesAsync();

            return Ok(lanche);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LancheExists(long id)
        {
            return db.Lanches.Count(e => e.IdLanche == id) > 0;
        }
    }
}