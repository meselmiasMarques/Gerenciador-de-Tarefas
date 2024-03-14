using System;
using System.Collections.Generic;

namespace MyTask.Models;

public class Tarefa
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Descricao { get; set; }

    public DateTime? DataCriacao { get; set; } = DateTime.Now;

    public bool Status { get; set; } = false;

    public int? UsuarioResponsavelId { get; set; }


   // public virtual ICollection<HistoricoAtividade> HistoricoAtividades { get; set; } = new List<HistoricoAtividade>();

    public virtual Usuario? UsuarioResponsavel { get; set; }
}
