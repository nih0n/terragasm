using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.Player;

namespace Terragasm;

public class TerragasmPlayer : ModPlayer
{
    public override void OnEnterWorld()
        => Task.Run(() => DeviceController.ConnectAsync());

    public override void OnHitAnything(float x, float y, Entity victim)
        => Task.Run(() => DeviceController.VibrateAsync(0.25, 500));

    public override void OnHurt(HurtInfo info)
        => Task.Run(() => DeviceController.VibrateAsync(1, 500));

    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        => Task.Run(() => DeviceController.VibrateAsync(1, Player.respawnTimer * 1000 / 60));

    public override void PreSavePlayer()
        => Task.Run(DeviceController.DisconnectAsync);
}