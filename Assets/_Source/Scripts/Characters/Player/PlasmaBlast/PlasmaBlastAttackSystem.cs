using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace TMG.Survivors
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(AfterPhysicsSystemGroup))]
    [BurstCompile]
    public partial struct PlasmaBlastAttackSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var blastAttackJob = new PlasmaBlastAttackJob()
            {
                PlasmaBlastDataLookup = SystemAPI.GetComponentLookup<PlasmaBlastData>(),
                EnemyTagLookup = SystemAPI.GetComponentLookup<EnemyTag>(),
                DamageBufferLookup = SystemAPI.GetBufferLookup<DamageThisFrame>(),
                DestroyEntityFlagLookup = SystemAPI.GetComponentLookup<DestroyEntityFlag>(),
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = blastAttackJob.Schedule(simulationSingleton, state.Dependency);
        }
    }
}