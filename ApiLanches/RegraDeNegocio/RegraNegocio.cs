using ApiLanches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiLanches.RegraDeNegocio
{
    public class RegraNegocio
    {
        public bool VerificaLancheLight(List<Ingrediente> listIngredientes)
        {
            bool light = false;
            int alface = 0;
            int bacon = 0;

            for (int i = 0; i < listIngredientes.Count; i++)
            {
                if (listIngredientes[i].Nome == "Alface")
                {
                    alface++;
                }
                if (listIngredientes[i].Nome == "Bacon")
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

        public double calculaLancheLight(double valorTotal)
        {
            double valor = 0;

            valor = valorTotal * 0.9;

            return valor;
        }

        public bool VerificaLancheMuitaCarne(List<Ingrediente> listIngredientes)
        {
            bool muitaCarne = false;
            int carne = 0;

            for (int i = 0; i < listIngredientes.Count; i++)
            {
                if (listIngredientes[i].Nome == "Hamburguer de carne" && listIngredientes[i].Qtd >= 3)
                {
                    muitaCarne = true;
                }

            }


            return muitaCarne;
        }

        public double calculaLancheMuitaCarne(double valorTotal, List<Ingrediente> listIngredientes)
        {
            double valor = 0;
            int carne = 0;
            int desconto = 0;
            double valorDesconto = 0;
            double valorCarne = 0;

            for (int i = 0; i < listIngredientes.Count; i++)
            {
                if (listIngredientes[i].Nome == "Hamburguer de carne")
                {

                    if (listIngredientes[i].Qtd >= 3)
                    {
                        valorCarne = listIngredientes[i].Valor;
                        desconto = listIngredientes[i].Qtd % 3;
                        valorDesconto = desconto * valorCarne;
                    }

                }


            }

            valor = valorTotal - valorDesconto;


            return valor;
        }


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

        public double calculaLancheMuitaQueijo(double valorTotal, List<Ingrediente> listIngredientes)
        {
            double valor = 0;
            int carne = 0;
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
                        desconto = listIngredientes[i].Qtd % 3;
                        valorDesconto = desconto * valorQueijo;
                    }

                }


            }

            valor = valorTotal - valorDesconto;


            return valor;
        }

    }
}