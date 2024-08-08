using IronDome.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IronDome.ViewModels;

public class AttackViewModel
{
    public Attack? Attack { get; set; }
    public ATTACK_TYPE SelectedAttackType { get; set; }
    public ATTACK_SOURCE SelectedAttackSource { get; set; }
    public List<SelectListItem>? AttackTypes { get; set; }
    public List<SelectListItem>? AttackSources { get; set; }
}
