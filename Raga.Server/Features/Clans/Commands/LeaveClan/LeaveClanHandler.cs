using MediatR;
using Raga.Server.Common.Interfaces;

namespace Raga.Server.Features.Clans.Commands.LeaveClan;

public class LeaveClanHandler(
    IClanRepository clanRepository)
    : IRequestHandler<LeaveClanCommand, LeaveClanResponse>
{
    public async Task<LeaveClanResponse> Handle(
        LeaveClanCommand request,
        CancellationToken cancellationToken)
    {
        var success = await clanRepository.LeaveClanAsync(request.PlayerId, request.ClanId);
        return new LeaveClanResponse
        {
            Success = success,
            Message = success ? "Left clan successfully" : "Failed to leave clan"
        };
    }
}