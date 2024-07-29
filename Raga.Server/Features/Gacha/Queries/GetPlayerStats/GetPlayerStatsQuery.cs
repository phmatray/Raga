using MediatR;

namespace Raga.Server.Features.Gacha.Queries.GetPlayerStats;

public class GetPlayerStatsQuery : IRequest<PlayerStatsResponse>
{
    public required string PlayerId { get; set; }
}