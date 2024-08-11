namespace IronDome.Models;

public class Launcher
{
    // add id,name,range,velocity
    public int Id { get; set; }
    public string Name { get; set; }
    public int Range { get; set; }
    public int Velocity { get; set; }
    public IEnumerable<Ammo>? Ammo { get; set; }
    public int AttackerId { get; set; }
    public Attacker Attacker { get; set; }
}

