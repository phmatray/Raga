using MediatR;
using Raga.Server.Common.Interfaces;
using Raga.Server.Data.Models;

namespace Raga.Server.Features.Gacha.Commands.PullGacha;

public class PullGachaHandler(
    IPlayerRepository playerRepository,
    IGachaItemRepository gachaItemRepository,
    IFakeDataGenerator<GachaItem> gachaItemFaker)
    : IRequestHandler<PullGachaCommand, GachaPullResponse>
{
    public async Task<GachaPullResponse> Handle(
        PullGachaCommand request,
        CancellationToken cancellationToken)
    {
        var player = await playerRepository.GetPlayerAsync(request.PlayerId);
            
        if (player == null)
        {
            // If Player doesn't exist, create it
            player = new Player { PlayerId = request.PlayerId };
            await playerRepository.AddPlayerAsync(player);
        }
        
        var item = gachaItemFaker.Generate();
        item.PlayerId = request.PlayerId;
        await gachaItemRepository.AddGachaItemAsync(item);
        
        player.TotalPulls++;
        player.TotalCurrency -= 10; // Assume each pull costs 10 currency

        await playerRepository.UpdatePlayerAsync(player);

        return new GachaPullResponse
        {
            Item = item.ToGachaItemResponse(),
            RemainingCurrency = player.TotalCurrency
        };
    }
}