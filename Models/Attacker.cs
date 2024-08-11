namespace IronDome.Models
{
    public class Attacker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Distance { get; set; }
        public IEnumerable<Volley> Volleys { get; set; }
    }
}
