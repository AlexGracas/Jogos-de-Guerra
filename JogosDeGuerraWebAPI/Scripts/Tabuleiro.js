/***
 * Baseado no código 
 * https://www.devmedia.com.br/desenhando-um-tabuleiro-de-damas-em-html-css-js/24591
 * 
 * **/
var ObterBatalha;
var IniciarBatalha;
var CriarNovaBatalha;
var MontarTabuleiro;
$(function () {
    var baseUrl = window.location.protocol + "//" +
        window.location.hostname +
        (window.location.port ? ':' + window.location.port : '');
    var casa_selecionada = null;
    var batalha = null;
    var peca_selecionadaId = null;
    var pecasNoTabuleiro = null;
    var pecaSelecionadaObj = null;
    var pecaElem = null;
    var elementos = null;

    //1 CriarNovaBatalha, 2 RetomarBatalha    
    var token = sessionStorage.getItem("accessToken");

    ObterBatalha = function (BatalhaId) {
        var urlObterBatalha = baseUrl + "/api/Batalhas/" + BatalhaId;
        var headers = {};
        if (token) {
            headers.Authorization = token;
        }
        $.ajax({
            type: 'GET',
            url: urlObterBatalha,
            headers: headers
        })
            .done(function (data) {
                VerificarBatalha(data);
            })
            .fail(
            function (jqXHR, textStatus) {
                alert("Código de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
            });
    }

    CriarNovaBatalha = function (NacaoID) {
        var urlCriarNovaBatalha = baseUrl + "/api/Batalhas/CriarNovaBatalha?Nacao=" + NacaoID;
        var headers = {};
        if (token) {
            headers.Authorization = token;
        }
        $.ajax({
            type: 'GET',
            url: urlCriarNovaBatalha,
            headers: headers
        }
        ).done(function (data) {
            window.location.reload();
        }
            ).fail(
            function (jqXHR, textStatus) {
                alert("Código de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
            });
    }

    IniciarBatalha = function (BatalhaID) {
        var urlIniciarBatalha = baseUrl + "/api/Batalhas/Iniciar?Id=" + BatalhaID;
        var headers = {};
        if (token) {
            headers.Authorization = token;
        }
        $.ajax({
            type: 'GET',
            url: urlIniciarBatalha,
            headers: headers
        }
        ).done(function (data) {
            VerificarBatalha(data);
        }
            ).fail(
            function (jqXHR, textStatus) {
                alert("Código de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
            });
    }
    
    MontarTabuleiro = function(batalhaParam) {
        pecasNoTabuleiro = [];
        var batalha = batalhaParam;
        var pecas = batalha.Tabuleiro.ElementosDoExercito
        var ExercitoBrancoId = batalha.ExercitoBrancoId;
        var ExercitoPretoId = batalha.ExercitoPretoId;
        var i;
        $("#tabuleiro").empty();
        for (i = 0; i < batalha.Tabuleiro.Altura; i++) {
            $("#tabuleiro").append("<div id='linha_" + i.toString() + "' class='linha' >");
            pecasNoTabuleiro[i] = [];
            for (j = 0; j < batalha.Tabuleiro.Largura; j++) {
                var nome_casa = "casa_" + i.toString() + "_" + j.toString();
                var classe = (i % 2 == 0 ? (j % 2 == 0 ? "casa_branca" : "casa_preta") : (j % 2 != 0 ? "casa_branca" : "casa_preta"));
                $("#linha_" + i.toString()).append("<div id='" + nome_casa + "' class='casa " + classe + "' />");
   
                for (x = 0; x < pecas.length; x++) {
                    if (pecas[x].Saude <= 0) {
                        continue;
                    }
                    if (pecas[x].posicao.Altura == i && pecas[x].posicao.Largura == j){
                        pecasNoTabuleiro[i][j] = pecas[x];                    
                        if (pecas[x].ExercitoId==ExercitoBrancoId) {
                            $("#" + nome_casa).append("<img src='https://www.w3schools.com/images/compatible_firefox.gif' class='peca' id='" + nome_casa.replace("casa", "peca_preta") + "'/>");
                        }
                        else if (pecas[x].ExercitoId == ExercitoPretoId) {
                            $("#" + nome_casa).append("<img src='https://www.w3schools.com/images/compatible_safari.gif' class='peca' id='" + nome_casa.replace("casa", "peca_branca") + "'/>");
                        }

                    }                    
                }

            }
        }
        $(".casa").click(function () {
            //Retirando a seleção da casa antiga.
            $("#" + casa_selecionada).removeClass("casa_selecionada");
            //Obtendo o Id.
            casa_selecionada = $(this).attr("id");
            //Adicionando Vermelho na Casa nova.
            $("#" + casa_selecionada).addClass("casa_selecionada");
            //Legenda que mostra informações da casa selecionada.
            $("#info_casa_selecionada").text(casa_selecionada);
            var altura = casa_selecionada.split("_")[1]
            var largura = casa_selecionada.split("_")[2]
            if (pecaElem == null) {
                //Obter o id da imagem selecionada.
                peca_selecionadaId = ObterPecaIDNaCasa(casa_selecionada);
                //Se for nulo
                if (peca_selecionadaId == null) {
                    pecaElem = null;
                    peca_selecionadaId = "NENHUMA PECA SELECIONADA";
                } else {
                    //Guardar a peça selecionada.
                    pecaElem = document.getElementById(peca_selecionadaId);
                    pecaSelecionadaObj = pecasNoTabuleiro[altura][largura];
                }
                //Legenda que mostra informações da peça selecionada.
                $("#info_peca_selecionada").text(peca_selecionadaId.toString());
            } else {
                var posicaopeca = {
                    Altura: altura,
                    Largura: largura
                };
                var ExercitoTurno = (batalha.TurnoId == batalha.ExercitoBrancoId) ? batalha.ExercitoBranco : batalha.ExercitoPreto;

                if(ObterPecaIDNaCasa(casa_selecionada) == null) {
                    ataque = false;
                } else {
                    ataque = true;
                }

                var movimento = {
                    Posicao: posicaopeca,
                    AutorId: ExercitoTurno.UsuarioId,
                    BatalhaId: batalha.Id,
                    ElementoId: pecaSelecionadaObj.Id,
                    TipoMovimento: ataque ? "Atacar" :"Mover"
                };
                var EmailUsuario = sessionStorage.getItem("emailUsuario");


                if (ExercitoTurno.Usuario.Email == EmailUsuario &&
                    ExercitoTurno.Id == pecaSelecionadaObj.ExercitoId
                ) {
                    Mover(movimento, pecaElem.parentNode, document.getElementById(casa_selecionada), pecaElem);
                } else if (ExercitoTurno.Usuario.Email != EmailUsuario){
                    alert("Não é a sua vez!");
                } else if (ExercitoTurno.Id != pecaSelecionadaObj.ExercitoId){
                    alert("Não é o seu exercito!");
                }
            

                pecaElem = null;
                $("#" + casa_selecionada).removeClass("casa_selecionada");
            }
        });

        function ObterPecaIDNaCasa(casa_selecionada) {
            return $("#" + casa_selecionada).children("img:first").attr("id");
        }

        function Mover(movimento, posAntiga, posNova, peca) {
            var token = sessionStorage.getItem("accessToken");
            var headers = {};
            if (token) {
                headers.Authorization = token;
            }
            $.ajax({
                type: 'POST',
                url: baseUrl + "/api/Batalhas/Jogar",
                headers: headers,
                data: movimento
            })
                .done(
                function (data) {
                    MontarTabuleiro(data);
                    /*
                    if (movimento.TipoMovimento == "Mover") {
                        MoverPeca(posAntiga, posNova, peca)
                    } else {
                        window.reload();
                    }
                    */
                }
                )
                .fail(
                function (jqXHR, textStatus) {
                    alert("Código de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
                });
        }


        function MoverPeca(posAntiga, posNova, peca) {
//            var casaElem = document.getElementById(casa_selecionada);
            //Remover a peça da casa antiga.
            posAntiga.removeChild(peca);
            //Colocar a peça na nova casa.
            posNova.appendChild(peca);
            //pecaElem = null para não mover a peça no novo clique.
            posNova.classList.remove("casa_selecionada")
        }
    }   

    function VerificarBatalha(Batalha) {
        if (Batalha.Estado == 0) {
            if (Batalha.ExercitoPretoId == null || Batalha.ExercitoBrancoId == null) {
                if ((Batalha.ExercitoPreto != null &&
                    Batalha.ExercitoPreto.Usuario.Email ==
                    sessionStorage.getItem("EmailUsuario")) ||
                    (Batalha.ExercitoBranco != null &&
                        Batalha.ExercitoBranco.Usuario.Email ==
                        sessionStorage.getItem("EmailUsuario"))
                ) {
                    alert("Espere. Ainda não existe jogador disponível");
                } else {
                    IniciarBatalha(Batalha.Id);
                }
            } else {
                IniciarBatalha(Batalha.Id);
            }
        } else {
            MontarTabuleiro(Batalha);
            if (Batalha.Estado == 10 || Batalha.Estado == 99) {
            }
        }
    }

});
