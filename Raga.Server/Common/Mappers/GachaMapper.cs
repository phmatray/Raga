using Raga.Server.Data.Models;
using Raga.Server.Gacha;

namespace Raga.Server.Common.Mappers;

public static class GachaMapper
{
    public static GachaItemResponse ToGachaItemResponse(this GachaItem item)
    {
        return new GachaItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Rarity = item.Rarity,
            Power = item.Power,
            Level = item.Level
        };
    }

    public static PlayerStatsResponse ToPlayerStatsResponse(this PlayerStats stats)
    {
        var response = new PlayerStatsResponse
        {
            PlayerId = stats.PlayerId,
            Level = stats.Level,
            TotalPulls = stats.TotalPulls,
            TotalCurrency = stats.TotalCurrency
        };
        response.Achievements.AddRange(stats.Achievements);
        return response;
    }
}