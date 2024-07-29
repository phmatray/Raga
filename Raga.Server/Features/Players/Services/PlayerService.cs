using Grpc.Core;
using MediatR;
using Raga.Server.Features.Players.Queries.GetPlayer;

namespace Raga.Server.Features.Players.Services;

public class PlayerService(
    ILogger<PlayerService> logger,
    IMediator mediator)
    : Server.PlayerService.PlayerServiceBase
{
    public override async Task<GetPlayerResponse> GetPlayer(
        GetPlayerRequest request,
        ServerCallContext context)
    {
        logger.LogInformation("GetPlayer called with PlayerId: {PlayerId}", request.PlayerId);
        
        try
        {
            var query = new GetPlayerQuery { PlayerId = request.PlayerId };
            var response = await mediator.Send(query);
            logger.LogInformation("GetPlayer succeeded for PlayerId: {PlayerId}", request.PlayerId);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while processing GetPlayer for PlayerId: {PlayerId}", request.PlayerId);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override Task<UpdatePlayerResponse> UpdatePlayer(
        UpdatePlayerRequest request,
        ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<DeletePlayerResponse> DeletePlayer(
        DeletePlayerRequest request,
        ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<ListPlayersResponse> ListPlayers(
        ListPlayersRequest request,
        ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}