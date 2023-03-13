using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using KitchenLib.Preferences;
using System.Collections.Generic;
using UnityEngine;

namespace MMOKitchenReborn.Menus
{
	public class PreferenceMenu<T> : KLMenu<T>
	{
		public PreferenceMenu(Transform container, ModuleList module_list) : base(container, module_list)
		{
		}

		public override void Setup(int player_id)
		{
			AddLabel("Required Consent Percentage");
			AddSelect<int>(requiredPercentage);
			requiredPercentage.OnChanged += delegate (object _, int result)
			{
				Main.manager.GetPreference<PreferenceInt>("requiredConsentPercentage").Set(result);
			};

			New<SpacerElement>(true);
			New<SpacerElement>(true);

			AddButton(base.Localisation["MENU_BACK_SETTINGS"], delegate (int i)
			{
				Main.manager.Save();
				this.RequestPreviousMenu();
			}, 0, 1f, 0.2f);
		}
		private Option<int> requiredPercentage = new Option<int>(new List<int> { 25, 50, 75, 100 }, Main.manager.GetPreference<PreferenceInt>("requiredConsentPercentage").Get(), new List<string> { "25%", "50%", "75%", "100%" });
	}
}
