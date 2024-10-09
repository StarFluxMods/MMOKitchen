using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace MMOKitchen.Systems
{
    public class PatchHelpers : GameSystemBase, IModSystem
    {

        public static PatchHelpers instance;
        
        public T _GetComponent<T>(Entity entity) where T : struct, IComponentData
        {
            return GetComponent<T>(entity);
        }
        
        public void _SetComponentData<T>(Entity entity, T componentData) where T : struct, IComponentData
        {
            EntityManager.SetComponentData<T>(entity, componentData);
        }

        protected override void Initialise()
        {
            instance = this;
        }

        protected override void OnUpdate()
        {
        }
    }
}