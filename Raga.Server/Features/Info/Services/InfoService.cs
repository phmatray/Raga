using Grpc.Core;
using MediatR;
using Raga.Server.Features.Info.Queries.GetInfo;

namespace Raga.Server.Features.Info.Services;

public class InfoService(
    ILogger<InfoService> logger,
    IMediator mediator)
    : Server.InfoService.InfoServiceBase
{
    public override async Task<GetInfoResponse> GetInfo(
        GetInfoRequest request,
        ServerCallContext context)
    {
        logger.LogInformation("GetInfo called");
        
        try
        {
            var query = new GetInfoQuery();
            var response = await mediator.Send(query);
            logger.LogInformation("GetInfo succeeded");
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while processing GetInfo");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
}