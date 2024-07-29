using MediatR;

namespace Raga.Server.Features.Gacha.Queries.GetPlayer;

public class GetPlayerQuery : IRequest<GetPlayerResponse>
{
    public required string PlayerId { get; set; }
}