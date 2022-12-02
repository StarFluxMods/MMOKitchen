using HarmonyLib;
using Kitchen;
using Unity.Entities;
using System.Reflection;
using KitchenLib.Utils;
using UnityEngine;
using KitchenData;
using Unity.Collections;

namespace MMOKitchen
{
    [HarmonyPatch(typeof(CreateBedrooms), "OnUpdate")]
    public class CreateBedrooms_Patch
    {
        public static bool Prefix(CreateBedrooms __instance)
        {
            MethodInfo createAssigned = ReflectionUtils.GetMethod<CreateBedrooms>("CreateAssigned");
            MethodInfo placeSpawnMarker = ReflectionUtils.GetMethod<CreateBedrooms>("PlaceSpawnMarker");
            MethodInfo getComponent = ReflectionUtils.GetMethod<CreateBedrooms>("GetComponent").MakeGenericMethod(typeof(CPlayer));
            FieldInfo players = ReflectionUtils.GetField<CreateBedrooms>("Players");
			NativeArray<Entity> nativeArray = ((EntityQuery)players.GetValue(__instance)).ToEntityArray(Allocator.Temp);
			Vector3[] array = new Vector3[]
			{
				new Vector3(9f, 0f, 5f),
				new Vector3(9f, 0f, 2f),
				new Vector3(9f, 0f, -3f),
				new Vector3(9f, 0f, -6f),
				new Vector3(14f, 0f, 5f),
				new Vector3(14f, 0f, 2f),
				new Vector3(14f, 0f, -3f),
				new Vector3(14f, 0f, -6f),
				new Vector3(-12f, 0f, 5f),
				new Vector3(-12f, 0f, 2f),
				new Vector3(-12f, 0f, -3f),
				new Vector3(-12f, 0f, -6f)
			};
			for (int i = 0; i < 12; i++)
			{
                Entity target = (Entity)createAssigned.Invoke(__instance, new object[] { i, GameData.Main.Get<Appliance>(AssetReference.Bed), array[i] + new Vector3(0f, 0f, 0f), Vector3.forward });
                Entity entity = (Entity)createAssigned.Invoke(__instance, new object[] { i, GameData.Main.Get<Appliance>(AssetReference.InteractionProxy), array[i] + new Vector3(0f, 0f, 0f), Vector3.forward });
				EntityUtils.GetEntityManager().AddComponentData<CInteractionProxy>(entity, new CInteractionProxy
				{
					Target = target,
					IsActive = true
				});
                createAssigned.Invoke(__instance, new object[] { i, GameData.Main.Get<Appliance>(AssetReference.OutfitStation), array[i] + new Vector3(-2f, 0f, 1f), Vector3.forward });
                createAssigned.Invoke(__instance, new object[] { i, GameData.Main.Get<Appliance>(AssetReference.CosmeticStation), array[i] + new Vector3(-2f, 0f, -1f), Vector3.forward });
                createAssigned.Invoke(__instance, new object[] { i, GameData.Main.Get<Appliance>(AssetReference.ColourStation), array[i] + new Vector3(-3f, 0f, 1f), Vector3.forward });
                createAssigned.Invoke(__instance, new object[] { i, GameData.Main.Get<Appliance>(AssetReference.OccupationIndicator), array[i] + new Vector3(1f, 0f, 0f), Vector3.forward });
                placeSpawnMarker.Invoke(__instance, new object[] { i, array[i] + new Vector3(-1f, 0f, 0f) });
				foreach (Entity entity2 in nativeArray)
				{
                    bool flag = i == ((CPlayer)getComponent.Invoke(__instance, new object[] { entity2 })).Index;
					if (flag)
					{
						EntityUtils.GetEntityManager().SetComponentData<CPosition>(entity2, new CPosition(array[i] + new Vector3(-1f, 0f, 0f)));
						break;
					}
				}
			}
			nativeArray.Dispose();
            return false;
        }
    }
}