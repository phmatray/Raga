using Raga.Server.Data.Models;

namespace Raga.Server.Common.Interfaces;

public interface IClanRepository
{
    Task<Clan> CreateClanAsync(Clan clan);
    Task<Clan?> GetClanByIdAsync(string clanId);
    Task<bool> JoinClanAsync(string playerId, string clanId);
    Task<bool> LeaveClanAsync(string playerId, string clanId);
    Task<List<PlayerStats>> GetClanMembersAsync(string clanId);
    Task<List<Clan>> GetClansByScoreAsync(string? location);
}