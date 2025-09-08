using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace TMG.Survivors
{
    public partial struct CharacterMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (velocity, facingDirection, direction, speed, entity) in
                SystemAPI.Query<RefRW<PhysicsVelocity>,
                    RefRW<FacingDirectionOverride>,
                    RefRO<CharacterMoveDirection>,
                    RefRO<CharacterMoveSpeed>>()
                        .WithEntityAccess())
            {
                var moveStep2D = speed.ValueRO.Value * direction.ValueRO.Value;
                velocity.ValueRW.Linear = new float3(moveStep2D, 0.0f);

                if (math.abs(moveStep2D.x) > 0.05f)
                    facingDirection.ValueRW.Value = moveStep2D.x > 0 ? 1 : -1;

                if (SystemAPI.HasComponent<PlayerTag>(entity))
                {
                    var playerAnimationOverride = SystemAPI.GetComponentRW<AnimationIndexOverride>(entity);
                    var animationType = math.lengthsq(moveStep2D) > float.Epsilon ?
                        PlayerAnimationIndex.Walk :
                        PlayerAnimationIndex.Idle;
                    playerAnimationOverride.ValueRW.Value = (float)animationType;
                }
            }
        }
    }
}