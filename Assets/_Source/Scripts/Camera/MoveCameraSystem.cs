using Unity.Entities;
using Unity.Transforms;

namespace TMG.Survivors
{
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct MoveCameraSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (camera, localToWorld) in SystemAPI.Query<RefRW<CameraTarget>, RefRO<LocalToWorld>>()
                .WithNone<InitCameraTargetTag>())
            {
                camera.ValueRW.TargetTransform.Value.position = localToWorld.ValueRO.Position;
            }
        }
    }
}