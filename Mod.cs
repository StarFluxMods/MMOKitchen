using KitchenLib;
using KitchenMods;
using System.Reflection;
using Kitchen;
using KitchenLib.Event;
using KitchenLib.Preferences;
using MMOKitchen.Menus;
using KitchenLogger = KitchenLib.Logging.KitchenLogger;

namespace MMOKitchen
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.starfluxgames.mmokitchen";
        public const string MOD_NAME = "MMO Kitchen";
        public const string MOD_VERSION = "0.3.2";
        public const string MOD_AUTHOR = "StarFluxGames";
        public const string MOD_GAMEVERSION = ">=1.2.0";

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
            
            ModsPreferencesMenu<MenuAction>.RegisterMenu("MMO Kitchen", typeof(PreferenceMenu<MenuAction>), typeof(MenuAction));
            ModsPreferencesMenu<MenuAction>.RegisterMenu("MMO Kitchen", typeof(PreferenceMenu<MenuAction>), typeof(MenuAction));
            Events.MainMenuView_SetupMenusEvent += (s, args) =>
            {
                args.addMenu.Invoke(args.instance, new object[] { typeof(PreferenceMenu<MenuAction>), new PreferenceMenu<MenuAction>(args.instance.ButtonContainer, args.module_list) });
            };
            Events.PlayerPauseView_SetupMenusEvent += (s, args) =>
            {
                args.addMenu.Invoke(args.instance, new object[] { typeof(PreferenceMenu<MenuAction>), new PreferenceMenu<MenuAction>(args.instance.ButtonContainer, args.module_list) });
            };
        }
    }
}

