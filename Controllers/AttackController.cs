using IronDome.Data;
using IronDome.Models;
using IronDome.Services;
using IronDome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IronDome.Controllers;
public class AttackController : Controller
{

    private readonly IronDomeContext _context;
    private readonly AttackHandlerService _attackHandlerService;

    public AttackController(IronDomeContext context, AttackHandlerService attackHandlerService)
    {
        _context = context;
        _attackHandlerService = attackHandlerService;
        _attackHandlerService.RebuildActiveAttacks();
    }

    public IActionResult Index()
    {
        List<Attack>? attacks = _context.Attack.ToList();
        return View(attacks);
    }

    public IActionResult Create()
    {
        AttackViewModel avm = new AttackViewModel()
        {
            Attack = new Attack(),
            AttackTypes = GetSelectListItems<ATTACK_TYPE>(),
            AttackSources = GetSelectListItems<ATTACK_SOURCE>(),
        };

        return View(avm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] AttackViewModel attackVM)
    {

        await _attackHandlerService.CreateAttack(new Attack()
        {
            Date = DateTime.Now,
            IsActive = false,
            ThreatType = attackVM.SelectedAttackType,
            ThreatSource = attackVM.SelectedAttackSource
        });

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("IronDome/StartAttack/{attackId}")]
    public async Task<IActionResult> StartAttack([FromRoute] int attackId)
    {
        bool isAttackLaunched = await _attackHandlerService.RegisterAndRunAttackTask(attackId);

        return isAttackLaunched ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Index), new { Error = "Attack not found" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EndAttack(int attackId)
    {
        Attack? attack = _context.Attack.Find(attackId);

        bool result = await _attackHandlerService.RemoveAttack(attackId);

        return result ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Index), new { Error = "Attack not found" });
    }

    private List<SelectListItem> GetSelectListItems<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
                   .Cast<TEnum>()
                   .Select(e => new SelectListItem
                   {
                       Value = ((int)(object)e).ToString(),
                       Text = e.ToString()
                   }).ToList();
    }
}

