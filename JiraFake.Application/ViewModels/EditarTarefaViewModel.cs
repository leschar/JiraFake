﻿using JiraFake.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace JiraFake.Application.ViewModels
{
    public class EditarTarefaViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome não pode ser nulo.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O Nome deve ter entre 2 e 50 caracteres.")]

        public string Nome { get; set; }

        [StringLength(500, MinimumLength = 0, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        public string Descricao { get; set; }

        [EnumDataType(typeof(StatusEnum), ErrorMessage = "O campo Status deve ter um valor válido do enum StatusEnum.")]
        public StatusEnum Status { get; set; }
    }
}
