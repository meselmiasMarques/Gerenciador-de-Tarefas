using System;
using System.Collections.Generic;

namespace MyTask.Models;

public partial class HistoricoAtividade
{
    public int Id { get; set; }

    public int? TarefaId { get; set; }

    public int? UsuarioId { get; set; }

    public string? TipoAtividade { get; set; }

    public DateTime? DataHoraAtividade { get; set; }

    public virtual Tarefa? Tarefa { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
