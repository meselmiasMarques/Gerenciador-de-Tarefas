using System;
using System.Collections.Generic;

namespace MyTask.Models;

public class Tarefa
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Descricao { get; set; }

    public DateTime? DataCriacao { get; set; } = DateTime.Now;

    public int Status { get; set; } = 1; //SE 1 ENTÃO ESTÁ EM ABERTO SE 0 FECHADO

    public int? UsuarioResponsavelId { get; set; }

    public int ProjetoId { get; set; }

    public int lActive { get; set; } = 1; //Flag de ativação / inativação


   // public virtual ICollection<HistoricoAtividade> HistoricoAtividades { get; set; } = new List<HistoricoAtividade>();

    public virtual Usuario? UsuarioResponsavel { get; set; }
}
