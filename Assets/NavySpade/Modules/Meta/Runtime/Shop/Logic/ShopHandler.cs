using NavySpade.Modules.Sound.Runtime.Core;
using ShopItem = NavySpade.Meta.Runtime.Shop.Items.ShopItem;

namespace Core.Meta.Shop.Logic
{
    public static class ShopHandler
    {
        public static bool TryBuy(ShopItem upgrade)
        {
            if (upgrade.TryBuy() == false)
                return false;

            Vibration.VibratePeek();
            SoundPlayer.PlaySoundFx("Upgrade_Successful");

            return true;
        }
    }
}