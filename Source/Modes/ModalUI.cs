using System;
using System.Collections.Generic;
using System.Linq;
using trwm.Source.Infrastructure;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class ModalUI : GameSynced
    {
        private List<Mode> modeStack = new List<Mode>();
        private Vector2 scrollPosition;
        private float boxWidth = 150f;
        private float boxHeight = 30f;
        private float padding = 5f;

        protected override void OnGui()
        {
            // Set up the scroll view in the bottom left corner
            float totalWidth = (modeStack.Count * (boxWidth + padding)) + padding;
            float totalHeight = Screen.height / 3f; // Use 1/3 of the screen height

            GUI.BeginScrollView(
                new Rect(padding, Screen.height - totalHeight - padding, Screen.width - padding * 2, totalHeight),
                scrollPosition,
                new Rect(0, 0, totalWidth, totalHeight)
            );

            float xOffset = padding;
            for (int i = 0; i < modeStack.Count; i++)
            {
                DrawModeStack(modeStack[i], xOffset, i == modeStack.Count - 1, totalHeight);
                xOffset += boxWidth + padding;
            }

            GUI.EndScrollView();
        }

        private void DrawModeStack(Mode mode, float xOffset, bool isActiveMode, float totalHeight)
        {
            float stackHeight = (mode.ActionMap.KeyCodes.Count() + 1) * (boxHeight + padding) + padding;
            float yOffset = totalHeight - stackHeight;

            // Draw mode name (title box)
            Color titleBoxColor = isActiveMode ? Color.green : GUI.color;
            GUI.color = titleBoxColor;
            GUI.Box(new Rect(xOffset, yOffset, boxWidth, boxHeight), mode.Name);
            GUI.color = Color.white;
            yOffset += boxHeight + padding;

            // Draw actions
            foreach (var action in mode.ActionMap.Actions)
            {
                GUI.Box(new Rect(xOffset, yOffset, boxWidth, boxHeight), $"{action.Name} ({action.Key})");
                yOffset += boxHeight + padding;
            }
        }

        public void PushMode(Mode mode)
        {
            modeStack.Add(mode);
        }

        public void PopMode()
        {
            if (modeStack.Count > 1)
            {
                modeStack.RemoveAt(modeStack.Count - 1);
            }
        }

        public void HighlightAction(string modeName, string actionName)
        {
            Mode mode = modeStack.Find(m => m.Name == modeName);
            if (mode != null)
            {
                //Action action = mode.Actions.Find(a => a.Name == actionName);
                //if (action != null)
                //{
                    //action.IsHighlighted = true;
                //}
            }
        }
    }
}