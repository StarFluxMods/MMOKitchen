using HarmonyLib;
using KitchenLib.Utils;
using UnityEngine;
using Kitchen.Layouts.Modules;
using System.IO;

namespace MMOKitchen
{

	
    [HarmonyPatch(typeof(FeaturesFromTexture), "ActOn")]
    public class FeaturesFromTexture_Patch
    {
        public static void Prefix(NewFromTexture __instance)
        {
			__instance.SourceTexture = Main.LoadImage("iVBORw0KGgoAAAANSUhEUgAAAB4AAAAQCAMAAAA25D/gAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAMUExURQD/ACQA/+nKIQAAAOiZT8gAAAAEdFJOU////wBAKqn0AAAACXBIWXMAAA6/AAAOvwE4BVMkAAAASUlEQVQoU52MyQ0AIAgEBfrv2StcYTXReSAyLE2u/Og2p7uAjTComihrZ32ITse9U5KuvKSh3kTFrMXSNTkxPV6wAUPgOOaqRToQCQU9rUN4jgAAAABJRU5ErkJggg==");
		}
	}
	
}