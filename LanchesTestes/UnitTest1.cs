using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ApiLanches.Models;
using ApiLanches.RegraNegocio;
using System.Collections.Generic;


namespace LanchesTestes
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

         [TestMethod]
        public void TesteValorXBaconErrado()
        {
            DataContext db = new DataContext();
            RegrasDeNegocio rn = new RegrasDeNegocio();
            double valorXBacon = 0;

            double valorErrado = 6.55;
            var lanche = rn.GetLanche(1);
            var ingredientes = rn.GetIngredientes();
            for (int i = 0; i < ingredientes.Count; i++)
            {
                if (ingredientes[i].Nome == "Bacon" || ingredientes[i].Nome == "Hamburguer de carne" || ingredientes[i].Nome == "Queijo")
                {
                    valorXBacon = valorXBacon + ingredientes[i].Valor;
                }

            }

            Assert.AreNotEqual(valorXBacon, valorErrado);
            Assert.IsNotNull(ingredientes);



        }

         [TestMethod]
         public void TesteValorXBurguerErrado()
         {
             DataContext db = new DataContext();
             RegrasDeNegocio rn = new RegrasDeNegocio();
             double valorXBurguer = 0;

             double valorErrado = 6.55;
             var lanche = rn.GetLanche(1);
             var ingredientes = rn.GetIngredientes();
             for (int i = 0; i < ingredientes.Count; i++)
             {
                 if (ingredientes[i].Nome == "Hamburguer de carne" || ingredientes[i].Nome == "Queijo")
                 {
                     valorXBurguer = valorXBurguer + ingredientes[i].Valor;
                 }
             }
             Assert.AreNotEqual(valorXBurguer, valorErrado);
             Assert.IsNotNull(ingredientes);

         }

         [TestMethod]
         public void TesteValorXEggErrado()
         {
             DataContext db = new DataContext();
             RegrasDeNegocio rn = new RegrasDeNegocio();
             double valorXEgg = 0;

             double valorErrado = 6.55;
             var lanche = rn.GetLanche(1);
             var ingredientes = rn.GetIngredientes();
             for (int i = 0; i < ingredientes.Count; i++)
             {
                 if (ingredientes[i].Nome == "Hamburguer de carne" || ingredientes[i].Nome == "Queijo" || ingredientes[i].Nome == "Ovo")
                 {
                     valorXEgg = valorXEgg + ingredientes[i].Valor;
                 }
             }
             Assert.AreNotEqual(valorXEgg, valorErrado);
             Assert.IsNotNull(ingredientes);

         }

         [TestMethod]
         public void TesteValorXEggBaconErrado()
         {
             DataContext db = new DataContext();
             RegrasDeNegocio rn = new RegrasDeNegocio();
             double valorXEggBacon = 0;

             double valorErrado = 6.55;
             var lanche = rn.GetLanche(1);
             var ingredientes = rn.GetIngredientes();
             for (int i = 0; i < ingredientes.Count; i++)
             {
                 if (ingredientes[i].Nome == "Hamburguer de carne" || ingredientes[i].Nome == "Queijo" || ingredientes[i].Nome == "Ovo" || ingredientes[i].Nome == "Bacon")
                 {
                     valorXEggBacon = valorXEggBacon + ingredientes[i].Valor;
                 }
             }
             Assert.AreNotEqual(valorXEggBacon, valorErrado);
             Assert.IsNotNull(ingredientes);

         }

         [TestMethod]
         public void PromocaoLightVerdade()
         {
             RegrasDeNegocio rn = new RegrasDeNegocio();
             Ingrediente ing = new Ingrediente();
             List<Ingrediente> listaIngredientes = new List<Ingrediente>();

             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 1, Nome = "Alface", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 0 });
             
             bool promLight = rn.VerificaLancheLight(listaIngredientes);

             Assert.IsTrue(promLight);

             
         }

         [TestMethod]
         public void PromocaoLightFalso()
         {
             RegrasDeNegocio rn = new RegrasDeNegocio();
             Ingrediente ing = new Ingrediente();
             List<Ingrediente> listaIngredientes = new List<Ingrediente>();

             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 1, Nome = "Alface", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 1 });

             bool promLight = rn.VerificaLancheLight(listaIngredientes);

             Assert.IsFalse(promLight);


         }

         [TestMethod]
         public void PromocaoLightDesconto()
         {
             RegrasDeNegocio rn = new RegrasDeNegocio();
             Ingrediente ing = new Ingrediente();
             List<Ingrediente> listaIngredientes = new List<Ingrediente>();
             double valorLanche = 0;
             double valorLancheComDesconto = 0;

             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 1, Nome = "Alface", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 0 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 5, Nome = "Queijo", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 3, Nome = "Hamburguer de carne", Qtd = 1 });

             bool promLight = rn.VerificaLancheLight(listaIngredientes);

             if (promLight)
             {
                 var listaTodosIngredientes = rn.GetIngredientes();
                 for (int i = 0; i < listaTodosIngredientes.Count; i++)
                 {
                     for(int k=0;k<listaIngredientes.Count; k++ )
                     {
                         if (listaTodosIngredientes[i].Nome == listaIngredientes[k].Nome)
                         {
                             valorLanche = valorLanche + listaTodosIngredientes[i].Valor;
                         }
                     }
                     
                 }
                 valorLancheComDesconto = rn.calculaLancheLight(valorLanche);


             }

             Assert.AreNotEqual(valorLancheComDesconto, valorLanche);
             Assert.AreEqual(valorLancheComDesconto, (valorLanche * 0.9));

         }

        [TestMethod]
         public void PromocaoMuitaCarneVerdade()
         {
             RegrasDeNegocio rn = new RegrasDeNegocio();
             
             List<Ingrediente> listaIngredientes = new List<Ingrediente>();

             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 3, Nome = "Hamburguer de carne", Qtd = 3 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 1 });

             bool promMuitacarne = rn.VerificaLancheMuitaCarne(listaIngredientes);

             Assert.IsTrue(promMuitacarne);

         }

        [TestMethod]
        public void PromocaoMuitaCarneFalso()
        {
            RegrasDeNegocio rn = new RegrasDeNegocio();

            List<Ingrediente> listaIngredientes = new List<Ingrediente>();

            listaIngredientes.Add(new Ingrediente() { IdIngrediente = 3, Nome = "Hamburguer de carne", Qtd = 2 });
            listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 1 });

            bool promMuitacarne = rn.VerificaLancheMuitaCarne(listaIngredientes);

            Assert.IsFalse(promMuitacarne);

        }

         [TestMethod]
        // Teste com 3 carnes
        public void PromocaoMuitaCarneDescontoSim1()
        {

            RegrasDeNegocio rn = new RegrasDeNegocio();
            Ingrediente ing = new Ingrediente();
            List<Ingrediente> listaIngredientes = new List<Ingrediente>();
            double valorLanche = 0;
            double valorLancheComDesconto = 0;
            double valorCarne = 0;
            double valorDescontoEsperado = 0;

            listaIngredientes.Add(new Ingrediente() { IdIngrediente = 1, Nome = "Alface", Qtd = 1 });
            listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 0 });
            listaIngredientes.Add(new Ingrediente() { IdIngrediente = 5, Nome = "Queijo", Qtd = 1 });
            listaIngredientes.Add(new Ingrediente() { IdIngrediente = 3, Nome = "Hamburguer de carne", Qtd = 3 });

            bool promMuitaCarne = rn.VerificaLancheMuitaCarne(listaIngredientes);

            if (promMuitaCarne)
            {
                var listaTodosIngredientes = rn.GetIngredientes();
                 for (int i = 0; i < listaTodosIngredientes.Count; i++)
                 {
                    
                     for(int k=0;k<listaIngredientes.Count; k++ )
                     {
                         if (listaTodosIngredientes[i].Nome == listaIngredientes[k].Nome)
                         {
                             valorLanche = valorLanche + listaTodosIngredientes[i].Valor;
                             listaIngredientes[k].Valor = listaTodosIngredientes[i].Valor;

                             if (listaTodosIngredientes[i].Nome == "Hamburguer de carne")
                             {
                                 valorCarne = listaTodosIngredientes[i].Valor;
                                 
                                 
                             }

                         }
                     }
                     
                 }
                 valorLancheComDesconto = rn.calculaLancheMuitaCarne(valorLanche, listaIngredientes);
                 valorDescontoEsperado = valorCarne;
            }

            Assert.IsTrue(promMuitaCarne);
            Assert.AreNotEqual(valorLanche, valorLancheComDesconto);
            Assert.AreEqual(valorLancheComDesconto, Math.Round((valorLanche-valorDescontoEsperado),2));
        }

         [TestMethod] 
        // Teste com 6 carnes
         public void PromocaoMuitaCarneDescontoSim2()
         {

             RegrasDeNegocio rn = new RegrasDeNegocio();
             Ingrediente ing = new Ingrediente();
             List<Ingrediente> listaIngredientes = new List<Ingrediente>();
             double valorLanche = 0;
             double valorLancheComDesconto = 0;
             double valorCarne = 0;
             double valorDescontoEsperado = 0;

             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 1, Nome = "Alface", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 0 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 5, Nome = "Queijo", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 3, Nome = "Hamburguer de carne", Qtd = 6 }); // Altere quantidade de carnes para testes

             bool promMuitaCarne = rn.VerificaLancheMuitaCarne(listaIngredientes);

             if (promMuitaCarne)
             {
                 var listaTodosIngredientes = rn.GetIngredientes();
                 for (int i = 0; i < listaTodosIngredientes.Count; i++)
                 {

                     for (int k = 0; k < listaIngredientes.Count; k++)
                     {
                         if (listaTodosIngredientes[i].Nome == listaIngredientes[k].Nome)
                         {
                             valorLanche = valorLanche + (listaTodosIngredientes[i].Valor * listaIngredientes[k].Qtd);
                             listaIngredientes[k].Valor = listaTodosIngredientes[i].Valor;


                             if (listaTodosIngredientes[i].Nome == "Hamburguer de carne")
                             {
                                 valorCarne = listaTodosIngredientes[i].Valor;
                                 valorDescontoEsperado = valorCarne * (listaIngredientes[k].Qtd / 3);

                             }
                             

                         }
                     }

                 }
                 valorLancheComDesconto = rn.calculaLancheMuitaCarne(valorLanche, listaIngredientes);
                 
             }

             Assert.IsTrue(promMuitaCarne);
             Assert.AreNotEqual(valorLanche, valorLancheComDesconto);
             Assert.AreEqual(valorLancheComDesconto, Math.Round((valorLanche - valorDescontoEsperado), 2));
         }


    /* ----------------------------------------------------------------------------------------------------------*/


         [TestMethod]
         public void PromocaoMuitoQueijoVerdade()
         {
             RegrasDeNegocio rn = new RegrasDeNegocio();

             List<Ingrediente> listaIngredientes = new List<Ingrediente>();

             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 3, Nome = "Hamburguer de carne", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 5, Nome = "Queijo", Qtd = 3 });

             bool promMuitoQueijo = rn.VerificaLancheMuitaQueijo(listaIngredientes);

             Assert.IsTrue(promMuitoQueijo);

         }

         [TestMethod]
         public void PromocaoMuitoQueijoFalso()
         {
             RegrasDeNegocio rn = new RegrasDeNegocio();

             List<Ingrediente> listaIngredientes = new List<Ingrediente>();

             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 3, Nome = "Hamburguer de carne", Qtd = 2 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 5, Nome = "Queijo", Qtd = 2 });

             bool promMuitoQueijo= rn.VerificaLancheMuitaQueijo(listaIngredientes);

             Assert.IsFalse(promMuitoQueijo);

         }

         [TestMethod]
         // Teste com 3 queijos
         public void PromocaoMuitoQueijoDescontoSim1()
         {

             RegrasDeNegocio rn = new RegrasDeNegocio();
             Ingrediente ing = new Ingrediente();
             List<Ingrediente> listaIngredientes = new List<Ingrediente>();
             double valorLanche = 0;
             double valorLancheComDesconto = 0;
             double valorQueijo = 0;
             double valorDescontoEsperado = 0;

             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 1, Nome = "Alface", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 0 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 5, Nome = "Queijo", Qtd = 3 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 3, Nome = "Hamburguer de carne", Qtd = 1 });

             bool promMuitoQueijo = rn.VerificaLancheMuitaQueijo(listaIngredientes);

             if (promMuitoQueijo)
             {
                 var listaTodosIngredientes = rn.GetIngredientes();
                 for (int i = 0; i < listaTodosIngredientes.Count; i++)
                 {

                     for (int k = 0; k < listaIngredientes.Count; k++)
                     {
                         if (listaTodosIngredientes[i].Nome == listaIngredientes[k].Nome)
                         {
                             valorLanche = valorLanche + listaTodosIngredientes[i].Valor;
                             listaIngredientes[k].Valor = listaTodosIngredientes[i].Valor;

                             if (listaTodosIngredientes[i].Nome == "Queijo")
                             {
                                 valorQueijo = listaTodosIngredientes[i].Valor;


                             }

                         }
                     }

                 }
                 valorLancheComDesconto = rn.calculaLancheMuitaQueijo(valorLanche, listaIngredientes);
                 valorDescontoEsperado = valorQueijo;
             }

             Assert.IsTrue(promMuitoQueijo);
             Assert.AreNotEqual(valorLanche, valorLancheComDesconto);
             Assert.AreEqual(valorLancheComDesconto, Math.Round((valorLanche - valorDescontoEsperado), 2));
         }

         [TestMethod]
         // Teste com 6 queijos
         public void PromocaoMuitoQueijoDescontoSim2()
         {

             RegrasDeNegocio rn = new RegrasDeNegocio();
             Ingrediente ing = new Ingrediente();
             List<Ingrediente> listaIngredientes = new List<Ingrediente>();
             double valorLanche = 0;
             double valorLancheComDesconto = 0;
             double valorQueijo = 0;
             double valorDescontoEsperado = 0;

             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 1, Nome = "Alface", Qtd = 1 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 2, Nome = "Bacon", Qtd = 0 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 5, Nome = "Queijo", Qtd = 6 });
             listaIngredientes.Add(new Ingrediente() { IdIngrediente = 3, Nome = "Hamburguer de carne", Qtd = 1 });

             bool promMuitoQueijo = rn.VerificaLancheMuitaQueijo(listaIngredientes);

             if (promMuitoQueijo)
             {
                 var listaTodosIngredientes = rn.GetIngredientes();
                 for (int i = 0; i < listaTodosIngredientes.Count; i++)
                 {

                     for (int k = 0; k < listaIngredientes.Count; k++)
                     {
                         if (listaTodosIngredientes[i].Nome == listaIngredientes[k].Nome)
                         {
                             valorLanche = valorLanche + (listaTodosIngredientes[i].Valor * listaIngredientes[k].Qtd);
                             listaIngredientes[k].Valor = listaTodosIngredientes[i].Valor;


                             if (listaTodosIngredientes[i].Nome == "Queijo")
                             {
                                 valorQueijo = listaTodosIngredientes[i].Valor;
                                 valorDescontoEsperado = valorQueijo * (listaIngredientes[k].Qtd / 3);

                             }


                         }
                     }

                 }
                 valorLancheComDesconto = rn.calculaLancheMuitaQueijo(valorLanche, listaIngredientes);

             }

             Assert.IsTrue(promMuitoQueijo);
             Assert.AreNotEqual(valorLanche, valorLancheComDesconto);
             Assert.AreEqual(valorLancheComDesconto, Math.Round((valorLanche - valorDescontoEsperado), 2));
         }
    }
}
