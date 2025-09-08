using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace TMG.Survivors
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CharacterInitSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (mass, shouldInit) in SystemAPI.Query<RefRW<PhysicsMass>, EnabledRefRW<InitCharacterFlag>>())
            {
                mass.ValueRW.InverseInertia = float3.zero;
                shouldInit.ValueRW = false;
            }
        }
    }
}