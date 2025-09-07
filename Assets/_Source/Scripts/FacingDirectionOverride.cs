using Unity.Entities;
using Unity.Rendering;

namespace TMG.Survivors
{
    [MaterialProperty("_FacingDirection")]
    public partial struct FacingDirectionOverride : IComponentData
    {
        public float Value;
    }
}