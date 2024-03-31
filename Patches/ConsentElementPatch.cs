using HarmonyLib;
using Kitchen;
using Kitchen.Modules;
using KitchenLib.Preferences;
using KitchenLib.Utils;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MMOKitchen.Patches
{
    [HarmonyPatch(typeof(ConsentElement), "Update")]
    public class ConsentElementPatch
    {
        public static bool Prefix(ConsentElement __instance)
        {

            PlayerManager pm = Unity.Entities.World.DefaultGameObjectInjectionWorld.GetExistingSystem<PlayerManager>();
            if (pm == null)
            {
                return true;
            }

            MethodInfo updateBar = ReflectionUtils.GetMethod<ConsentElement>("UpdateBar");
            MethodInfo getProgressSpeed = ReflectionUtils.GetMethod<ConsentElement>("GetProgressSpeed");
            FieldInfo progress = ReflectionUtils.GetField<ConsentElement>("Progress");
            PropertyInfo isCompleted = AccessTools.Property(typeof(ConsentElement), "IsCompleted");

            FieldInfo consentsSwap = ReflectionUtils.GetField<ConsentElement>("ConsentsSwap");

            Dictionary<int, bool> x = (Dictionary<int, bool>)consentsSwap.GetValue(__instance);

            int counter = 0;
            foreach (int y in x.Keys)
                if (x[y])
                    counter++;

            float progressSpeed = (float)getProgressSpeed.Invoke(__instance, new object[] { });

            if ((((float)counter / (float)x.Count) * 100) >= Mod.manager.GetPreference<PreferenceInt>("requiredConsentPercentage").Value)
                progressSpeed = 1f;

            if (progressSpeed <= 0f)
                progress.SetValue(__instance, (float)progress.GetValue(__instance) - (2f * Time.unscaledDeltaTime));
            else
                progress.SetValue(__instance, (float)progress.GetValue(__instance) + (progressSpeed * Time.unscaledDeltaTime));

            isCompleted.SetValue(__instance, ((float)progress.GetValue(__instance) >= 1f));
            progress.SetValue(__instance, Mathf.Clamp01((float)progress.GetValue(__instance)));
            updateBar.Invoke(__instance, new object[] { });

            return false;
        }
    }
}
