using MediatR;
using Raga.Server.Common.Interfaces;
using Raga.Server.Common.Mappers;
using Raga.Server.Data.Models;

namespace Raga.Server.Features.Gacha.Commands.PullGacha;

public class PullGachaHandler(
    IPlayerStatsRepository playerStatsRepository,
    IGachaItemRepository gachaItemRepository,
    IFakeDataGenerator<GachaItem> gachaItemFaker)
    : IRequestHandler<PullGachaCommand, GachaPullResponse>
{
    public async Task<GachaPullResponse> Handle(
        PullGachaCommand request,
        CancellationToken cancellationToken)
    {
        var playerStats = await playerStatsRepository.GetPlayerStatsAsync(request.PlayerId);
            
        if (playerStats == null)
        {
            // If PlayerStats doesn't exist, create it
            playerStats = new PlayerStats { PlayerId = request.PlayerId };
            await playerStatsRepository.AddPlayerStatsAsync(playerStats);
        }
        
        var item = gachaItemFaker.Generate();
        item.PlayerId = request.PlayerId;
        await gachaItemRepository.AddGachaItemAsync(item);
        
        playerStats.TotalPulls++;
        playerStats.TotalCurrency -= 10; // Assume each pull costs 10 currency

        await playerStatsRepository.UpdatePlayerStatsAsync(playerStats);

        return new GachaPullResponse
        {
            Item = item.ToGachaItemResponse(),
            RemainingCurrency = playerStats.TotalCurrency
        };
    }
}