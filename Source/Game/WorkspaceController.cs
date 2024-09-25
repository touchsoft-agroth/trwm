using trwm.Source.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace trwm.Source.Game
{
    public class WorkspaceController
    {
        public WorkspaceController()
        {
            _workspace = Object.FindObjectOfType<Workspace>(true);
            _workspace.ThrowIfNull();
            
            _scrollRect = _workspace.GetFieldValue<ScrollRect>("scrollRect");
            _scrollRect.ThrowIfNull();
        }

        public void Move(Vector2 movement)
        {
            const float scalar = 0.05f;
            movement *= scalar;
            
            if (movement.magnitude > 1f)
            {
                movement = movement.normalized;
            }
            
            _scrollRect.normalizedPosition += movement;
        }

        public void Zoom(float amount)
        {
            _workspace.Zoom(amount);
        }
 
        private readonly Workspace _workspace;
        private readonly ScrollRect _scrollRect;
    }
}