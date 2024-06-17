using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FatecSisMed.Web.Models
{
    public class RemedioViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }
        public int Preco { get; set; }
        public string? Marca { get; set; }
        public string? MarcaID { get; set; }
    }
}

