using MediatR;
using Raga.Server.Gacha;

namespace Raga.Server.Features.Gacha.Commands.ClaimDailyReward;

public class ClaimDailyRewardCommand : IRequest<DailyRewardResponse>
{
    public required string PlayerId { get; set; }
}