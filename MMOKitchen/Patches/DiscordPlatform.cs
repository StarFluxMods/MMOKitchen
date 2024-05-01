using HarmonyLib;
using System.Reflection;
using System.Reflection.Emit;
using Steamworks;
using Kitchen.NetworkSupport;
using System.Threading.Tasks;
using System;
using Steamworks.Data;
namespace MMOKitchen
{
    [HarmonyPatch(typeof(DiscordPlatform), "SetActivity")]
    public class DiscordPlatform_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> _instructions)
        {
            var codes = new List<CodeInstruction>(_instructions);
            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldloca_S && codes[i + 1].opcode == OpCodes.Ldc_I4_4 &&
                    codes[i + 2].opcode == OpCodes.Stfld && codes[i + 3].opcode == OpCodes.Ldloc_S)
                {
                    codes[i + 1].opcode = OpCodes.Ldc_I4;
                    codes[i + 1].operand = 12;
                }
            }

            return codes.AsEnumerable();
        }
    }
}