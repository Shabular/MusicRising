using MusicRising.Models;

namespace MusicRising.Data.Services;

public interface IBandsService
{
    IQueryable<Band> GetAll();
    Task Add(Band band);

}