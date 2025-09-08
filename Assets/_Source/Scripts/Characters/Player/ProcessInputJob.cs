using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    [WithAll(typeof(PlayerTag))]
    [BurstCompile]
    public partial struct ProcessInputJob : IJobEntity
    {
        public Vector2 MoveInput;

        public void Execute(ref CharacterMoveDirection direction)
        {
            direction.Value = MoveInput;
        }
    }
}