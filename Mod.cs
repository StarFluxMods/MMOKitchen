using KitchenLib;
using KitchenLib.Logging;
using KitchenMods;
using System.Reflection;
using Kitchen;
using KitchenLib.Event;
using KitchenLib.Preferences;
using MMOKitchen.Menus;

namespace MMOKitchen
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.starfluxgames.mmokitchen";
        public const string MOD_NAME = "MMO Kitchen";
        public const string MOD_VERSION = "0.3.0";
        public const string MOD_AUTHOR = "StarFluxGames";
        public const string MOD_GAMEVERSION = ">=1.1.9";

        public static KitchenLogger Logger;
        public static PreferenceManager manager;

        public static int MaxPlayers = 100;
        
        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            Logger.LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            Logger = InitLogger();
            manager = new PreferenceManager("mmokitchen"); // Keeping the old GUID so we don't have to reset the preferences
            manager.RegisterPreference(new PreferenceInt("requiredConsentPercentage", 100));
            manager.RegisterPreference(new PreferenceBool("scaleAbove4Players", false));
            manager.RegisterPreference(new PreferenceFloat("scaleAbove4PlayersMultiplier", 0.1f));
            manager.Load();

            ModsPreferencesMenu<PauseMenuAction>.RegisterMenu(MOD_NAME, typeof(PreferenceMenu<PauseMenuAction>), typeof(PauseMenuAction));

            Events.PreferenceMenu_PauseMenu_CreateSubmenusEvent += (s, args) =>
            {
                args.Menus.Add(typeof(PreferenceMenu<PauseMenuAction>), new PreferenceMenu<PauseMenuAction>(args.Container, args.Module_list));
            };

            ModsPreferencesMenu<MainMenuAction>.RegisterMenu(MOD_NAME, typeof(PreferenceMenu<MainMenuAction>), typeof(MainMenuAction));

            Events.PreferenceMenu_MainMenu_CreateSubmenusEvent += (s, args) =>
            {
                args.Menus.Add(typeof(PreferenceMenu<MainMenuAction>), new PreferenceMenu<MainMenuAction>(args.Container, args.Module_list));
            };
        }
    }
}

