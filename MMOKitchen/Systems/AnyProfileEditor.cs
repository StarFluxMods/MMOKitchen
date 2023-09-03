using Kitchen;
using KitchenMods;

namespace MMOKitchenReborn.Systems;

public class AnyProfileEditor : InteractionSystem, IModSystem
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

    private CTriggerProfileEditor Editor;
}