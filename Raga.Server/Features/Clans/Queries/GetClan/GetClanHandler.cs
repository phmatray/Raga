using MediatR;
using Raga.Server.Common.Interfaces;

namespace Raga.Server.Features.Clans.Queries.GetClan;

public class GetClanHandler(
    IClanRepository clanRepository)
    : IRequestHandler<GetClanQuery, GetClanResponse>
{
    public async Task<GetClanResponse> Handle(
        GetClanQuery request,
        CancellationToken cancellationToken)
    {
        var clan = await clanRepository.GetClanByIdAsync(request.ClanId);
        if (clan == null)
        {
            return new GetClanResponse { Clan = null };
        }

        return new GetClanResponse
        {
            Clan = new ClanResponse
            {
                Id = clan.Id,
                Name = clan.Name,
                Description = clan.Description,
                MemberCount = clan.Members.Count
            }
        };
    }
}