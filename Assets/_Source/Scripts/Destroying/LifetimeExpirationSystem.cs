using Unity.Entities;

namespace TMG.Survivors
{
    public partial struct LifetimeExpirationSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (expirationData, entity) in SystemAPI.Query<RefRO<LifetimeExpirationData>>()
                .WithPresent<DestroyEntityFlag>().WithEntityAccess())
            {
                var currentTime = SystemAPI.Time.ElapsedTime;
                if (expirationData.ValueRO.ExpirationTime > currentTime)
                    continue;

                SystemAPI.SetComponentEnabled<DestroyEntityFlag>(entity, true);
            }
        }
    }

    public partial struct LifetimeStartSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (lifetimeData, expirationData, entity) in SystemAPI.Query<RefRO<LifetimeData>,
                RefRW<LifetimeExpirationData>>()
                .WithDisabled<LifetimeExpirationData>()
                .WithEntityAccess())
            {
                expirationData.ValueRW.ExpirationTime = SystemAPI.Time.ElapsedTime + lifetimeData.ValueRO.Lifetime;
                SystemAPI.SetComponentEnabled<LifetimeExpirationData>(entity, true);
            }
        }
    }
}