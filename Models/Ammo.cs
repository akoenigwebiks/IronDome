namespace IronDome.Models;
/// <summary>
/// IsDestroyed => hit or intercepted
/// </summary>
public class Ammo
{
    public int Id { get; set; }
    public int LauncherId { get; set; }
    public Launcher Launcher { get; set; }
    public int VolleyId { get; set; }
    public Volley Volley { get; set; }
    public bool IsDestroyed { get; set; }
    public bool IsLaunched { get; set; }
}
