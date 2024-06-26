﻿using MusicRising.Models;

namespace MusicRising.Data.Services;

public interface IVenuesService
{
    IQueryable<Venue> GetAll();
    Task Add(Venue venue);
    Task Delete(Venue venue);
    Task Update(Venue venue);
    bool IsVenueHolder(string? userID);
    Task<Venue> GetVenueByIdAsync(string id);
 
}