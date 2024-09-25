using trwm.Source.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace trwm.Source.Game
{
    // todo: make work because I dont want my game loop synchronizer to be wasted :(
    public class WorkspaceMover : GameSynced
    {
        public bool IsMoving => _moveTimer < 1f;
        
        private readonly ScrollRect _scrollRect;
        private readonly float _speed;

        private Vector2 _movement;
        private float _moveTimer;
        
        public WorkspaceMover(ScrollRect scrollRect, float speed)
        {
            _scrollRect = scrollRect;
            _speed = speed;
        }

        public void Move(Vector2 amount)
        {
            _movement = amount;
            _moveTimer = 0f;
        }

        protected override void Update()
        {
            if (IsMoving)
            {
                UpdatePosition();
                UpdateMoveTimer();
            }
        }

        private void UpdatePosition()
        {
            var t = _moveTimer / 1f;
            var frameMovement = Vector2.Lerp(Vector2.zero, _movement, t);
            _scrollRect.normalizedPosition += frameMovement;
        }

        private void UpdateMoveTimer()
        {
            _moveTimer += Time.deltaTime * _speed;

            if (_moveTimer > 1f)
            {
                _moveTimer = 1f;
            }
        }
    }
}