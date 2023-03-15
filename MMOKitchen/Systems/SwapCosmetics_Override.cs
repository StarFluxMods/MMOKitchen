using Kitchen;
using KitchenMods;

namespace MMOKitchenReborn.Systems
{
	public class SwapCosmetics_Override : SwapCosmetics, IModSystem
	{
		protected override bool IsPossible(ref InteractionData data)
		{
			base.IsPossible(ref data);
			bool result;
			if (!Require(data.Target, out Selector))
				result = false;
			else
				result = Require(data.Interactor, out Cosmetics);
			return result;
		}

		protected override void Perform(ref InteractionData data)
		{
			base.Perform(ref data);
		}

		private CCosmeticSelector Selector;

		private CPlayerCosmetics Cosmetics;
	}
}
