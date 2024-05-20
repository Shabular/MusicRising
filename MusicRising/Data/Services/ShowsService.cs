﻿using Microsoft.EntityFrameworkCore;
using MusicRising.Models;

namespace MusicRising.Data.Services;

public class ShowsService : IShowsService
{
    
    private readonly ApplicationDbContext _context;

    public ShowsService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<Show> GetAll()
    {
        var applicationDbContext = _context.Shows.Include(s => s.HeadLiner).Include(s => s.Venue);
        return applicationDbContext;

    }

    public async Task Add(Show show)
    {
        _context.Shows.Add(show);
        await _context.SaveChangesAsync();
    }

    public Task Delete(Show show)
    {
        throw new NotImplementedException();
    }

    public Task Update(Show show)
    {
        throw new NotImplementedException();
    }
}