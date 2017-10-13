using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiLanches;
using ApiLanches.Controllers;
using ApiLanches.Models;
using ApiLanches.RegraNegocio;

namespace ApiLanches.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }

         [TestMethod]
        public void ValorXBacon()
        {
            DataContext db = new DataContext();
            RegrasDeNegocio rn = new RegrasDeNegocio();
            Lanche lanches = new Lanche();
            Ingrediente ig = new Ingrediente();
            ig.SomaTotal = 0;
            double valorXbacon = 6.5;

            var lanche = db.Lanches.Find(1);
            var ingredientes = lanche.Ingredientes;

            for (int i = 0; i < ingredientes.Count; i++)
            {
                ig.SomaTotal = ig.SomaTotal + ingredientes[i].Valor;
            }

            Assert.AreEqual(valorXbacon, ig.SomaTotal);
            
        }
    }
}
