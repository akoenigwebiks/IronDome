namespace IronDome.Models
{
    public class Volley
    {
        public int Id { get; set; }
        public DateTime LaunchDate { get; set; }
        public IEnumerable<Launcher> Launchers { get; set; }
    }
}
