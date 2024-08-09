using System.Collections.Generic;
using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using KitchenLib.Preferences;
using UnityEngine;

namespace MMOKitchen.Menus
{
    public class PreferenceMenu<T> : KLMenu<T>
    {
        public PreferenceMenu(Transform container, ModuleList module_list) : base(container, module_list)
        {
        }

        private Option<int> requiredPercentage = new Option<int>(new List<int> { 25, 50, 75, 100 }, Mod.manager.GetPreference<PreferenceInt>("requiredConsentPercentage").Get(), new List<string> { "25%", "50%", "75%", "100%" });
        private Option<float> scaleMultiplier = new Option<float>(new List<float> { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f }, Mod.manager.GetPreference<PreferenceFloat>("scaleAbove4PlayersMultiplier").Get(), new List<string> { "0.1", "0.2", "0.3", "0.4", "0.5", "0.6", "0.7", "0.8", "0.9", "1.0" });
        private Option<bool> scaleEnabled = new Option<bool>(new List<bool> { true, false }, Mod.manager.GetPreference<PreferenceBool>("scaleAbove4Players").Get(), new List<string> { "Enabled", "Disabled" });
        
        public override void Setup(int player_id)
        {
            New<SpacerElement>(true);
            AddLabel("Required Consent Percentage");
            AddSelect<int>(requiredPercentage);
            requiredPercentage.OnChanged += delegate (object _, int result)
            {
                Mod.manager.GetPreference<PreferenceInt>("requiredConsentPercentage").Set(result);
            };

            New<SpacerElement>(true);

            AddLabel("Scale Above 4 Players");
            AddSelect<bool>(scaleEnabled);
            scaleEnabled.OnChanged += delegate (object _, bool result)
            {
                Mod.manager.GetPreference<PreferenceBool>("scaleAbove4Players").Set(result);
            };

            AddLabel("Scale Multiplier");
            AddSelect<float>(scaleMultiplier);
            scaleMultiplier.OnChanged += delegate (object _, float result)
            {
                Mod.manager.GetPreference<PreferenceFloat>("scaleAbove4PlayersMultiplier").Set(result);
            };

            New<SpacerElement>(true);
            New<SpacerElement>(true);

            AddButton(base.Localisation["MENU_BACK_SETTINGS"], delegate (int i)
            {
                Mod.manager.Save();
                this.RequestPreviousMenu();
            }, 0, 1f, 0.2f);
        }
    }
}
