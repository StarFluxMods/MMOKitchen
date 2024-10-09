using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Kitchen;

namespace MMOKitchen.Patches
{
    [HarmonyPatch(typeof(Session), "GetInvite")]
    public class SessionPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher matcher = new(instructions);
            
            matcher.MatchForward(false, new CodeMatch(OpCodes.Ldloca_S), new CodeMatch(OpCodes.Ldc_I4_3), new CodeMatch(OpCodes.Ldc_I4_4))
                .Advance(2)
                .Set(OpCodes.Ldc_I4, Mod.MaxPlayers); 
				
            return matcher.InstructionEnumeration();
        }
    }
}