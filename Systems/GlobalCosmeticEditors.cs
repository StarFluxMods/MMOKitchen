using Kitchen;
using KitchenLib.References;
using KitchenMods;

namespace MMOKitchen.Systems
{
    public class GlobalCosmeticEditors : InteractionSystem, IModSystem
    {
        protected override bool AllowActOrGrab => true;

        protected override bool ShouldAct(ref InteractionData data)
        {
            if (!Require(data.Target, out Editor))
            {
                return false;
            }
            bool flag = data.Attempt.Type == InteractionType.Grab;
            bool flag2 = base.ShouldAct(ref data);
            return Editor.UseGrab == flag && flag2;
        }

        protected override bool IsPossible(ref InteractionData data)
        {
            return Require(data.Target, out Editor) && Require(data.Target, out CAppliance cAppliance) && cAppliance.ID == ApplianceReferences.BedroomOutfitSelector;
        }

        protected override void Perform(ref InteractionData data)
        {
            Editor.IsTriggered = true;
            Editor.TriggerEntity = data.Interactor;
            SetComponent(data.Target, Editor);
        }

        private CTriggerPlayerSpecificUI Editor;
    }
}