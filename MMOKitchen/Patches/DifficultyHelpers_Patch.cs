using HarmonyLib;
using Kitchen;
using KitchenLib.Preferences;

namespace MMOKitchenReborn.Patches
{
	[HarmonyPatch(typeof(DifficultyHelpers))]
	public class DifficultyHelpers_Patch
	{
		[HarmonyPatch("CustomerPlayersRateModifier")]
		[HarmonyPostfix]
		static void CustomerPlayersRateModifier_Postfix(ref float __result, int player_count)
		{
			if (Main.manager.GetPreference<PreferenceBool>("scaleAbove4Players").Value)
				if (player_count > 4)
					__result = 1 + (player_count * Main.manager.GetPreference<PreferenceFloat>("scaleAbove4PlayersMultiplier").Value);
		}

		[HarmonyPatch("FireSpreadModifier")]
		[HarmonyPostfix]
		static void FireSpreadModifier_Postfix(ref float __result, int player_count)
		{
			if (Main.manager.GetPreference<PreferenceBool>("scaleAbove4Players").Value)
				if (player_count > 4)
					__result = 0.75f + (player_count * Main.manager.GetPreference<PreferenceFloat>("scaleAbove4PlayersMultiplier").Value);
		}

		[HarmonyPatch("PatiencePlayerCountModifier")]
		[HarmonyPostfix]
		static void PatiencePlayerCountModifier_Postfix(ref float __result, int player_count)
		{
			if (Main.manager.GetPreference<PreferenceBool>("scaleAbove4Players").Value)
				if (player_count > 4)
					__result = 0.75f + (player_count * Main.manager.GetPreference<PreferenceFloat>("scaleAbove4PlayersMultiplier").Value);
		}
	}
}
