using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raga.Server.Data.Models;

public class GachaItem
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Rarity { get; set; }
    public int Power { get; set; }
    public int Level { get; set; }
    public string PlayerId { get; set; }
    [ForeignKey("PlayerId")]
    public PlayerStats PlayerStats { get; set; }
}