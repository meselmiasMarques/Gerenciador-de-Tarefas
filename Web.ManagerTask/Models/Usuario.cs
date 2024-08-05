using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyTask.Models;

public partial class Usuario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [MaxLength(100)]
    [MinLength(3)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress( ErrorMessage = "O Formato do email não é válido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [PasswordPropertyText]
    public string? Senha { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<HistoricoAtividade> HistoricoAtividades { get; set; } = new List<HistoricoAtividade>();

    public virtual ICollection<ProjetoViewModel> Projetos { get; set; } = new List<ProjetoViewModel>();

    public virtual ICollection<TarefaViewModel> Tarefas { get; set; } = new List<TarefaViewModel>();
}
