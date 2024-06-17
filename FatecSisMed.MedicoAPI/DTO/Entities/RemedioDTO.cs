using FatecSisMed.MarcaAPI.DTO.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FatecSisMed.RemedioAPI.DTO.Entities;

public class RemedioDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório!")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O preco é obrigatório!")]
    public int Preco { get; set; }

    [JsonIgnore]
    public MarcaDTO? MarcaDTO { get; set; }
    public int MarcaId { get; set; }

}

