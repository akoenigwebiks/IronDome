namespace IronDome.Models;

public class VolleyViewModel
{
    public Volley Volley { get; set; }
    public List<LauncherAmmoSummary> LauncherAmmoSummary { get; set; }
}

public class LauncherAmmoSummary
{
    public string LauncherName { get; set; }
    public int AmmoCount { get; set; }
}
