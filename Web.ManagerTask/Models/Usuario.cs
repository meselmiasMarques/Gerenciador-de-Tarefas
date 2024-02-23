using System;
using System.Collections.Generic;

namespace MyTask.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Email { get; set; }

    public string? Senha { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<HistoricoAtividade> HistoricoAtividades { get; set; } = new List<HistoricoAtividade>();

    public virtual ICollection<Projeto> Projetos { get; set; } = new List<Projeto>();

    public virtual ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}
