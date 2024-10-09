using Kitchen;
using KitchenMods;
using Unity.Entities;
using UnityEngine;

namespace MMOKitchen.Systems
{
    public class GlobalProfileEditors : InteractionSystem, IModSystem
    {
        protected override bool IsPossible(ref InteractionData data)
        {
            return Has<CPlayer>(data.Interactor) && Has<ManageProfileEditors.COpensProfileEditor>(data.Target) && Has<CPosition>(data.Target) && Require(data.Target, out COwnedByPlayer cOwnedByPlayer) && cOwnedByPlayer.Player != data.Interactor;
        }

        protected override void Perform(ref InteractionData data)
        {
            if (!Require(data.Target, out CPosition cPosition)) return;
            if (!Require(data.Interactor, out CPlayer cPlayer)) return;
            
            Entity entity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(entity, new CPosition(cPosition + new Vector3(-2f, 1f, 0f)));
            EntityManager.AddComponentData(entity, new CRequiresView
            {
                Type = ViewType.ProfileEditor,
                ViewMode = ViewMode.WorldToScreen
            });
            EntityManager.AddComponentData(entity, new ManageProfileEditors.CProfileEditor
            {
                Trigger = data.Target,
                PlayerID = cPlayer.ID,
                Player = data.Interactor
            });
            EntityManager.AddComponentData(data.Target, new ManageProfileEditors.CHasProfileEditor
            {
                Editor = entity
            });
        }
    }
}