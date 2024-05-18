using MusicRising.Models;

namespace MusicRising.Data.Services;

public interface IVenuesService
{
    IQueryable<Venue> GetAll();
}