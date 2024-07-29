using Grpc.Core;
using MediatR;
using Raga.Server.Features.Gacha.Commands;
using Raga.Server.Features.Gacha.Commands.ClaimDailyReward;
using Raga.Server.Features.Gacha.Commands.PullGacha;
using Raga.Server.Features.Gacha.Commands.TradeItems;
using Raga.Server.Features.Gacha.Queries;
using Raga.Server.Features.Gacha.Queries.GetInventoryHandler;
using Raga.Server.Features.Gacha.Queries.GetPlayerStats;
using Raga.Server.Gacha;

namespace Raga.Server.Features.Gacha.Services;

public class GachaService(
    ILogger<GachaService> logger,
    IMediator mediator)
    : Server.Gacha.Gacha.GachaBase
{
    public override async Task<GachaPullResponse> PullGacha(
        GachaPullRequest request,
        ServerCallContext context)
    {
        logger.LogInformation("PullGacha called with PlayerId: {PlayerId}", request.PlayerId);
        
        try
        {
            var command = new PullGachaCommand { PlayerId = request.PlayerId };
            var response = await mediator.Send(command);
            logger.LogInformation("PullGacha succeeded for PlayerId: {PlayerId}", request.PlayerId);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while processing PullGacha for PlayerId: {PlayerId}", request.PlayerId);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<InventoryResponse> GetInventory(
        InventoryRequest request,
        ServerCallContext context)
    {
        logger.LogInformation("GetInventory called with PlayerId: {PlayerId}", request.PlayerId);
        
        try
        {
            var query = new GetInventoryQuery { PlayerId = request.PlayerId };
            var response = await mediator.Send(query);
            logger.LogInformation("GetInventory succeeded for PlayerId: {PlayerId}", request.PlayerId);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while processing GetInventory for PlayerId: {PlayerId}", request.PlayerId);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<PlayerStatsResponse> GetPlayerStats(
        PlayerRequest request,
        ServerCallContext context)
    {
        logger.LogInformation("GetPlayerStats called with PlayerId: {PlayerId}", request.PlayerId);
        
        try
        {
            var query = new GetPlayerStatsQuery { PlayerId = request.PlayerId };
            var response = await mediator.Send(query);
            logger.LogInformation("GetPlayerStats succeeded for PlayerId: {PlayerId}", request.PlayerId);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while processing GetPlayerStats for PlayerId: {PlayerId}", request.PlayerId);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<TradeResponse> TradeItems(
        TradeRequest request,
        ServerCallContext context)
    {
        logger.LogInformation("TradeItems called with FromPlayerId: {FromPlayerId}, ToPlayerId: {ToPlayerId}", request.FromPlayerId, request.ToPlayerId);
        
        try
        {
            var command = new TradeItemsCommand
            {
                FromPlayerId = request.FromPlayerId,
                ToPlayerId = request.ToPlayerId,
                OfferedItemId = request.OfferedItemId,
                RequestedItemId = request.RequestedItemId
            };
                
            var response = await mediator.Send(command);
            logger.LogInformation("TradeItems succeeded for FromPlayerId: {FromPlayerId}, ToPlayerId: {ToPlayerId}", request.FromPlayerId, request.ToPlayerId);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while processing TradeItems for FromPlayerId: {FromPlayerId}, ToPlayerId: {ToPlayerId}", request.FromPlayerId, request.ToPlayerId);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<DailyRewardResponse> ClaimDailyReward(
        DailyRewardRequest request,
        ServerCallContext context)
    {
        logger.LogInformation("ClaimDailyReward called with PlayerId: {PlayerId}", request.PlayerId);
        
        try
        {
            var command = new ClaimDailyRewardCommand { PlayerId = request.PlayerId };
            var response = await mediator.Send(command);
            logger.LogInformation("ClaimDailyReward succeeded for PlayerId: {PlayerId}", request.PlayerId);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while processing ClaimDailyReward for PlayerId: {PlayerId}", request.PlayerId);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
}
