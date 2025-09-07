using Unity.Entities;
using Unity.Mathematics;

namespace TMG.Survivors
{
    public struct CharacterMoveDirection : IComponentData
    {
        public float2 Value;
    }
}