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
        $('#modalExcluirUsuario').click(function () {
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