﻿using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicRising.Models;

namespace MusicRising.Data.Services
{
    public class BandsService : IBandsService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BandsService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IQueryable<Band> GetAll()
        {
            try
            {
                var bands = _context.Bands.Include(b => b.User);
                Debug.WriteLine($"Found {bands.Count()} bands");
                return bands;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching bands: {ex.Message}");
                return Enumerable.Empty<Band>().AsQueryable();
            }
        }
        public async Task<Band> GetBandByIdAsync(string id)
        {
            return await _context.Bands.Include(b => b.User).FirstOrDefaultAsync(b => b.BandId == id);
        }

        public async Task Add(Band band)
        {
            _context.Bands.Add(band);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Band band)
        {
            _context.Bands.Remove(band);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Band band)
        {
            _context.Bands.Update(band);
            await _context.SaveChangesAsync();
        }


    }
}