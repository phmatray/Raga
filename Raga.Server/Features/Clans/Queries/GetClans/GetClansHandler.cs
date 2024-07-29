using MediatR;
using Raga.Server.Common.Interfaces;

namespace Raga.Server.Features.Clans.Queries.GetClans;

public class GetClansHandler(
    IClanRepository clanRepository)
    : IRequestHandler<GetClansQuery, GetClansResponse>
{
    public async Task<GetClansResponse> Handle(
        GetClansQuery request,
        CancellationToken cancellationToken)
    {
        var clans = await clanRepository.GetClansByScoreAsync(request.Location);
        var response = new GetClansResponse();

        foreach (var clan in clans)
        {
            var clanScoreResponse = new ClanScoreResponse
            {
                Id = clan.Id,
                Name = clan.Name,
                Description = clan.Description,
                MemberCount = clan.Members.Count,
                Score = clan.Members.Sum(m => m.TotalCurrency)
            };
            
            if (clan.Location != null)
            {
                clanScoreResponse.Location = clan.Location;
            }
            
            
            response.Clans.Add(clanScoreResponse);
        }

        return response;
    }
}