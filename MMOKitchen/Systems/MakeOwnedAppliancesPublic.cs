using Kitchen;
using Unity.Collections;
using Unity.Entities;

namespace MMOKitchenReborn.Systems
{
	public class MakeOwnedAppliancesPublic : GenericSystemBase
	{
		protected override void Initialise()
		{
			base.Initialise();
			this.query = GetEntityQuery(new EntityQueryDesc
			{
				All = new ComponentType[]
				{
					typeof(COwnedByPlayer)
				}
			});
		}

		protected override void OnUpdate()
		{
			NativeArray<Entity> nativeArray = query.ToEntityArray(Allocator.Temp);

			for (int i = 0; i < nativeArray.Length; i++)
			{
				EntityManager.RemoveComponent<COwnedByPlayer>(nativeArray[i]);
			}
		}

		private EntityQuery query;
	}
}
