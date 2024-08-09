using HarmonyLib;
using Kitchen.NetworkSupport;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MMOKitchen.Patches
{
    /*
     * This patch is used to change how many players can join the lobby using Steam.
     */
    [HarmonyPatch(typeof(SteamPlatform))]
    public class SteamPlatformPatch
    { 
        private static MethodBase TargetMethod()
        {
            Type type = AccessTools.FirstInner(typeof(SteamPlatform), t => t.Name.Contains("<CreateNewLobby>d__26"));
            return AccessTools.FirstMethod(type, method => method.Name.Contains("MoveNext"));
        }
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher matcher = new(instructions);
            
            matcher.MatchForward(false, new CodeMatch(OpCodes.Ldc_I4_2), new CodeMatch(OpCodes.Bne_Un), new CodeMatch(OpCodes.Ldc_I4_4))
                .Advance(2)
                .Set(OpCodes.Ldc_I4, Mod.MaxPlayers); 
				
            return matcher.InstructionEnumeration();
        }
    }
}
