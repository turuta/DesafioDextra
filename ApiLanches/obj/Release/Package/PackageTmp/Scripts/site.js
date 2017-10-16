var app = window.app || {}


app.ingrediente = (function($) {
    'use strict';
    var lanche = {
            "IdLanche": null,
            "Nome": null,
            "ValorTotal": null,
            "Desconto": null,
            "Ingredientes": [
                {
                    "IdIngrediente": null,
                    "Nome": null,
                    "Valor": null,
                    "Qtd": null
                }
            ]
    },
        ingredientes ={},
        me = {
            lancheEscolhido: lancheEscolhido,
            adicionarIngrediente: adicionarIngrediente,
            removerIngrediente: removerIngrediente,
            enviarPedido: enviarPedido
        }


    function somaValorLanche() {
        console.log(lanche);
        var ret = 0;
        $.each(lanche.Ingredientes,
            function(k, v) {
                ret = ret + v.SomaTotal;
            });
        return Math.floor(ret,2);
    }

    function mostrarPedido() {
        var mostrarlancheEscolhido = $('#mostrarlancheEscolhido');
        var ret = '';
        ret += '<h1>' + lanche.Nome + '</h1>';
        $.each(lanche.Ingredientes,
            function(k, v) {
                if (v.Qtd > 0) {
                    ret += '<p><button class="btn btn-primary">' +
                        v.Nome.toUpperCase() +
                        '</button><button class="btn btn-success">' +
                        v.Qtd +
                        '</button></p>';
                }
            });
        console.log(lanche);
        if (lanche.ValorTotal == null || lanche.ValorTotal == 0) {
            ret += '<h3>Valor total do lanche: R$ ' +  somaValorLanche().toFixed(2) + '<h3>';

        }
        else {
            ret += '<h3>Valor total do lanche: R$ ' + lanche.ValorTotal.toFixed(2) + '<h3>';
        }
        
        if (somaValorLanche() > 0) {
            ret += '<button class="btn btn-success" onclick="app.ingrediente.enviarPedido()">Enviar pedido</button>';
        }


        lanche.Desconto === null ? ret += "" : ret += '<small>   Desconto da promoção: R$ ' + lanche.Desconto + '</small>';
        
        mostrarlancheEscolhido.html('').html(ret);

    }

    function mostrarlanche(dados) {
        console.log(dados);
        lanche.IdLanche = dados.IdLanche;
        lanche.Nome = dados.Nome;
        lanche.ValorTotal = dados.ValorTotal;
        lanche.Ingredientes = dados.Ingredientes;
        lanche.Desconto = dados.Desconto;

        var listaDeLanches = $('#listaDeLanches');
   
        mostrarPedido();
        listarIngredientes();
        listaDeLanches.hide();
    }

    function comboIngredientes(dados) {
        ingredientes = dados;
        var divcomboIngredientes = $('#comboIngredientes');
        divcomboIngredientes.hide().html('');
        var ret = "<h3>Adiocione ou remova ingredientes</h3>";
        $.each(dados,
            function(k, v) {
                ret += '<p><button class="btn btn-success" onclick="app.ingrediente.adicionarIngrediente(' +
                    v.IdIngrediente +
                    ')">+</button><button class="btn btn-primary">' +
                    v.Nome.toUpperCase() +
                    '</button><button class="btn btn-danger" onclick="app.ingrediente.removerIngrediente(' +
                    v.IdIngrediente +
                    ')">-</button></p>';
            });
        
        divcomboIngredientes.html(ret).show();
    }

    function listarIngredientes() {

        $.ajax({
            type: "GET",
            url: "/api/IngredientesApi",
            dataType: "json",
            success: comboIngredientes
        });
    }

    function lancheEscolhido(idLanche) {
        $.ajax({
            type: "GET",
            url: "/api/LanchesApi/" + idLanche,
            dataType: "json",
            success: mostrarlanche
        });
    }


    
    function adicionarIngrediente(idIngrediente) {
        console.log(idIngrediente);
        var encontrado = false;

        $.each(lanche.Ingredientes,
            function(k, v) {
                if (v.IdIngrediente === idIngrediente) {
                    v.Qtd = v.Qtd + 1;
                    encontrado = true;
                }  
                v.SomaTotal = v.Valor * v.Qtd;
            });
        if (!encontrado) {
            pegarIngrediente(idIngrediente);
        }

        mostrarPedido();

    }

    

    function pegarIngrediente(idIngrediente) {
        $.ajax({
            type: "GET",
            url: "/api/IngredientesApi/" + idIngrediente,
            dataType: "json",
            success: function(dados) {
                lanche.Ingredientes.push(dados);
                mostrarPedido();
            }
        });
    }

    function removerIngrediente(idIngrediente) {
        $.each(lanche.Ingredientes,
            function(k, v) {
                if (v.IdIngrediente === idIngrediente) {
                    v.Qtd > 0 ? v.Qtd -- : v.Qtd = 0;
                }
                v.SomaTotal = v.Valor * v.Qtd;
            });
        mostrarPedido();
    }

    function enviarPedido() {
        $.ajax({
            type: "POST",
            data: lanche,
            url: "/api/Pedidos",
            dataType: "json",
            success: mostrarlanche
        });
        
    }

    return me;
})(jQuery);
