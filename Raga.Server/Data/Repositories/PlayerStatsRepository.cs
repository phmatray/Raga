using Microsoft.EntityFrameworkCore;
using Raga.Server.Common.Interfaces;
using Raga.Server.Data.Models;

namespace Raga.Server.Data.Repositories;

public class PlayerStatsRepository(
    GachaContext context)
    : IPlayerStatsRepository
{
    public async Task<PlayerStats?> GetPlayerStatsAsync(string playerId)
    {
        return await context.PlayerStats
            .Include(p => p.Inventory)
            .FirstOrDefaultAsync(p => p.PlayerId == playerId);
    }

    public async Task<PlayerStats> AddPlayerStatsAsync(PlayerStats playerStats)
    {
        context.PlayerStats.Add(playerStats);
        await context.SaveChangesAsync();
        return playerStats;
    }

    public async Task<PlayerStats> UpdatePlayerStatsAsync(PlayerStats playerStats)
    {
        context.PlayerStats.Update(playerStats);
        await context.SaveChangesAsync();
        return playerStats;
    }
}