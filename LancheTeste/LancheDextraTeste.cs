using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiLanches.Models;
using ApiLanches.RegraNegocio;
using ApiLanches.Controllers;
using System.Web;

namespace LancheTeste
{
    [TestClass]
    public class LancheDextraTeste
    {
        [TestMethod]
        public void TestValorXBacon()
        {
            RegrasDeNegocio rg = new RegrasDeNegocio();
            Lanche objLanche = new Lanche();
            Ingrediente ing = new Ingrediente();
            DataContext db = new DataContext();

            var lanche = db.Lanches;
            
            var ingredientes = db.Ingredientes;

            var xbacon = lanche.Find(1);
            
            var alface = ingredientes.Find(1);

            //var bacon = ing.GetIngrediente(2);
           // var hamburguer = ing.GetIngrediente(3);
           // var ovo = ing.GetIngrediente(4);
          //  var queijo = ing.GetIngrediente(5);
           


            var valor = rg.calculaDesconto(objLanche);
        }
    }
}
