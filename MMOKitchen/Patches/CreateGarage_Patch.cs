using HarmonyLib;
using Kitchen;
using Unity.Entities;
using System.Reflection;
using KitchenLib.Utils;
using UnityEngine;
using System;
using KitchenData;

namespace MMOKitchen
{
    [HarmonyPatch(typeof(CreateGarage), "OnUpdate")]
    public class CreateGarage_Patch
    {
        public static bool Prefix(CreateGarage __instance)
        {
            MethodInfo create = AccessTools.Method(typeof(CreateGarage), "Create", new Type[] {typeof(Appliance), typeof(Vector3), typeof(Vector3)});
            FieldInfo crates = ReflectionUtils.GetField<CreateGarage>("Crates");

			GameObject garageDecorations = GameObjectUtils.GetChildObject(GameData.Main.Get<Appliance>(AssetReference.GarageDecorations).Prefab, "LoadoutDoor");
			GameObject doorLight = GameObjectUtils.GetChildObject(GameData.Main.Get<Appliance>(AssetReference.GarageDecorations).Prefab, "Door Light");
			doorLight.SetActive(false);
			garageDecorations.SetActive(false);
            create.Invoke(__instance, new object[]{GameData.Main.Get<Appliance>(AssetReference.GarageDecorations), new Vector3(-8f, 0f, -2f), Vector3.forward});

			Mod.Log(GameData.Main.Get<Appliance>(AssetReference.GarageDecorations).Prefab.name);
			Entity entity = default(Entity);
            entity = (Entity)create.Invoke(__instance, new object[]{GameData.Main.Get<Appliance>(AssetReference.LoadoutPedestal), new Vector3(-8.5f, 0f, -4f), Vector3.right});
			EntityUtils.GetEntityManager().AddComponent<CItemPedestal>(entity);
            entity = (Entity)create.Invoke(__instance, new object[]{GameData.Main.Get<Appliance>(AssetReference.LoadoutPedestal), new Vector3(-9.5f, 0f, -4f), Vector3.right});
            bool isEmpty = ((EntityQuery)crates.GetValue(__instance)).IsEmpty;
			if (!isEmpty)
			{
				for (int i = 0; i < 6; i++)
				{
					bool flag = i == 2;
					if (!flag)
					{
						for (int j = 0; j < 5; j++)
						{
							Vector3 facing = (j % 2 == 1) ? Vector3.forward : Vector3.back;
                            Entity entity2 = (Entity)create.Invoke(__instance, new object[]{GameData.Main.Get<Appliance>(AssetReference.GarageShelf), new Vector3((float)(-5 - i), 0f, (float)(1 - j)), facing});
							EntityUtils.GetEntityManager().AddComponentData<CPersistentItemStorageLocation>(entity2, new CPersistentItemStorageLocation
							{
								Type = PersistentStorageType.Crate
							});
							bool flag2 = j % 2 == 1;
							if (flag2)
							{
                                create.Invoke(__instance, new object[]{GameData.Main.Get<Appliance>(AssetReference.GarageDivider), new Vector3((float)(-5 - i), 0f, (float)(1 - j)), Vector3.forward});
							}
						}
					}
				}
			}
            return false;
        }
    }
}