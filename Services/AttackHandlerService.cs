using System.Collections.Concurrent;
using IronDome.Data;
using IronDome.Models;

namespace IronDome.Services
{
    public class AttackHandlerService
    {
        private static ConcurrentDictionary<string, CancellationTokenSource> _attacks = new ConcurrentDictionary<string, CancellationTokenSource>();
        private readonly IronDomeContext _context;

        public AttackHandlerService(IronDomeContext ctx)
        {
            _context = ctx;
        }

        //create an attack from attack model
        public async Task CreateAttack(Attack attack)
        {
            _context.Attack.Add(attack);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RegisterAndRunAttackTask(int attackId)
        {
            Attack? attack = _context.Attack.Find(attackId);
            bool isRunning = IsAttackRunning(attackId);

            if (attack == null || attack.IsInterceptedOrExploded || isRunning) return false;

            var attackActiveId = Guid.NewGuid().ToString();
            attack.ActiveID = attackActiveId;
            attack.IsActive = true;
            await _context.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            _attacks[attackActiveId] = cts;

            /*
             var cts = jhgjhghjgvkj
             defenses[defensseid]=cts
             */

            /*green non awaited warning without _ = */
            _ = Task.Run(() => RunTask(attackActiveId, cts.Token), cts.Token);

            return true;
        }

        // a method to check if attack is running
        public bool IsAttackRunning(int attackId)
        {
            Attack? attack = _context.Attack.Find(attackId);
            if (attack == null ||
                attack.IsActive == false ||
                attack.ActiveID == null ||
                attack.IsInterceptedOrExploded == true) return false;

            return _attacks.ContainsKey(attack.ActiveID);
        }

        public async Task<bool> RemoveAttack(int attackId)
        {
            Attack? attack = _context.Attack.Find(attackId);

            bool isRunning = IsAttackRunning(attackId);

            if (isRunning == false)
            {
                return false;
            }

            _attacks.TryRemove(attack.ActiveID, out CancellationTokenSource? cts);
            //var cts = _attacks[attack.ActiveID];
            cts?.Cancel();

            attack.ActiveID = null;
            attack.IsActive = false;
            attack.IsInterceptedOrExploded = true;
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task RunTask(string attackId, CancellationToken token)
        {
            try
            {
                int elapsed = 0;
                while (elapsed < 120 && !token.IsCancellationRequested)
                {
                    await Task.Delay(2000, token); // Wait for 2 seconds or cancel if requested
                    elapsed += 2;
                    var message = $"Attack {attackId} running for {elapsed} seconds.";
                    Console.WriteLine(message);
                    //await _hubContext.Clients.All.SendAsync("ReceiveProgress", message);
                }

                // Finished
                if (!token.IsCancellationRequested)
                {
                    //await _hubContext.Clients.All.SendAsync("ReceiveProgress", $"Attack {attackId} completed.");
                }
            }
            catch (TaskCanceledException)
            {
                //await _hubContext.Clients.All.SendAsync("ReceiveProgress", $"Attack {attackId} cancelled.");
            }
            finally
            {
                await RemoveAttack(int.Parse(attackId));
            }
        }

        internal void RebuildActiveAttacks()
        {
            if (_attacks.Count > 0)
            {
                return;
            }

            var attacks = _context.Attack.Where(a => a.IsActive).ToList();
            foreach (var attack in attacks)
            {
                if (attack.ActiveID != null)
                {
                    var cts = new CancellationTokenSource();
                    _attacks[attack.ActiveID] = cts;
                    _ = Task.Run(() => RunTask(attack.ActiveID, cts.Token), cts.Token);
                }
            }
        }
    }
}
