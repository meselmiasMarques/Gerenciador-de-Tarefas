﻿using System;
using System.Collections.Generic;

namespace MyTask.Models;

public partial class Projeto
{
    public int Id { get; set; }

    public string? NomeProjeto { get; set; }

    public string? DescricaoProjeto { get; set; }

    public DateTime? DataCriacaoProjeto { get; set; } = DateTime.Now;

    public bool Status { get; set; } = false;

    public int? UsuarioCriadorId { get; set; }

    public virtual ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();

    public virtual Usuario? UsuarioCriador { get; set; }
}
