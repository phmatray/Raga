using System.Threading.Tasks;
using Grpc.Net.Client;

namespace Raga.Game.Libs;

public class GachaClient
{
    private readonly Gacha.GachaClient _client;
    private readonly string _playerId;

    public GachaClient(string serverAddress, string playerId)
    {
        var channel = GrpcChannel.ForAddress(serverAddress);
        _client = new Gacha.GachaClient(channel);
        _playerId = playerId;
    }

    public async Task<GachaPullResponse> PullGachaAsync()
    {
        return await _client.PullGachaAsync(new GachaPullRequest { PlayerId = _playerId });
    }

    public async Task<InventoryResponse> GetInventoryAsync()
    {
        return await _client.GetInventoryAsync(new InventoryRequest { PlayerId = _playerId });
    }

    public async Task<PlayerStatsResponse> GetPlayerStatsAsync()
    {
        return await _client.GetPlayerStatsAsync(new PlayerRequest { PlayerId = _playerId });
    }

    public async Task<TradeResponse> TradeItemsAsync(string toPlayerId, string offeredItemId, string requestedItemId)
    {
        return await _client.TradeItemsAsync(new TradeRequest
        {
            FromPlayerId = _playerId,
            ToPlayerId = toPlayerId,
            OfferedItemId = offeredItemId,
            RequestedItemId = requestedItemId
        });
    }
}