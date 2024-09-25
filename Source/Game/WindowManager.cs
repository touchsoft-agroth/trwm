using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using trwm.Source.Infrastructure;
using trwm.Source.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace trwm.Source.Game
{
    public class WindowManager
    {
        public WindowHandle? ActiveWindow { get; private set; }

        private Dictionary<string, Window> Windows => _workspace.openWindows;
        
        private readonly Workspace _workspace;
        private readonly Interpreter _interpreter;
        
        private readonly Logging.Logger _logger;
        private readonly Dictionary<WindowHandle, CodeWindow> _codeWindowCache;
        private readonly CachedKeyValueResolver<WindowHandle, TMP_InputField> _windowNameTexts;

        public WindowManager()
        {
            _workspace = Object.FindObjectOfType<Workspace>(true);
            _workspace.ThrowIfNull();

            _interpreter = Object.FindObjectOfType<Interpreter>(true);
            _interpreter.ThrowIfNull();

            _codeWindowCache = new Dictionary<WindowHandle, CodeWindow>();

            _windowNameTexts = new CachedKeyValueResolver<WindowHandle, TMP_InputField>(handle =>
            {
                var codeWindow = ResolveCodeWindow(handle);
                return codeWindow.GetFieldValue<TMP_InputField>("fileNameText");
            });

            _logger = new Logging.Logger(this);
            
            SetActive(GetAll().First());
        }

        public IEnumerable<WindowHandle> GetAll()
        {
            // todo: not optimal at all
            return Windows.Select(kvp => new WindowHandle(kvp.Key));
        }

        public List<string> GetAllWindowTitles()
        {
            return Windows.Keys.ToList();
        }

        public WindowHandle? FindByTitle(string windowTitle)
        {
            var handle = new WindowHandle(windowTitle);
            if (ResolveWindow(handle) == null)
            {
                return null;
            }

            return handle;
        }

        public void ToggleMinimized(WindowHandle handle)
        {
            var window = ResolveWindow(handle);
            if (window != null)
            {
                window.ToggleMinimize();
                _logger.Info($"Set window {handle.Identifier} minimized status to: {window.isMinimized}");
            }
        }

        public void SetActive(WindowHandle handle)
        {
            if (ActiveWindow != null)
            {
                SetWindowTitleColor(ActiveWindow.Value, Color.white);
            }
            
            ActiveWindow = handle;
            SetWindowTitleColor(handle, Color.green);
            
            _logger.Info($"Set window {handle.Identifier} as active window");
        }

        public void RunWindow(WindowHandle handle)
        {
            var codeWindow = ResolveCodeWindow(handle);
            if (codeWindow != null)
            {
                _logger.Info($"running window {handle.Identifier}");
                _interpreter.StartExecution(handle.Identifier);
            }
        }

        public void MoveTo(WindowHandle handle)
        {
            var window = ResolveWindow(handle);
            if (window != null)
            {
                var rectTransform = window.GetComponent<RectTransform>();
                _workspace.MoveCameraTo(rectTransform);
                window.MoveToFront();
            }
        }

        public void SetRandomActiveLol()
        {
            var windows = GetAll();
            var i = Random.Range(0, windows.Count());
            SetActive(windows.ElementAt(i));
        }

        public void MakeFocused(WindowHandle handle)
        {
            var window = ResolveWindow(handle);
            if (window != null)
            {
                var codeInputField = window.GetComponentInChildren<CodeInputField>();
                codeInputField.Select();
            }
        }

        private Window? ResolveWindow(WindowHandle handle)
        {
            return Windows.GetValueOrDefault(handle.Identifier);
        }

        private CodeWindow? ResolveCodeWindow(WindowHandle handle)
        {
            // todo: convert this cache to a cached key value resolver
            
            if (_codeWindowCache.TryGetValue(handle, out var cachedCodeWindow))
            {
                return cachedCodeWindow;
            }

            var window = ResolveWindow(handle);
            if (window != null)
            {
                var codeWindow = window.GetComponent<CodeWindow>();
                _codeWindowCache.Add(handle, codeWindow);
                return codeWindow;
            }

            return null;
        }

        private void SetWindowTitleColor(WindowHandle handle, Color color)
        {
            var windowNameTextField = _windowNameTexts.Get(handle);
            windowNameTextField.textComponent.color = color;
        }
    }
}