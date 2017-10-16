using ApiLanches.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ApiLanches.RegraNegocio
{
    public class RegrasDeNegocio
    {

        /// <summary>
        /// Método que verifica todas as promoções e retorna um objeto Lanche com valor do desconto e o valor total do lanche com o desconto
        /// </summary>
        /// <param name="objLancheFront"></param>
        /// <returns>Retorna um objeto do tipo lanche com dados de valot total e desconto atualizados</returns>
        public Lanche calculaDesconto(Lanche objLancheFront)
        {
            
            bool light = VerificaLancheLight(objLancheFront.Ingredientes);
            bool muitaCarne = VerificaLancheMuitaCarne(objLancheFront.Ingredientes);
            bool muitoQueijo = VerificaLancheMuitaQueijo(objLancheFront.Ingredientes);
            objLancheFront.ValorTotal = 0;
            
            for (int i = 0; i < objLancheFront.Ingredientes.Count; i++)
            {
                objLancheFront.ValorTotal = objLancheFront.ValorTotal + objLancheFront.Ingredientes[i].SomaTotal;
            }
            double valorTotalSemDesconto = Convert.ToDouble(objLancheFront.ValorTotal);
            if (light)
            {
                objLancheFront.ValorTotal = Convert.ToDouble(calculaLancheLight(Convert.ToDouble(objLancheFront.ValorTotal)));
                objLancheFront.Desconto = valorTotalSemDesconto - objLancheFront.ValorTotal;
            }
            if (muitaCarne)
            {
                objLancheFront.ValorTotal = Convert.ToDouble(calculaLancheMuitaCarne(Convert.ToDouble(objLancheFront.ValorTotal), objLancheFront.Ingredientes));
                objLancheFront.Desconto = valorTotalSemDesconto - objLancheFront.ValorTotal;
            }
            if (muitoQueijo)
            {
                objLancheFront.ValorTotal = Convert.ToDouble(calculaLancheMuitaQueijo(Convert.ToDouble(objLancheFront.ValorTotal), objLancheFront.Ingredientes));
                objLancheFront.Desconto = valorTotalSemDesconto - objLancheFront.ValorTotal;
            }

            return objLancheFront;
        }
        
        /// <summary>
        /// Método que verifica se lanche faz parte da promoção "Lanche Light" - Se lanche possui alface e não tem bacon, ganha 10% de desconto
        /// </summary>
        /// <param name="listIngredientes"></param>
        /// <returns>Retorna um booleano - Se lanche faz parte da promoção ou não</returns>
        public bool VerificaLancheLight(List<Ingrediente> listIngredientes)
        {
            bool light = false;
            int alface = 0;
            int bacon = 0;

            for (int i = 0; i < listIngredientes.Count; i++)
            {
                if (listIngredientes[i].Nome == "Alface" && listIngredientes[i].Qtd > 0)
                {
                    alface++;
                }
                if (listIngredientes[i].Nome == "Bacon" && listIngredientes[i].Qtd > 0)
                {
                    bacon++;
                }
            }
            if (alface > 0 && bacon == 0)
            {
                light = true;
            }

            return light;
        }


        /// <summary>
        /// Método que calcula valor de desconto do lanche caso faça parte da promoção "Lanche Light"
        /// </summary>
        /// <param name="valorTotal"></param>
        /// <returns>Retorna valor do lanche com desconto da promoção</returns>
        public double calculaLancheLight(double valorTotal)
        {
            double valor = 0;

            valor = valorTotal * 0.9;

            return valor;
        }


        /// <summary>
        /// Método que verifica se lanche faz parte da promoção "Muita Carne" - A cada 3 porções de carne o cliente só paga duas.
        /// </summary>
        /// <param name="listIngredientes"></param>
        /// <returns>Retorna um booleano se o lanche faz parte da promoção ou não</returns>
        public bool VerificaLancheMuitaCarne(List<Ingrediente> listIngredientes)
        {
            bool muitaCarne = false;
            

            for (int i = 0; i < listIngredientes.Count; i++)
            {
                if (listIngredientes[i].Nome == "Hamburguer de carne" && listIngredientes[i].Qtd >= 3)
                {
                    muitaCarne = true;
                }

            }


            return muitaCarne;
        }

        
        /// <summary>
        /// Método que calcula valor de desconto do lanche quando fazer parte da promoção "Muita Carne"
        /// </summary>
        /// <param name="valorTotal"></param>
        /// <param name="listIngredientes"></param>
        /// <returns></returns>
        public double calculaLancheMuitaCarne(double valorTotal, List<Ingrediente> listIngredientes)
        {
            double valor = 0;
            int desconto = 0;
            double valorDesconto = 0;
            double valorCarne = 0;

            for (int i = 0; i < listIngredientes.Count; i++)
            {
                if (listIngredientes[i].Nome == "Hamburguer de carne" && listIngredientes[i].Qtd >= 3)
                {

                        valorCarne = listIngredientes[i].Valor;
                        
                        desconto = listIngredientes[i].Qtd /3;
                       
                        valorDesconto =  desconto * valorCarne;
                    
                }


            }

            valor = valorTotal - valorDesconto;


            return Math.Round(valor,2);
        }


        /// <summary>
        /// Método que verifica se o lanche faz parte da promoção "Muito Queijo" - A cada 3 porções de queijo paga somente duas
        /// </summary>
        /// <param name="listIngredientes"></param>
        /// <returns></returns>
        public bool VerificaLancheMuitaQueijo(List<Ingrediente> listIngredientes)
        {
            bool muitoQueijo = false;


            for (int i = 0; i < listIngredientes.Count; i++)
            {
                if (listIngredientes[i].Nome == "Queijo" && listIngredientes[i].Qtd >= 3)
                {
                    muitoQueijo = true;
                }

            }


            return muitoQueijo;
        }


        /// <summary>
        /// Método que calcula valor de desconto do lanche quando fazer parte da promoção "Muito Queijo"
        /// </summary>
        /// <param name="valorTotal"></param>
        /// <param name="listIngredientes"></param>
        /// <returns></returns>
        public double calculaLancheMuitaQueijo(double valorTotal, List<Ingrediente> listIngredientes)
        {
            double valor = 0;
            
            int desconto = 0;
            double valorDesconto = 0;
            double valorQueijo = 0;

            for (int i = 0; i < listIngredientes.Count; i++)
            {
                if (listIngredientes[i].Nome == "Queijo")
                {

                    if (listIngredientes[i].Qtd >= 3)
                    {
                        valorQueijo = listIngredientes[i].Valor;
                        desconto = listIngredientes[i].Qtd / 3;

                        valorDesconto = desconto * valorQueijo;
                    }

                }


            }

            valor = valorTotal - valorDesconto;


            return valor;
        }


        
        public Lanche GetLanche(int id)
        {
           
            string cmdSql = "";
            Lanche objLanche = new Lanche();
            NpgsqlDataReader aReader = null;
            //cria a conexão com o banco de dados
            string tabela = "dbo.#Lanches#";
            string Id = "#IdLanche#";

            NpgsqlConnection aConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Lanches"].ConnectionString);
            try
            {
                cmdSql = @"select * from " + tabela.Replace('#', '"') + " where " + Id.Replace('#', '"') + " = " + id + " ";
                aConnection.Open();
                //cria o objeto command and armazena a consulta SQL
                NpgsqlCommand aCommand = new NpgsqlCommand(cmdSql, aConnection);

                aReader = aCommand.ExecuteReader();

                while (aReader.Read())
                {
                    
                    objLanche.IdLanche = Convert.ToInt32(aReader["IdLanche"]);
                    objLanche.Nome = aReader["Nome"].ToString();

                   
                }
               
            }
            catch (Exception e)
            {

            }
            finally
            {
                aConnection.Close();
            }
            return objLanche;
        }

        public List<Ingrediente> GetIngredientes()
        {
            List<Ingrediente> listIngredientes = new List<Ingrediente>();
            string cmdSql = "";
            Ingrediente objIng; 
            NpgsqlDataReader aReader = null;
            //cria a conexão com o banco de dados
            string tabela = "dbo.#Ingredientes#";
            string Id = "#IdIngrediente#";

            NpgsqlConnection aConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Lanches"].ConnectionString);
            try
            {
                cmdSql = @"select * from " + tabela.Replace('#', '"') + " ";
                aConnection.Open();
                //cria o objeto command and armazena a consulta SQL
                NpgsqlCommand aCommand = new NpgsqlCommand(cmdSql, aConnection);

                aReader = aCommand.ExecuteReader();

                while (aReader.Read())
                {
                    objIng = new Ingrediente();
                    objIng.IdIngrediente = Convert.ToInt32(aReader["IdIngrediente"]);
                    objIng.Nome = aReader["Nome"].ToString();
                    objIng.Valor = Convert.ToDouble(aReader["Valor"]);

                    listIngredientes.Add(objIng);

                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                aConnection.Close();
            }
            return listIngredientes;
        }


    }
}