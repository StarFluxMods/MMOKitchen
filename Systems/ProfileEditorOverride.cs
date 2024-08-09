using Kitchen;
using KitchenMods;

namespace MMOKitchen.Systems
{
    public class ProfileEditorOverride : InteractionSystem, IModSystem
    {
        protected override bool IsPossible(ref InteractionData data)
        {
            return Require(data.Target, out Editor);
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