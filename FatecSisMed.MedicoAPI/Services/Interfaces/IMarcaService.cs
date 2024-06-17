
using FatecSisMed.MarcaAPI.DTO.Entities;
using FatecSisMed.MedicoAPI.DTO.Entities;

namespace FatecSisMed.MarcaAPI.Services.Interfaces;

public interface IMarcaService
{
    Task<IEnumerable<MarcaDTO>> GetAll();
    Task<MarcaDTO> GetById(int id);
    Task<IEnumerable<MarcaDTO>> GetMarcaRemedios();
    Task Create(MarcaDTO marcaDTO);
    Task Update(MarcaDTO marcaDTO);
    Task Remove(int id);
}

