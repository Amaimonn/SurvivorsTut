using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public partial class PlayerInputSystem : SystemBase
    {
        private PlayerInput _input;
        private PlayerInput.PlayerActions _playerInput;

        protected override void OnCreate()
        {
            _input = new PlayerInput();
            _playerInput = _input.Player;
            _input.Enable();
        }

        protected override void OnUpdate()
        {
            var readMove = _playerInput.Move.ReadValue<Vector2>();
            var processInputJob = new ProcessInputJob() { MoveInput = readMove };
            processInputJob.ScheduleParallel();
        }
    }
}