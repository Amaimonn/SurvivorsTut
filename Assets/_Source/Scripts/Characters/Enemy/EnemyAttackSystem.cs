using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace TMG.Survivors
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(AfterPhysicsSystemGroup))]
    public partial struct EnemyAttackSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            foreach (var (currentCooldown, isOnCooldown) in SystemAPI.Query<RefRW<EnemyCooldownExpirationTimestep>,
                EnabledRefRW<EnemyCooldownExpirationTimestep>>())
            {
                if (elapsedTime < currentCooldown.ValueRO.Value)
                    continue;

                isOnCooldown.ValueRW = false;
            }

            var attackJob = new EnemyAttackJob()
            {
                PlayerLookup = SystemAPI.GetComponentLookup<PlayerTag>(isReadOnly: true),
                EnemyAttackLookup = SystemAPI.GetComponentLookup<EnemyAttackData>(isReadOnly: true),
                EnemyCooldownLookup = SystemAPI.GetComponentLookup<EnemyCooldownExpirationTimestep>(),
                DamageThisFrameLookup = SystemAPI.GetBufferLookup<DamageThisFrame>(),
                ElapsedTime = elapsedTime
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = attackJob.Schedule(simulationSingleton, state.Dependency);
        }
    }
}