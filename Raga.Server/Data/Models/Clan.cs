using System.ComponentModel.DataAnnotations;

namespace Raga.Server.Data.Models;

public class Clan
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<PlayerStats> Members { get; set; } = [];
}