using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ApiLanches.Models;
using ApiLanches.RegraNegocio;

namespace ApiLanches.Controllers
{
    public class PedidosController : ApiController
    {
        private DataContext db = new DataContext();
        // POST: api/Pedidos
        [ResponseType(typeof(Lanche))]
        public async Task<IHttpActionResult> PostLanche(Lanche lanche)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegrasDeNegocio rg= new RegrasDeNegocio();
            var desconto = rg.calculaDesconto(lanche);


            return CreatedAtRoute("DefaultApi", new { id = desconto.IdLanche }, desconto);
        }
    }
}
