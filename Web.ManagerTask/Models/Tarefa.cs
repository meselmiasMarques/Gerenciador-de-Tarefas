using System;
using System.Collections.Generic;

namespace MyTask.Models;

public partial class Tarefa
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Descricao { get; set; }

    public DateTime? DataCriacao { get; set; } = DateTime.Now;

    public bool Status { get; set; }

    public int? UsuarioResponsavelId { get; set; }

    public int? ProjetoId { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<HistoricoAtividade> HistoricoAtividades { get; set; } = new List<HistoricoAtividade>();

    public virtual Projeto? Projeto { get; set; }

    public virtual Usuario? UsuarioResponsavel { get; set; }
}
