﻿$(document).ready(function () {

    $('.btn-editar-usuario').click(function () {
        var usuarioId = $(this).attr('usuario-id');

        $.ajax({
            type: 'GET',
            url: '/Usuario/Editar/' + usuarioId,
            success: function (result) {
                $("#editaUsuarios").html(result);
                $('#modalEditaUsuario').modal('show');
            }
        });

    });

    $('.btn-excluir-usuario').click(function () {
        var usuarioId = $(this).attr('usuario-id');

        $('#modalExcluirUsuario').modal('show');
        $('.btn-confirma-exclusao').click(function () {
         
            $.ajax({
                type: 'POST',
                url: '/Usuario/Excluir/' + usuarioId,
                success: function () {
                    location.reload();

                    $('#modalExcluirUsuario').modal('hide');

                }
            });

        });
    });


    $('.btn-excluir-projeto').click(function () {
        var projetoId = $(this).attr('projeto-id');
        console.log(projetoId)

        $('#modalExcluirProjeto').modal('show');
        $('#ProjetoConfirmaExclusao').click(function () {
            $.ajax({
                type: 'POST',
                url: '/Projeto/Excluir/' + projetoId,
                success: function () {
                    location.reload();

                    $('#modalExcluirProjeto').modal('hide');

                }
            });

        });
    });

    $('.btn-detalhar-tarefa').click(function () {
        var tarefaId = $(this).attr('tarefa-id');

        $('#modalDetalharTarefa').modal('show');
      
        $.ajax({
            type: 'GET',
            url: '/Tarefa/Detalhar/' + tarefaId,
            success: function (result) {
                $("#detalheTarefas").html(result);
                
            }
        });
    })


    $('.btn-excluir-tarefa').click(function () {
        var tarefaId = $(this).attr('tarefa-id');
       

        $('#modalExcluirTarefa').modal('show');
        $('.btn-confirmar-excluir-tarefa').click(function () {
            $.ajax({
                type: 'POST',
                url: '/Tarefa/Excluir/' + tarefaId,
                success: function () {
                    location.reload();

                    $('#modalExcluirTarefa').modal('hide');

                }
            });

        });
    });


    // Função para ocultar o alerta após 5 segundos
    setTimeout(function () {
        $('#alertaSuccesso').fadeOut('slow');
        $('#alertaErro').fadeOut('slow');
    }, 3000); // 5000 milissegundos = 5 segundos

})