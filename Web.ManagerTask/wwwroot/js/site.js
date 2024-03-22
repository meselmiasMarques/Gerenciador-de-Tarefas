$(document).ready(function () {

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
        $('#ConfirmaExclusao').click(function () {
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

    $('.btn-detalhar-tarefa').click(() => {
        var tarefaId = $(this).attr('tarefa-id');

        $('#modalDetalharTarefa').modal('show');

    })
})