using System.ComponentModel.DataAnnotations;

namespace IronDome.Models;

public enum DEFENSE_TYPE
{
    IRON_DOME
}

public class Defense
{
    [Key]
    public int ID { get; set; }
    public int Ammunition { get; set; }
}
