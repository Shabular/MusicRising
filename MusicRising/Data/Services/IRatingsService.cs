using MusicRising.Models;

namespace MusicRising.Data.Services;

public interface IRatingsService
{
    IQueryable<Rating> GetAll();
}