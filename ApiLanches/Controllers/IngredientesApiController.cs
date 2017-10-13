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
    public class IngredientesApiController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/IngredientesApi
        public IQueryable<Ingrediente> GetIngredientes()
        {
            return db.Ingredientes;
        }

        // GET: api/IngredientesApi/5
        [ResponseType(typeof(Ingrediente))]
        public async Task<IHttpActionResult> GetIngrediente(long id)
        {
            Ingrediente ingrediente = await db.Ingredientes.FindAsync(id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return Ok(ingrediente);
        }

        // PUT: api/IngredientesApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIngrediente(long id, Ingrediente ingrediente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingrediente.IdIngrediente)
            {
                return BadRequest();
            }

            db.Entry(ingrediente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredienteExists(id))
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

        // POST: api/IngredientesApi
        [ResponseType(typeof(Ingrediente))]
        public async Task<IHttpActionResult> PostIngrediente(Ingrediente ingrediente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ingredientes.Add(ingrediente);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ingrediente.IdIngrediente }, ingrediente);
        }

        // DELETE: api/IngredientesApi/5
        [ResponseType(typeof(Ingrediente))]
        public async Task<IHttpActionResult> DeleteIngrediente(long id)
        {
            Ingrediente ingrediente = await db.Ingredientes.FindAsync(id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            db.Ingredientes.Remove(ingrediente);
            await db.SaveChangesAsync();

            return Ok(ingrediente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IngredienteExists(long id)
        {
            return db.Ingredientes.Count(e => e.IdIngrediente == id) > 0;
        }
    }
}