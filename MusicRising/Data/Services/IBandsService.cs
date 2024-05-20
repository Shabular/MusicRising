using MusicRising.Models;

namespace MusicRising.Data.Services;

public interface IBandsService
{
    IQueryable<Band> GetAll();
    Task Add(Band band);

    Task Delete(Band band);
    Task Update(Band band);
}