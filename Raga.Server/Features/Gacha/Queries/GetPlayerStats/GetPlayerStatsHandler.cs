using MediatR;
using Raga.Server.Common.Interfaces;
using Raga.Server.Common.Mappers;
using Raga.Server.Gacha;

namespace Raga.Server.Features.Gacha.Queries.GetPlayerStats;

public class GetPlayerStatsHandler(
    IPlayerStatsRepository playerStatsRepository)
    : IRequestHandler<GetPlayerStatsQuery, PlayerStatsResponse>
{
    public async Task<PlayerStatsResponse> Handle(
        GetPlayerStatsQuery request,
        CancellationToken cancellationToken)
    {
        var stats = await playerStatsRepository.GetPlayerStatsAsync(request.PlayerId);
        return stats.ToPlayerStatsResponse();
    }
}