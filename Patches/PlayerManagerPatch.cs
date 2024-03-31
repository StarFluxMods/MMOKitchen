using HarmonyLib;
using Kitchen;
using KitchenLib.Utils;
using System.Reflection;

namespace MMOKitchen.Patches
{
    [HarmonyPatch(typeof(PlayerManager), "GetLeastUnusedIndex")]
    public class PlayerManagerPatch
    {
        static void Prefix(PlayerManager __instance)
        {
            FieldInfo info = ReflectionUtils.GetField<PlayerManager>("MaxPlayers");
            info.SetValue(__instance, Mod.MaxPlayers);
        }
    }
}
