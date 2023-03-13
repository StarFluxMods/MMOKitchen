using HarmonyLib;
using Kitchen.NetworkSupport;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MMOKitchenReborn.Patches
{
	/*
	 * This patch is used to change how many players can join the lobby using Discord.
	 */

	[HarmonyPatch(typeof(DiscordPlatform), "CreateNewLobby")]
	public static class DiscordPlatform_Patch
	{
		static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			var codes = new List<CodeInstruction>(instructions);
			for (var i = 0; i < codes.Count; i++)
				if (codes[i].opcode == OpCodes.Ldc_I4_4)
					codes[i].opcode = OpCodes.Ldc_I4_8;

			return codes;
		}
	}
}
