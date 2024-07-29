using MediatR;
using Raga.Server.Common.Interfaces;
using Raga.Server.Data.Models;

namespace Raga.Server.Features.Gacha.Commands.ClaimDailyReward;

public class ClaimDailyRewardHandler(
    IPlayerRepository playerRepository,
    IFakeDataGenerator<GachaItem> gachaItemFaker)
    : IRequestHandler<ClaimDailyRewardCommand, DailyRewardResponse>
{
    public async Task<DailyRewardResponse> Handle(
        ClaimDailyRewardCommand request,
        CancellationToken cancellationToken)
    {
        var player = await playerRepository.GetPlayerAsync(request.PlayerId);

        if (player == null)
        {
            return new DailyRewardResponse
            {
                Success = false,
                Message = "Player not found"
            };
        }

        // Check if player has already claimed the daily reward (simplified logic)
        if (DateTime.UtcNow.Date == player.LastDailyRewardClaim.Date)
        {
            var nextRewardTime = player.LastDailyRewardClaim.AddDays(1);
            
            return new DailyRewardResponse
            {
                Success = false,
                Message = $"Daily reward already claimed. Next reward available at {nextRewardTime}"
            };
        }

        player.LastDailyRewardClaim = DateTime.UtcNow;
        player.TotalCurrency += 10; // Example reward
        var rewardItem = gachaItemFaker.Generate();
        rewardItem.PlayerId = request.PlayerId;

        await playerRepository.UpdatePlayerAsync(player);
        
        return new DailyRewardResponse
        {
            Success = true,
            Message = "Daily reward claimed",
            RewardCurrency = 10,
            RewardItem = rewardItem.ToGachaItemResponse()
        };
    }
}