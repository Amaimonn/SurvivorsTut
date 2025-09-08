using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public partial struct GlobalTimeUpdateSystem : ISystem
    {
        private static readonly int _globalTimeShaderPropertyId = Shader.PropertyToID("_GlobalTime");

        public readonly void OnUpdate(ref SystemState state)
        {
            Shader.SetGlobalFloat(_globalTimeShaderPropertyId, (float)SystemAPI.Time.ElapsedTime);
        }
    }
}