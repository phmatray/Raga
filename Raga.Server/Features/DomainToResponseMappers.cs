using Raga.Server.Data.Models;

namespace Raga.Server.Features;

public static class DomainToResponseMappers
{
    public static ClanResponse ToClanResponse(
        this Clan clan)
    {
        var target = new ClanResponse
        {
            Id = clan.Id,
            Name = clan.Name,
            Description = clan.Description,
            MemberCount = clan.Members.Count,
            Score = clan.Members.Sum(m => m.TotalCurrency)
        };
        
        if (clan.Location != null)
        {
            target.Location = "Global";
        }
        
        return target;
    }
    
    public static PlayerResponse ToPlayerResponse(
        this Player player)
    {
        var target = new PlayerResponse
        {
            PlayerId = player.PlayerId,
            Level = player.Level,
            TotalPulls = player.TotalPulls,
            TotalCurrency = player.TotalCurrency,
            Achievements = { player.Achievements }
        };
        
        return target;
    }
    
    public static GachaItemResponse ToGachaItemResponse(
        this GachaItem item)
    {
        var target = new GachaItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Rarity = item.Rarity,
            Power = item.Power,
            Level = item.Level
        };
        
        return target;
    }
}