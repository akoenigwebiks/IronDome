using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IronDome.Models;

public enum ATTACK_TYPE
{
    BALISTIC_MISSILE = 18000,
    KATBAM = 300,
    ROCKET = 880
}

public enum ATTACK_SOURCE
{
    IRAN = 1600,
    LEBANON = 50,
    IRAQ = 1500,
    YEMEN = 2377,
    GAZA = 70
}

public class Attack
{
    public int ID { get; set; }
    public DateTime Date { get; set; }
    public ATTACK_TYPE ThreatType { get; set; }
    public ATTACK_SOURCE ThreatSource { get; set; }
    public bool IsActive { get; set; }
    public bool IsInterceptedOrExploded { get; set; }
    public string? ActiveID { get; set; }

    [NotMapped]
    [DisplayName("Time To Impact")]
    public int TimeToImpact
    {
        get
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan timeDifference = currentTime - Date;

            // Convert speed from km/h to km/s
            double speedKmh = (double)ThreatType;
            double speedKms = speedKmh / 3600;

            // Distance in kilometers
            double distanceKm = (double)ThreatSource;

            // Time to impact in seconds
            double timeToImpactSeconds = distanceKm / speedKms;
            return (int)Math.Round(timeToImpactSeconds);
        }
    }

}

