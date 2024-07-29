using MediatR;
using Raga.Server.Common.Interfaces;

namespace Raga.Server.Features.Clans.Commands.JoinClan;

public class JoinClanHandler(
    IClanRepository clanRepository)
    : IRequestHandler<JoinClanCommand, JoinClanResponse>
{
    public async Task<JoinClanResponse> Handle(
        JoinClanCommand request,
        CancellationToken cancellationToken)
    {
        // TODO: maximum 30 players per clan
        
        var success = await clanRepository.JoinClanAsync(request.PlayerId, request.ClanId);
        return new JoinClanResponse
        {
            Success = success,
            Message = success ? "Joined clan successfully" : "Failed to join clan"
        };
    }
}