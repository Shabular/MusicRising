using MusicRising.Models;

namespace MusicRising.Data.Services;

public interface IPromoItemsService
{
    IQueryable<PromoItem> GetAll();
}