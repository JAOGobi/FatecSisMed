﻿using FatecSisMed.RemedioAPI.DTO.Entities;
using System.ComponentModel.DataAnnotations;

namespace FatecSisMed.MarcaAPI.DTO.Entities
{
    public class MarcaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Nome { get; set; }

        public string? Observacao { get; set; }

        public ICollection<RemedioDTO>? RemedioDTOs { get; set; }
    }
}

