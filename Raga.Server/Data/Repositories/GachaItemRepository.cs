using Microsoft.EntityFrameworkCore;
using Raga.Server.Common.Interfaces;
using Raga.Server.Data.Models;

namespace Raga.Server.Data.Repositories;

public class GachaItemRepository : IGachaItemRepository
{
    private readonly GachaContext _context;

    public GachaItemRepository(GachaContext context)
    {
        _context = context;
    }

    public async Task<GachaItem> AddGachaItemAsync(GachaItem item)
    {
        _context.GachaItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<List<GachaItem>> GetPlayerInventoryAsync(string playerId)
    {
        return await _context.GachaItems.Where(i => i.PlayerId == playerId).ToListAsync();
    }
}