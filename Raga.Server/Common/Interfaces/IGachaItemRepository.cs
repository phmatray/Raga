using Raga.Server.Data.Models;

namespace Raga.Server.Common.Interfaces;

public interface IGachaItemRepository
{
    Task<GachaItem> AddGachaItemAsync(GachaItem item);
    Task<List<GachaItem>> GetPlayerInventoryAsync(string playerId);
}