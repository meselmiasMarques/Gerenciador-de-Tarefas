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

    //$('.btn-cadastrar-usuario').click(function () {

    //    $.ajax({
    //        type: 'GET',
    //        url: '/Usuario/Cadastrar/',
    //        success: function (result) {
    //            $('#modalCadastraUsuario').modal('show');

    //            $("#cadastroUsuarios").html(result);
    //        }
    //    });

    //    $('#ConfirmaExclusao').click(function () {

    //        $.ajax({
    //            type: 'POST',
    //            url: '/Usuario/Excluir/' + usuarioId,
    //            success: function () {
    //                location.reload();

    //                $('#modalExcluirUsuario').modal('hide');

    //            }


    //        });
    //    });

    //});


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
})