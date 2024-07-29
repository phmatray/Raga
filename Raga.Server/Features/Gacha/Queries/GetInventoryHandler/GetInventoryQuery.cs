using MediatR;

namespace Raga.Server.Features.Gacha.Queries.GetInventoryHandler;

public class GetInventoryQuery : IRequest<InventoryResponse>
{
    public required string PlayerId { get; set; }
}