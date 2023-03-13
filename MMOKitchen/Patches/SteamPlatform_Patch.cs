﻿using HarmonyLib;
using Kitchen.NetworkSupport;
using Steamworks;
using Steamworks.Data;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace MMOKitchenReborn.Patches
{
	/*
	 * This patch is used to change how many players can join the lobby using Steam.
	 */

	[HarmonyPatch(typeof(SteamPlatform), "CreateNewLobby")]
	public class SteamPlatform_Patch
	{
		public static bool Prefix(SteamPlatform __instance, Action<bool, Lobby> callback)
		{
			if (__instance.IsReady)
			{
				SteamMatchmaking.CreateLobbyAsync(12).ContinueWith(delegate (Task<Lobby?> task)
				{
					if (task.IsCompleted && task.Result != null)
					{
						Lobby valueOrDefault = task.Result.GetValueOrDefault();
						__instance.CurrentInviteLobby = valueOrDefault;

						MethodInfo performSetPermissions = AccessTools.Method(typeof(SteamPlatform), "PerformSetPermissions");
						performSetPermissions.Invoke(__instance, new object[] { __instance.Permissions });
						callback(true, valueOrDefault);
					}
					else
					{
						callback(false, default(Lobby));
					}
				});
			}
			return false;
		}
	}
}
