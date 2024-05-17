using MusicRising.Models;
using ZstdSharp.Unsafe;

namespace MusicRising.Data.Services;

public interface IShowsService
{
    IQueryable<Show> GetAll();

    Task Add(Show show);

}