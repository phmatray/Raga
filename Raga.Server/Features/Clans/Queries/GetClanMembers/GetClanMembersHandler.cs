using MediatR;
using Raga.Server.Common.Interfaces;

namespace Raga.Server.Features.Clans.Queries.GetClanMembers;

public class GetClanMembersHandler(
    IClanRepository clanRepository)
    : IRequestHandler<GetClanMembersQuery, GetClanMembersResponse>
{
    public async Task<GetClanMembersResponse> Handle(
        GetClanMembersQuery request,
        CancellationToken cancellationToken)
    {
        var members = await clanRepository.GetClanMembersAsync(request.ClanId);
        var response = new GetClanMembersResponse();
        
        foreach (var member in members)
        {
            response.Members.Add(new PlayerStatsResponse
            {
                PlayerId = member.PlayerId,
                Level = member.Level,
                TotalPulls = member.TotalPulls,
                TotalCurrency = member.TotalCurrency,
                Achievements = { member.Achievements }
            });
        }
        
        return response;
    }
}