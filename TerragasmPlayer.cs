using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terragasm;

public class TerragasmPlayer : ModPlayer
{
    public override void OnEnterWorld(Player player)
        => Task.Run(() => DeviceController.ConnectAsync());

    public override void OnHitAnything(float x, float y, Entity victim)
        => Task.Run(() => DeviceController.VibrateAsync(0.25, 500));

    public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        => Task.Run(() => DeviceController.VibrateAsync(crit ? 1 : 0.5, 500));

    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        => Task.Run(() => DeviceController.VibrateAsync(1, Player.respawnTimer * 1000 / 60));

    public override void PreSavePlayer()
        => Task.Run(DeviceController.DisconnectAsync);
}