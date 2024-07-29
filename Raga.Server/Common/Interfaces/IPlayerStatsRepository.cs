using Raga.Server.Data.Models;

namespace Raga.Server.Common.Interfaces;

public interface IPlayerStatsRepository
{
    Task<PlayerStats?> GetPlayerStatsAsync(string playerId);
    Task<PlayerStats> AddPlayerStatsAsync(PlayerStats playerStats);
    Task<PlayerStats> UpdatePlayerStatsAsync(PlayerStats playerStats);
}