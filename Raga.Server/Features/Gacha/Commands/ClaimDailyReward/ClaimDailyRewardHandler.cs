using MediatR;
using Raga.Server.Common.Interfaces;
using Raga.Server.Common.Mappers;
using Raga.Server.Data.Models;
using Raga.Server.Gacha;

namespace Raga.Server.Features.Gacha.Commands.ClaimDailyReward;

public class ClaimDailyRewardHandler(
    IPlayerStatsRepository playerStatsRepository,
    IFakeDataGenerator<GachaItem> gachaItemFaker)
    : IRequestHandler<ClaimDailyRewardCommand, DailyRewardResponse>
{
    public async Task<DailyRewardResponse> Handle(
        ClaimDailyRewardCommand request,
        CancellationToken cancellationToken)
    {
        var playerStats = await playerStatsRepository.GetPlayerStatsAsync(request.PlayerId);

        if (playerStats == null)
        {
            return new DailyRewardResponse
            {
                Success = false,
                Message = "Player not found"
            };
        }

        // Check if player has already claimed the daily reward (simplified logic)
        if (DateTime.UtcNow.Date == playerStats.LastDailyRewardClaim.Date)
        {
            var nextRewardTime = playerStats.LastDailyRewardClaim.AddDays(1);
            
            return new DailyRewardResponse
            {
                Success = false,
                Message = $"Daily reward already claimed. Next reward available at {nextRewardTime}"
            };
        }

        playerStats.LastDailyRewardClaim = DateTime.UtcNow;
        playerStats.TotalCurrency += 10; // Example reward
        var rewardItem = gachaItemFaker.Generate();
        rewardItem.PlayerId = request.PlayerId;

        await playerStatsRepository.UpdatePlayerStatsAsync(playerStats);
        
        return new DailyRewardResponse
        {
            Success = true,
            Message = "Daily reward claimed",
            RewardCurrency = 10,
            RewardItem = rewardItem.ToGachaItemResponse()
        };
    }
}