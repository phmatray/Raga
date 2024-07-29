using System.ComponentModel.DataAnnotations;

namespace Raga.Server.Data.Models;

public class PlayerStats
{
    [Key]
    public string PlayerId { get; set; }
    public int Level { get; set; } = 1;
    public int TotalPulls { get; set; }
    public int TotalCurrency { get; set; } = 100;
    public DateTime LastDailyRewardClaim { get; set; }
    public List<GachaItem> Inventory { get; set; } = [];
    public List<string> Achievements { get; set; } = [];
}