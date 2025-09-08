using Unity.Entities;
using Unity.Rendering;

namespace TMG.Survivors
{
    [MaterialProperty("_AnimationIndex")]
    public struct AnimationIndexOverride : IComponentData
    {
        public float Value;
    }
}