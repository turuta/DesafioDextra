using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiLanches;
using ApiLanches.Controllers;
using ApiLanches.Models;
using ApiLanches.RegraNegocio;

namespace UnitTestLanches
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValorXBacon()
        {
            DataContext db = new DataContext();
            RegrasDeNegocio rn = new RegrasDeNegocio();
            Lanche lanches = new Lanche();
            Ingrediente ig = new Ingrediente();

            var l = new LanchesApiController();
            var lll = l.GetLanche(1);

            ig.SomaTotal = 0;
            double valorXbacon = 6.5;

            var lanche = ApiLanches.GetLanche(1);
            

            for (int i = 0; i < ingredientes.Count; i++)
            {
                ig.SomaTotal = ig.SomaTotal + ingredientes[i].Valor;
            }

            Assert.AreEqual(valorXbacon, ig.SomaTotal);

        }
    }
}
