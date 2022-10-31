using HarmonyLib;
using Kitchen;
using UnityEngine;

namespace MMOKitchen
{
    [HarmonyPatch(typeof(LayoutBuilder), "BuildWallBetween")]
    public class LayoutBuilder_Patch
    {
        public static bool Prefix(LayoutBuilder __instance, Vector2 tile1, Vector2 tile2)
        {
            if (tile1 == new Vector2(-11, -5) || tile1 == new Vector2(-11, -6))
                return false;
            return true;
        }
    }
}