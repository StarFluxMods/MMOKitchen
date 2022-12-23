using KitchenLib;
using System.Reflection;
using KitchenLib.Utils;
using UnityEngine;
using System;
using Kitchen;
using KitchenLib.Event;
using MMOKitchen.Menus;

#if BEPINEX
using BepInEx;
#endif
#if WORKSHOP
using KitchenMods;
#endif

namespace MMOKitchen
{
#if BEPINEX
	[BepInProcess("PlateUp.exe")]
	[BepInPlugin(MOD_ID, MOD_NAME, MOD_VERSION)]
#endif
	public class Main : BaseMod
	{
		public const string MOD_ID = "mmokitchen";
		public const string MOD_NAME = "MMO Kitchen";
		public const string MOD_AUTHOR = "StarFluxGames";
		public const string MOD_VERSION = "0.1.6";
		public const string MOD_COMPATIBLE_VERSIONS = "1.1.2";

		public const string CONSENT_REQUIRED_ID = "requiredConsent";

		public int RequiredConsentPercentage;
		public Main() : base(MOD_ID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_COMPATIBLE_VERSIONS, Assembly.GetExecutingAssembly()) { }

#if BEPINEX
		protected override void OnInitialise()
#endif
#if WORKSHOP
		protected override void OnPostActivate(Mod mod)
#endif
		{
			KitchenLib.IntPreference requiredConsent = PreferenceUtils.Register<KitchenLib.IntPreference>(MOD_ID, CONSENT_REQUIRED_ID, "Required Consent Percentage");
			requiredConsent.Value = 50;
			PreferenceUtils.Load();
			RequiredConsentPercentage = PreferenceUtils.Get<KitchenLib.IntPreference>(MOD_ID, CONSENT_REQUIRED_ID).Value;
			SetupPreferences();
		}

		public static Texture2D LoadImage(string base64)
		{
			byte[] bytes = Convert.FromBase64String(base64);

			Texture2D image = ResourceUtils.LoadTextureRaw(bytes);

			return image;
		}

		private void SetupPreferences()
		{
			Events.PreferenceMenu_MainMenu_SetupEvent += (s, args) =>
			{
				Type type = args.instance.GetType().GetGenericArguments()[0];
				args.mInfo.Invoke(args.instance, new object[] { MOD_NAME, typeof(MMOKitchenPreferences<>).MakeGenericType(type), false });
			};

			Events.PreferenceMenu_MainMenu_CreateSubmenusEvent += (s, args) =>
			{
				args.Menus.Add(typeof(MMOKitchenPreferences<MainMenuAction>), new MMOKitchenPreferences<MainMenuAction>(args.Container, args.Module_list));
			};

			//Setting Up For Pause Menu
			Events.PreferenceMenu_PauseMenu_SetupEvent += (s, args) =>
			{
				Type type = args.instance.GetType().GetGenericArguments()[0];
				args.mInfo.Invoke(args.instance, new object[] { MOD_NAME, typeof(MMOKitchenPreferences<>).MakeGenericType(type), false });
			};
			Events.PreferenceMenu_PauseMenu_CreateSubmenusEvent += (s, args) =>
			{
				args.Menus.Add(typeof(MMOKitchenPreferences<PauseMenuAction>), new MMOKitchenPreferences<PauseMenuAction>(args.Container, args.Module_list));
			};

			Events.PreferencesSaveEvent += (s, args) =>
			{
				int consentPercentage = PreferenceUtils.Get<KitchenLib.IntPreference>(MOD_ID, CONSENT_REQUIRED_ID).Value;
				RequiredConsentPercentage = consentPercentage;
			};
		}
	}
}