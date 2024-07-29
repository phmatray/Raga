using MediatR;
using Raga.Server.Common.Interfaces;

namespace Raga.Server.Features.Gacha.Queries.GetInventoryHandler;

public class GetInventoryHandler(
    IGachaItemRepository gachaItemRepository)
    : IRequestHandler<GetInventoryQuery, InventoryResponse>
{
    public async Task<InventoryResponse> Handle(
        GetInventoryQuery request,
        CancellationToken cancellationToken)
    {
        var inventory = await gachaItemRepository.GetPlayerInventoryAsync(request.PlayerId);
        var response = new InventoryResponse();
        response.Items.AddRange(inventory.Select(i => i.ToGachaItemResponse()));

        return response;
    }
}