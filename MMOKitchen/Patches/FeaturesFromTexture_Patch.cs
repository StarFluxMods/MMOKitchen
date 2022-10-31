using HarmonyLib;
using KitchenLib.Utils;
using UnityEngine;
using Kitchen.Layouts.Modules;
using System.IO;

namespace MMOKitchen
{

    [HarmonyPatch(typeof(FeaturesFromTexture), "ActOn")]
    public class FeaturesFromTexture_Patch
    {
        public static void Prefix(NewFromTexture __instance)
        {
            __instance.SourceTexture = ResourceUtils.LoadTextureFromFile(Path.Combine(Application.streamingAssetsPath, "FeaturesFromTexture.png"));
        }
    }
}