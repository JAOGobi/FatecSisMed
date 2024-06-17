using FatecSisMed.RemedioAPI.DTO.Entities;

namespace FatecSisMed.RemedioAPI.Services.Interfaces;

public interface IRemedioService
{
    Task<IEnumerable<RemedioDTO>> GetAll();
    Task<RemedioDTO> GetById(int id);
    Task Create(RemedioDTO remedioDTO);
    Task Update(RemedioDTO remedioDTO);
    Task Remove(int id);
}

