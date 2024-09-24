using System.Collections.Generic;

namespace trwm.Source.Game
{
    public class WindowManager
    {
        public WindowHandle? ActiveWindow { get; private set; }

        private readonly Dictionary<string, CodeWindow> _codeWindows;

        public void ToggleMinimized(WindowHandle handle)
        {
            var window = ResolveWindow(handle);
            if (window != null)
            {
                window.SetMinimized();
            }
        }

        private CodeWindow? ResolveWindow(WindowHandle handle)
        {
            return _codeWindows.GetValueOrDefault(handle.Identifier);
        }
    }
}