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
			__instance.SourceTexture = Main.LoadImage("iVBORw0KGgoAAAANSUhEUgAAAB4AAAAQCAMAAAA25D/gAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAMUExURQD/ACQA/+nKIQAAAOiZT8gAAAAEdFJOU////wBAKqn0AAAACXBIWXMAAA6/AAAOvwE4BVMkAAAAS0lEQVQoU52MUQ4AIAhCU+9/5yzLbFJb8YGOJxa56geXlpqBixBkTLRjtRH0QXR6vrapDWe9tCE2RcQ8zdu52eRYJ7iAJfAc64pFKggsBTc0VJH0AAAAAElFTkSuQmCC");
		}
	}
	
}