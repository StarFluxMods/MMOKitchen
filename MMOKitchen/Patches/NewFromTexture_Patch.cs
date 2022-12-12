using HarmonyLib;
using UnityEngine;
using Kitchen.Layouts.Modules;
using Kitchen;
using System.Reflection;
using Unity.Entities;
using KitchenLib.Utils;
using System.IO;
using System.Collections.Generic;
using System;

namespace MMOKitchen
{
	
    [HarmonyPatch(typeof(NewFromTexture), "ActOn")]
    public class NewFromTexture_Patch
    {
        public static void Prefix(NewFromTexture __instance)
        {
            PlayerManager playerManager = World.DefaultGameObjectInjectionWorld.GetExistingSystem<PlayerManager>();
            FieldInfo finfo = typeof(PlayerManager).GetField("MaxPlayers", BindingFlags.Instance | BindingFlags.Public);
            finfo.SetValue(playerManager, 12);
			
			__instance.SourceTexture = Main.LoadImage("iVBORw0KGgoAAAANSUhEUgAAAB4AAAAQCAMAAAA25D/gAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAhUExURe0cJIgAFTHf77eA6hbzSv9/J//yAIC66hY68//JDgAAANY41qIAAAALdFJOU/////////////8ASk8B8gAAAAlwSFlzAAAOvwAADr8BOAVTJAAAAF1JREFUKFOl0UkOgDAMQ9Gamdz/wMRuQCVIgOAtIuG/qih2qxA66d1A0KhE3/PoUgY08qSMSiNPm6M5jTxFnzkfnvLkIs87aOR5lReJeMqru2SNPD+zWbyhFb/TbAPTUQgZqLo2tAAAAABJRU5ErkJggg==");
		}
	}
	
}