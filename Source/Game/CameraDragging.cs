using System;
using trwm.Source.Utils;
using UnityEngine.UI;

namespace trwm.Source.Game
{
    public static class CameraDragging
    {
        public static void SetEnabled(bool enabled)
        {
            var scrollRect = LazyScrollRect.Value;
            scrollRect.vertical = enabled;
            scrollRect.horizontal = enabled;
        }
        
        private static readonly Lazy<ScrollRect> LazyScrollRect = new Lazy<ScrollRect>(RetrieveWorkspaceScrollRect);

        private static ScrollRect RetrieveWorkspaceScrollRect()
        {
            var workspaceScrollRect = UnityEngine.Object.FindObjectOfType<Workspace>(true).GetFieldValue<ScrollRect>("scrollRect");
            workspaceScrollRect.ThrowIfNull();
            return workspaceScrollRect;
        }
    }
}