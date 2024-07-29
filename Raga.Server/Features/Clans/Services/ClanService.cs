using Grpc.Core;
using MediatR;
using Raga.Server.Features.Clans.Commands.CreateClan;
using Raga.Server.Features.Clans.Commands.JoinClan;
using Raga.Server.Features.Clans.Commands.LeaveClan;
using Raga.Server.Features.Clans.Queries.GetClan;
using Raga.Server.Features.Clans.Queries.GetClanMembers;
using Raga.Server.Features.Clans.Queries.GetClans;

namespace Raga.Server.Features.Clans.Services;

public class ClanService(
    ILogger<Gacha.Services.GachaService> logger,
    IMediator mediator)
    : Server.ClanService.ClanServiceBase
{
    public override async Task<CreateClanResponse> CreateClan(CreateClanRequest request, ServerCallContext context)
    {
        logger.LogInformation("CreateClan called with Name: {Name}", request.Name);

        try
        {
            var command = new CreateClanCommand { Name = request.Name, Description = request.Description };
            var response = await mediator.Send(command);
            logger.LogInformation("CreateClan succeeded with Name: {Name}", request.Name);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating clan with Name: {Name}", request.Name);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<GetClanResponse> GetClan(GetClanRequest request, ServerCallContext context)
    {
        logger.LogInformation("GetClan called with ClanId: {ClanId}", request.ClanId);

        try
        {
            var query = new GetClanQuery { ClanId = request.ClanId };
            var response = await mediator.Send(query);
            logger.LogInformation("GetClan succeeded for ClanId: {ClanId}", request.ClanId);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while retrieving clan with ClanId: {ClanId}", request.ClanId);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<JoinClanResponse> JoinClan(JoinClanRequest request, ServerCallContext context)
    {
        logger.LogInformation("JoinClan called with PlayerId: {PlayerId}, ClanId: {ClanId}", request.PlayerId, request.ClanId);

        try
        {
            var command = new JoinClanCommand { PlayerId = request.PlayerId, ClanId = request.ClanId };
            var response = await mediator.Send(command);
            logger.LogInformation("JoinClan succeeded for PlayerId: {PlayerId}, ClanId: {ClanId}", request.PlayerId, request.ClanId);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while joining clan for PlayerId: {PlayerId}, ClanId: {ClanId}", request.PlayerId, request.ClanId);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<LeaveClanResponse> LeaveClan(LeaveClanRequest request, ServerCallContext context)
    {
        logger.LogInformation("LeaveClan called with PlayerId: {PlayerId}, ClanId: {ClanId}", request.PlayerId, request.ClanId);

        try
        {
            var command = new LeaveClanCommand { PlayerId = request.PlayerId, ClanId = request.ClanId };
            var response = await mediator.Send(command);
            logger.LogInformation("LeaveClan succeeded for PlayerId: {PlayerId}, ClanId: {ClanId}", request.PlayerId, request.ClanId);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while leaving clan for PlayerId: {PlayerId}, ClanId: {ClanId}", request.PlayerId, request.ClanId);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<GetClanMembersResponse> GetClanMembers(GetClanMembersRequest request,
        ServerCallContext context)
    {
        logger.LogInformation("GetClanMembers called with ClanId: {ClanId}", request.ClanId);

        try
        {
            var query = new GetClanMembersQuery { ClanId = request.ClanId };
            var response = await mediator.Send(query);
            logger.LogInformation("GetClanMembers succeeded for ClanId: {ClanId}", request.ClanId);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while retrieving clan members for ClanId: {ClanId}", request.ClanId);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<GetClansResponse> GetClans(GetClansRequest request, ServerCallContext context)
    {
        logger.LogInformation("GetClans called with Location: {Location}", request.Location);

        try
        {
            var query = new GetClansQuery { Location = request.Location };
            var response = await mediator.Send(query);
            logger.LogInformation("GetClans succeeded for Location: {Location}", request.Location);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while retrieving clans for Location: {Location}", request.Location);
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
}