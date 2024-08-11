namespace IronDome.Models
{
    public class Volley
    {
        public int Id { get; set; }
        public DateTime? LaunchDate { get; set; }
        public List<Launcher> Launchers { get; set; } = new List<Launcher>();
    }
}
