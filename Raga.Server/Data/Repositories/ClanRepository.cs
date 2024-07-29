using Microsoft.EntityFrameworkCore;
using Raga.Server.Common.Interfaces;
using Raga.Server.Data.Models;

namespace Raga.Server.Data.Repositories;

public class ClanRepository(
    GachaContext context)
    : IClanRepository
{
    public async Task<Clan> CreateClanAsync(Clan clan)
    {
        context.Clans.Add(clan);
        await context.SaveChangesAsync();
        return clan;
    }

    public async Task<Clan?> GetClanByIdAsync(string clanId)
    {
        return await context.Clans
            .Include(c => c.Members)
            .FirstOrDefaultAsync(c => c.Id == clanId);
    }

    public async Task<bool> JoinClanAsync(string playerId, string clanId)
    {
        var player = await context.PlayerStats.FindAsync(playerId);
        if (player == null || player.ClanId != null) return false;

        var clan = await context.Clans.FindAsync(clanId);
        if (clan == null || clan.Members.Count >= 50) return false;

        player.ClanId = clanId;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> LeaveClanAsync(string playerId, string clanId)
    {
        var player = await context.PlayerStats.FindAsync(playerId);
        if (player == null || player.ClanId != clanId) return false;

        player.ClanId = null;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<PlayerStats>> GetClanMembersAsync(string clanId)
    {
        var clan = await context.Clans
            .Include(c => c.Members)
            .FirstOrDefaultAsync(c => c.Id == clanId);
        return clan?.Members ?? [];
    }
}