using System;
using System.Collections.Generic;

namespace ManagerTask.Domain.Entities;

public partial class Comentario
{
    public int Id { get; set; }

    public int? TarefaId { get; set; }

    public int? UsuarioId { get; set; }

    public string? Conteudo { get; set; }

    public DateTime? DataHoraComentario { get; set; }

    public virtual Tarefa? Tarefa { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
