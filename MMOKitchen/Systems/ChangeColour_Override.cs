using Kitchen;
using KitchenMods;
using UnityEngine;

namespace MMOKitchenReborn.Systems
{
	public class ChangeColour_Override : ChangeColour, IModSystem
	{
		protected override bool IsPossible(ref InteractionData data)
		{
			bool result;
			if (!Require(data.Target, out Selector))
				result = false;
			else
				result = Require(data.Interactor, out Colour);
			return result;
		}

		protected override void Perform(ref InteractionData data)
		{
			float num;
			float s;
			float v;
			Color.RGBToHSV(Colour.Color, out num, out s, out v);
			Colour.Color = Color.HSVToRGB(num + 0.05f, s, v);
			data.Context.Set(data.Interactor, Colour);
		}

		private CColourSelector Selector;

		private CPlayerColour Colour;
	}
}
