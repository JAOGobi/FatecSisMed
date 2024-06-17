using System;
using System.ComponentModel.DataAnnotations;

namespace FatecSisMed.Web.Models
{
    public class MarcaViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }

        public string? Observacao { get; set; }
    }
}

