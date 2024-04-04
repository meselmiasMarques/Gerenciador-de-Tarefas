using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyTask.Models;

public partial class Projeto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Nome:")]
    public string? NomeProjeto { get; set; }

    [Display(Name = "Descrição:")]
    public string? DescricaoProjeto { get; set; }

    public DateTime? DataCriacaoProjeto { get; set; } = DateTime.Now;

    public bool Status { get; set; } = false;

    public int? UsuarioCriadorId { get; set; }

    public virtual ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();

    public virtual Usuario? UsuarioCriador { get; set; }
}
