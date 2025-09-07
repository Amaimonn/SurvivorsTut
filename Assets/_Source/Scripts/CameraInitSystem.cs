using Unity.Entities;

namespace TMG.Survivors
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CameraInitSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InitCameraTargetTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if (CameraTargetSingleton.Instance == null)
                return;

            var entityCommandBuffer = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (cameraTarget, entity) in SystemAPI.Query<RefRW<CameraTarget>>()
                .WithAll<InitCameraTargetTag, PlayerTag>()
                .WithEntityAccess())
            {
                cameraTarget.ValueRW.TargetTransform = CameraTargetSingleton.Instance.transform;
                entityCommandBuffer.RemoveComponent<InitCameraTargetTag>(entity);
            }
            entityCommandBuffer.Playback(state.EntityManager);
        }
    }
}