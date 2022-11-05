using HarmonyLib;
using UnityEngine;
using Kitchen.Layouts.Modules;
using Kitchen;
using System.Reflection;
using Unity.Entities;
using KitchenLib.Utils;
using System.IO;
namespace MMOKitchen
{
    [HarmonyPatch(typeof(NewFromTexture), "ActOn")]
    public class NewFromTexture_Patch
    {
        public static void Prefix(NewFromTexture __instance)
        {
            PlayerManager playerManager = World.DefaultGameObjectInjectionWorld.GetExistingSystem<PlayerManager>();
            FieldInfo finfo = typeof(PlayerManager).GetField("MaxPlayers", BindingFlags.Instance | BindingFlags.Public);
            finfo.SetValue(playerManager, 12);

            __instance.SourceTexture = ResourceUtils.LoadTextureFromFile(Path.Combine(Application.streamingAssetsPath, "NewFromTexture.png"));
        }
    }
}