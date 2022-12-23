using Kitchen.Modules;
using Kitchen;
using KitchenLib.Utils;
using KitchenLib;
using System.Collections.Generic;
using UnityEngine;

namespace MMOKitchen.Menus
{
	public class MMOKitchenPreferences<T> : KLMenu<T>
	{
		public MMOKitchenPreferences(Transform container, ModuleList module_list) : base(container, module_list)
		{
		}
		public override void Setup(int player_id)
		{
			this.Percentage = new Option<int>(
				new List<int>
				{
						25, 50, 75, 100
				},
				PreferenceUtils.Get<KitchenLib.IntPreference>(Main.MOD_ID, Main.CONSENT_REQUIRED_ID).Value,
				new List<string>
				{
						"25%", "50%", "75%", "100%"
				});

			AddLabel("Required Consent Percentage");
			Add<int>(this.Percentage).OnChanged += delegate (object _, int f)
			{
				PreferenceUtils.Get<KitchenLib.IntPreference>(Main.MOD_ID, Main.CONSENT_REQUIRED_ID).Value = f;
			};

			New<SpacerElement>();
			New<SpacerElement>();

			AddButton("Apply", delegate
			{
				PreferenceUtils.Save();
			});

			AddButton(base.Localisation["MENU_BACK_SETTINGS"], delegate
			{
				RequestPreviousMenu();
			});
		}

		private Option<int> Percentage;
	}
}
