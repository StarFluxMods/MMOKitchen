using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using SteamNetworkService = Kitchen.NetworkSupport.SteamNetworkService;

namespace MMOKitchen.Patches
{
    /*
     * This patch is used to change how many players can join the lobby using Steam.
     */
    [HarmonyPatch(typeof(SteamNetworkService))]
    public class SteamPlatformPatch
    {
        private static MethodBase TargetMethod()
        {
            Type type = AccessTools.FirstInner(typeof(SteamNetworkService), t => t.Name.Contains("<CreateNewLobby>d__31"));
            return AccessTools.FirstMethod(type, method => method.Name.Contains("MoveNext"));
        }
        
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeMatcher matcher = new(instructions);

            foreach (var instruction in matcher.InstructionEnumeration())
            {
                if (instruction.operand != null)
                {
                    Debug.Log($"{instruction.opcode} : {instruction.operand}");
                }
                else
                {
                    Debug.Log($"{instruction.opcode}");
                }

            }

            matcher.MatchForward(false, new CodeMatch(OpCodes.Ldc_I4_4), new CodeMatch(OpCodes.Call), new CodeMatch(OpCodes.Callvirt), new CodeMatch(OpCodes.Stloc_S), new CodeMatch(OpCodes.Ldloca_S), new CodeMatch(OpCodes.Call), new CodeMatch(OpCodes.Brtrue))
                .Set(OpCodes.Ldc_I4, Mod.MaxPlayers); 
				
            return matcher.InstructionEnumeration();
        }
    }
}
