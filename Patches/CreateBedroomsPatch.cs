using System.Reflection;
using HarmonyLib;
using Kitchen;
using KitchenLib.Utils;
using MMOKitchen.Systems;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace MMOKitchen.Patches
{
    [HarmonyPatch(typeof(CreateBedrooms), "OnUpdate")]
    public class CreateBedroomsPatch
    {
        static void Postfix(CreateBedrooms __instance)
        {
            FieldInfo _Players = ReflectionUtils.GetField<CreateBedrooms>("Players");
            EntityQuery Players = (EntityQuery)_Players.GetValue(__instance);
            NativeArray<Entity> players = Players.ToEntityArray(Allocator.Temp);

            foreach (Entity player in players)
            {
                if (PatchHelpers.instance._GetComponent<CPlayer>(player).Index > 3)
                {
                    PatchHelpers.instance._SetComponentData(player, new CPosition(LobbyPositionAnchors.Office + new Vector3(0,0,-2)));
                }
            }

            players.Dispose();
        }
    }
}