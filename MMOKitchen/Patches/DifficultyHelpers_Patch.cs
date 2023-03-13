using HarmonyLib;
using Kitchen;

namespace MMOKitchenReborn.Patches
{
	[HarmonyPatch(typeof(DifficultyHelpers))]
	public class DifficultyHelpers_Patch
	{
		[HarmonyPatch("CustomerPlayersRateModifier")]
		[HarmonyPostfix]
		static void CustomerPlayersRateModifier_Postfix(ref float __result, int player_count)
		{
			__result = 1 + (player_count * 0.25f);
		}

		[HarmonyPatch("FireSpreadModifier")]
		[HarmonyPostfix]
		static void FireSpreadModifier_Postfix(ref float __result, int player_count)
		{
			__result = 0.75f + (player_count * 0.25f);
		}

		[HarmonyPatch("PatiencePlayerCountModifier")]
		[HarmonyPostfix]
		static void PatiencePlayerCountModifier_Postfix(ref float __result, int player_count)
		{
			__result = 0.75f + (player_count * 0.25f);
		}
	}
}
