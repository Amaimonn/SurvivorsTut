using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace TMG.Survivors
{
    public partial struct CharacterMoveSystem : ISystem
    {
        [BurstCompile]
        public readonly void OnUpdate(ref SystemState state)
        {
            foreach (var (velocity, direction, speed) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<CharacterMoveDirection>, RefRO<CharacterMoveSpeed>>())
            {
                var moveStep2D = speed.ValueRO.Value * direction.ValueRO.Value;
                velocity.ValueRW.Linear = new float3(moveStep2D, 0.0f);
            }
        }
    }
}