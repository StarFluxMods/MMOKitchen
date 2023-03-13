using HarmonyLib;
using Kitchen;
using KitchenLib.Utils;
using System.Reflection;

namespace MMOKitchenReborn.Patches
{
	[HarmonyPatch(typeof(PlayerManager), "Initialise")]
	public class PlayerManager_Patch
	{
		static void Prefix(PlayerManager __instance)
		{
			FieldInfo maxPlayers = ReflectionUtils.GetField<PlayerManager>("MaxPlayers");
			maxPlayers.SetValue(__instance, 12);
		}
	}
}
