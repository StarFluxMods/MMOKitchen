using Kitchen;
using KitchenLib;
using KitchenLib.Event;
using KitchenLib.Preferences;
using KitchenMods;
using MMOKitchenReborn.Menus;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MMOKitchenReborn
{
	public class Main : BaseMod
	{
		public const string MOD_ID = "mmokitchen";
		public const string MOD_NAME = "MMO Kitchen";
		public const string MOD_AUTHOR = "StarFluxGames";
		public const string MOD_VERSION = "0.2.1";
		public const string MOD_COMPATIBLE_VERSIONS = ">=1.1.4";

		public static PreferenceManager manager;
		public Main() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_COMPATIBLE_VERSIONS, Assembly.GetExecutingAssembly()) { }

		protected override void OnPostActivate(Mod mod)
		{
			manager = new PreferenceManager(MOD_ID);
			manager.RegisterPreference(new PreferenceInt("requiredConsentPercentage", 100));
			manager.RegisterPreference(new PreferenceBool("scaleAbove4Players", false));
			manager.RegisterPreference(new PreferenceFloat("scaleAbove4PlayersMultiplier", 0.1f));
			manager.Load();

			ModsPreferencesMenu<PauseMenuAction>.RegisterMenu("MMO Kitchen", typeof(PreferenceMenu<PauseMenuAction>), typeof(PauseMenuAction));

			Events.PreferenceMenu_PauseMenu_CreateSubmenusEvent += (s, args) =>
			{
				args.Menus.Add(typeof(PreferenceMenu<PauseMenuAction>), new PreferenceMenu<PauseMenuAction>(args.Container, args.Module_list));
			};

			ModsPreferencesMenu<MainMenuAction>.RegisterMenu("MMO Kitchen", typeof(PreferenceMenu<MainMenuAction>), typeof(MainMenuAction));

			Events.PreferenceMenu_MainMenu_CreateSubmenusEvent += (s, args) =>
			{
				args.Menus.Add(typeof(PreferenceMenu<MainMenuAction>), new PreferenceMenu<MainMenuAction>(args.Container, args.Module_list));
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogInfo(string message)
		{
			Debug.Log($"[{MOD_NAME}] " + message);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogWarning(string message)
		{
			Debug.LogWarning($"[{MOD_NAME}] " + message);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void LogError(string message)
		{
			Debug.LogError($"[{MOD_NAME}] " + message);
		}
	}
}